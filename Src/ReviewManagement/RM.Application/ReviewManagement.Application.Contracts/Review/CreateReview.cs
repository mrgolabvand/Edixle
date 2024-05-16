namespace ReviewManagement.Application.Contracts.Review
{
    public class CreateReview
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string Strength { get; set; }
        public string Weakness { get; set; }
        public int Rate { get; set; }
        public int EditVideoQuality { get; set; }
        public int EditSoundQuality { get; set; }
        public int UseProperVideoEffects { get; set; }
        public int UseProperSoundEffects { get; set; }
        public int UseProperMemes { get; set; }
        public int UseProperFontAndColors { get; set; }
        public long OwnerRecordId { get; set; }
    }
}