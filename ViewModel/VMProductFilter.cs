namespace Code_EcomMgmtDB1.ViewModel
{
    public class VMProductFilter
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public List<int>? ProductIds{ get; set; }
    }
}
