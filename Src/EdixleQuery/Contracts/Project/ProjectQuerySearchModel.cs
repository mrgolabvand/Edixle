namespace EdixleQuery.Contracts.Project
{
    public class ProjectQuerySearchModel
    {
        public string SearchWord { get; set; }
        public int OrderId { get; set; }
        public int MinPay { get; set; }
        public int MaxPay { get; set; }
        public bool IsOpen { get; set; }
    }
}
