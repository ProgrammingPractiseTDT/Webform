namespace Webform.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int SalesAgentID { get; set; }

        public bool Ordered { get; set; }
    }
}
