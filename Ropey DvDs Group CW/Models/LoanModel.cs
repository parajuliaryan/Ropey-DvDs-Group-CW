using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ropey_DvDs_Group_CW.Models
{
    public class LoanModel
    {
        [Key]
        public int LoanNumber { get; set; }

        public int LoanTypeNumber { get; set; }
        [ForeignKey("LoanTypeNumber")]
        public LoanTypeModel? LoanTypeModel { get; set; }
        public int CopyNumber { get; set; }
        [ForeignKey("CopyNumber")]
        public DVDCopyModel? DVDCopyModel { get; set; }
        public int MemberNumber { get; set; }
        [ForeignKey("MemberNumber")]
        public MemberModel? MemberModel { get; set; }
        public DateTime DateOut { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime? DateReturned { get; set; }
    }
}
