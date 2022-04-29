using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ropey_DvDs_Group_CW.Models
{
    public class DVDTitleModel
    {
        [Key]
        public int DVDNumber { get; set; }
        public int CategoryNumber { get; set; }
        [ForeignKey("CategoryNumber")]
        public DVDCategoryModel? DVDCategoryModel { get; set; }

        public int StudioNumber { get; set; }
        [ForeignKey("StudioNumber")]

        public StudioModel? StudioModel { get; set; }
        public int ProducerNumber { get; set; }
        [ForeignKey("ProducerNumber")]
        public ProducerModel? ProducerModel { get; set; }
        public string? DVDTitle { get; set; }

        public DateTime DateReleased { get; set; }

        public int StandardCharge { get; set; }

        public int PenaltyCharge { get; set; }

        public ICollection<CastMemberModel>? CastMembers { get; set; }

        public ICollection<DVDCopyModel>? DVDCopys { get; set; }

    }
}
