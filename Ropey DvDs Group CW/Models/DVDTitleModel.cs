namespace Ropey_DvDs_Group_CW.Models
{
    public class DVDTitleModel
    {
        public int DVDNumber { get; set; }

        public int CategoryNumber { get; set; }

        public int StudioNumber { get; set; }

        public int ProducerNumber { get; set; }

        public string? DVDTitle { get; set; }

        public DateTime DateReleased { get; set; }

        public int StandardCharge { get; set; }

        public int PenaltyCharge { get; set; }

    }
}
