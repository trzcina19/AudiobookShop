namespace Shop.Models
{
    public class PositionOrder
    {
        public int PositionOrderId { get; set; }
        public int OrderId { get; set; }
        public int AudiobookId { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Audiobook audiobook { get; set; }
        public virtual Order order { get; set; }
    }
}