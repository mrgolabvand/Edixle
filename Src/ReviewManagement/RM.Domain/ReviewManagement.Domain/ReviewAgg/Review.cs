using _0_Framework.Domain;

namespace ReviewManagement.Domain.ReviewAgg
{
    public class Review : BaseEntity
    {
        public string Name { get; private set; }
        public string Message { get; private set; }
        public string Strength { get; private set; }
        public string Weakness { get; private set; }
        public int Rate { get; private set; }
        public int IsHelpful { get; private set; }
        public int IsHarmful { get; private set; }
        public int EditVideoQuality { get; private set; }
        public int EditSoundQuality { get; private set; }
        public int UseProperVideoEffects { get; private set; }
        public int UseProperSoundEffects { get; private set; }
        public int UseProperMemes { get; private set; }
        public int UseProperFontAndColors { get; private set; }
        public long OwnerRecordId { get; private set; }
        public bool IsConfirmed { get; private set; } = true;

        public Review(string name, string message, string strength, string weakness, int rate, int editVideoQuality, int editSoundQuality, int useProperVideoEffects, int useProperSoundEffects, int useProperMemes, int useProperFontAndColors, long ownerRecordId)
        {
            Name = name;
            Message = message;
            Strength = strength;
            Weakness = weakness;
            Rate = rate;
            EditVideoQuality = editVideoQuality;
            EditSoundQuality = editSoundQuality;
            UseProperVideoEffects = useProperVideoEffects;
            UseProperSoundEffects = useProperSoundEffects;
            UseProperMemes = useProperMemes;
            UseProperFontAndColors = useProperFontAndColors;
            OwnerRecordId = ownerRecordId;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsConfirmed = false;
        }

        public void IncreaseHelpful()
        {
            IsHelpful += 1;
        }

        public void IncreaseHarmful()
        {
            IsHarmful += 1;
        }
    }
}
