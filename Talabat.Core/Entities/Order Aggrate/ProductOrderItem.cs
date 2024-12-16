namespace Talabat.Core.Entities.Order_Aggrate
{
    public class ProductOrderItem
    {
        public ProductOrderItem() 
        { 

        }
        public ProductOrderItem(int productId, string productName, string productUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}