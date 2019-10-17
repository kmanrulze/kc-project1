namespace StoreApp.BusinessLogic.Objects
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }

        public bool CheckAddressNotNull()
        {
            if(street != null && city != null && state != null && zip != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}