namespace EdixleQuery.Contracts.Review
{
    public class ReviewQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public int IsHelpful { get; set; }
        public int IsHarmful { get; set; }
        public int EditVideoQuality { get; set; }
        public int EditSoundQuality { get; set; }
        public int UseProperVideoEffects { get; set; }
        public int UseProperSoundEffects { get; set; }
        public int UseProperMemes { get; set; }
        public int UseProperFontAndColors { get; set; }
        public long OwnerRecordId { get; set; }
        public bool IsConfirmed { get; set; }
        public string CreationDate { get; set; }
    }
}
