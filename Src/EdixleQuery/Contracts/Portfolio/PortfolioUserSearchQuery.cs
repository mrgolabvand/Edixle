namespace EdixleQuery.Contracts.Portfolio
{
    public class PortfolioUserSearchQuery
    {
        public string Name { get; set; }
        public long OrderId { get; set; }
        public Dictionary<long, bool> CategoriesDictionary { get; set; }
    }
}
