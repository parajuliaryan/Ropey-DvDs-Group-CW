using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ropey_DvDs_Group_CW.Models
{
    public class DVDCopyModel
    {
        [Key]
        public int CopyNumber { get; set; }

        public int DVDNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public DVDTitleModel? DVDTitleModel { get; set; }
        public DateTime DatePurchased { get; set; }

        public ICollection<LoanModel>? Loans { get; set; }

    }
}
