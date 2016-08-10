namespace ManicureProject.Models
{
    public class OrderQueryModel
    {
        public OrderQueryModel()
        {
            PerPage = 20;
            Page = 0;
        }

        public int PerPage { get; set; }
        public int Page { get; set; }
        public string FilterText { get; set; }
    }
}