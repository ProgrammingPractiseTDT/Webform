namespace Webform.Models
{
    public class ProductInCartViewModel
    {
        public  Webform.Models.ItemInCart CartItem { get; set; }
        public  Webform.Models.Product CartProduct { get; set; }

        public double Cost { get; set; }

        public string ImageSrc { get; set; }

        public int CartID { get; set; }

    }
}
