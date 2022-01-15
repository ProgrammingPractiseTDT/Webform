namespace Webform.Models
{
    public class DeliverySlip
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        public string ShipAdress { get; set; }

        public int status{ get; set; }
        public bool Payment { get; set; }
       
    }
}
