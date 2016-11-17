namespace SportsStore.Models.Domain
{
    public class OrderLine : CartLine
    {
        #region Properties
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        #endregion
    }
}