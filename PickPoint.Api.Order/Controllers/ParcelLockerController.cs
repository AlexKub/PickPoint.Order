using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PickPoint.Api.Order.Models;
using PickPoint.DataBase.Order;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PickPoint.Api.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParcelLockerController : ControllerBase
    {
        private readonly ILogger<ParcelLockerController> _logger;
        private readonly OrdersContext _dbContext;

        public ParcelLockerController(ILogger<ParcelLockerController> logger, OrdersContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var parcel_lockers = await _dbContext.ParcelLockers.ToListAsync();

                var response = parcel_lockers.Select(pl => new ParcelLockerInfo
                {
                    Number = pl.Number,
                    IsActive = pl.IsActive,
                    Address = pl.Address
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get parcel locker info exception");

                return BadRequest();
            }
        }

        [HttpGet("{plNumber}")]
        public async Task<IActionResult> Get(string plNumber)
        {
            try
            {
                var parcel_locker = await _dbContext.ParcelLockers.FirstOrDefaultAsync(o => o.Number == plNumber);

                if (parcel_locker == null)
                {
                    return NotFound();
                }

                var infoModel = new ParcelLockerInfo
                {
                    Number = plNumber,
                    IsActive = parcel_locker.IsActive,
                    Address = parcel_locker.Address
                };

                return Ok(infoModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get parcel locker info exception");

                return BadRequest();
            }
        }
    }
}
