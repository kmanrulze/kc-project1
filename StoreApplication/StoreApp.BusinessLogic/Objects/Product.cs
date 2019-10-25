namespace StoreApp.BusinessLogic.Objects
{
    public class Product
    {
        public int productTypeID { get; set; }
        public string name { get; set; }
        private int amount = 0;
        public decimal price { get; set; }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
