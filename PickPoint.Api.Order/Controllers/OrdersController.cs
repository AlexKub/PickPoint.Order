using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PickPoint.Api.Order.Models;
using PickPoint.DataBase.Order;
using PickPoint.DataBase.Order.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PickPoint.Api.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;
        private readonly OrdersContext _dbContext;

        public OrdersController(ILogger<OrdersController> logger, OrdersContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderModel createOrderModel)
        {
            try
            {
                var parcelLocker = await _dbContext.ParcelLockers.FirstOrDefaultAsync(l => l.Number == createOrderModel.ParcelLockNumber);

                if (parcelLocker == null)
                {
                    return NotFound();
                }

                if (parcelLocker.IsActive == false)
                {
                    return StatusCode(403);
                }

                var newOrder = new DataBase.Order.Models.Order
                {
                    ParcelLockerId = parcelLocker.Id,
                    Price = createOrderModel.Price,
                    RecipientPhone = createOrderModel.RecipientPhone,
                    RecipientFullName = createOrderModel.RecipientFullName,
                    Status = OrderStatus.Registred
                };

                _dbContext.Orders.Add(newOrder);

                await _dbContext.SaveChangesAsync();

                var articles = createOrderModel.Articles.Select(a => new OrderArticle
                {
                    OrderId = newOrder.Id,
                    Article = a
                });

                _dbContext.OrderArticles.AddRange(articles);

                await _dbContext.SaveChangesAsync();

                return Ok(new CreateOrderResult
                {
                    Number = newOrder.Number
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create order exception");

                return BadRequest();
            }
        }

        [HttpPut("{orderNumber}")]
        public async Task<IActionResult> Update(int orderNumber, [FromBody] UpdateOrderModel updateModel)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Articles)
                    .Include(o => o.ParcelLocker)
                    .FirstOrDefaultAsync(o => o.Number == orderNumber);

                if (order == null)
                {
                    return NotFound();
                }

                order.RecipientFullName = updateModel.RecipientFullName;
                order.RecipientPhone = updateModel.RecipientPhone;
                order.Price = updateModel.Price;

                var orderArticles = await _dbContext.OrderArticles.Where(a => a.OrderId == order.Id).ToListAsync();

                if (updateModel.Articles == null || updateModel.Articles.Length == 0)
                {
                    _dbContext.RemoveRange(orderArticles);
                }
                else
                {
                    foreach (var article in updateModel.Articles)
                    {
                        if (orderArticles.Any(oa => oa.Article == article))
                        {
                            continue;
                        }
                        else
                        {
                            _dbContext.OrderArticles.Add(new OrderArticle
                            {
                                OrderId = order.Id,
                                Article = article
                            });
                        }
                    }

                    foreach (var orderArticle in orderArticles)
                    {
                        if (!updateModel.Articles.Contains(orderArticle.Article))
                        {
                            _dbContext.OrderArticles.Remove(orderArticle);
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();

                return Ok(new OrderInfoModel
                {
                    Number = order.Number,
                    Status = (int)order.Status,
                    ParcelLockerNumber = order.ParcelLocker.Number,
                    Price = order.Price,
                    RecipientFullName = order.RecipientFullName,
                    RecipientPhone = order.RecipientPhone,
                    Articles = updateModel.Articles,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Update order exception");

                return BadRequest();
            }
        }

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> Get(int orderNumber)
        {
            try
            {
                var order = await _dbContext.Orders
                    .Include(o => o.Articles)
                    .Include(o => o.ParcelLocker)
                    .FirstOrDefaultAsync(o => o.Number == orderNumber);

                if (order == null)
                {
                    return NotFound();
                }

                var infoModel = new OrderInfoModel
                {
                    Number = order.Number,
                    Articles = order.Articles?.Select(a => a.Article).ToArray(),
                    ParcelLockerNumber = order.ParcelLocker.Number,
                    Price = order.Price,
                    RecipientFullName = order.RecipientFullName,
                    RecipientPhone = order.RecipientPhone,
                    Status = (int)order.Status
                };

                return Ok(infoModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get order info exception");

                return BadRequest();
            }
        }


        [HttpDelete("{orderNumber}")]
        public async Task<IActionResult> Cancel(int orderNumber)
        {
            try
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Number == orderNumber);

                if (order == null)
                {
                    return NotFound();
                }

                order.Status = OrderStatus.Canceled;

                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cancel order exception");

                return BadRequest();
            }
        }
    }
}
