namespace _0_Framework.Domain.Offer
{
    public class OfferEntity : BaseEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsAccept { get; set; }
        public bool IsCancel { get; set; }

        public OfferEntity(string title, double price, string description)
        {
            Title = title;
            Price = price;
            Description = description;
            IsAccept = false;
            IsCancel = false;
        }
        public void Edit(string title, double price, string description)
        {
            Title = title;
            Price = price;
            Description = description;
        }
        public void Accept()
        {
            IsAccept = true;
            IsCancel = false;
        }

        public void Cancel()
        {
            IsAccept = false;
            IsCancel = true;
        }
    }
}
