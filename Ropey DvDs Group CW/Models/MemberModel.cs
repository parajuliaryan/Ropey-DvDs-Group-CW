using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ropey_DvDs_Group_CW.Models
{
    public class MemberModel
    {
        [Key]
        public int MemberNumber { get; set; }

        public int MembershipCategoryNumber { get; set; }
        [ForeignKey("MembershipCategoryNumber")]
        public MembershipCategoryModel? membershipCategoryModel { get; set; }

        public string? MemberLastName { get; set; }

        public string? MemberFirstName { get; set; }

        public string? MemberAddress { get; set; }

        public DateTime MemberDateOfBirth { get; set; }

        public ICollection<LoanModel>? LoanModels { get; set; }

    }
}
