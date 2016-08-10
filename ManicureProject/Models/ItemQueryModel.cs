namespace ManicureProject.Models
{
    public class ItemQueryModel
    {
        public ItemQueryModel()
        {
            PerPage = 20;
            Page = 0;
        }

        public string FilterText { get; set; }

        public int PerPage { get; set; }
        public int Page { get; set; }
    }
}