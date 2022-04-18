using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models
{
    public class MembershipCategoryModel
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }

        public string? MembershipCategoryDescription { get; set; }

        public int MembershipCategoryTotalLoan { get; set; }

        public ICollection<MemberModel> Members { get; set; }

    }
}
