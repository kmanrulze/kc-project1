namespace StoreApp.BusinessLogic.Objects
{
    public class Product
    {
        public int productTypeID { get; set; }
        public string name { get; set; }
        public int amount = 0;
        public decimal price { get; set; }
    }
}
