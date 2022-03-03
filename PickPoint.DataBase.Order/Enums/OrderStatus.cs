namespace PickPoint.DataBase.Order
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Не определен (по умолчанию)
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// Зарегистрирован 
        /// </summary>
        Registred = 1,
        /// <summary>
        /// Принят на складе
        /// </summary>
        Warehoused = 2,
        /// <summary>
        /// Выдан курьеру 
        /// </summary>
        Carried = 3,
        /// <summary>
        /// Доставлен в постамат 
        /// </summary>
        InParcelLocker = 4,
        /// <summary>
        /// Доставлен получателю 
        /// </summary>
        Recipiented = 5,
        /// <summary>
        /// Отменен
        /// </summary>
        Canceled = 6
    }
}
