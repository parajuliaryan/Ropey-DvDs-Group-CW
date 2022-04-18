using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models
{
    public class LoanTypeModel
    {
        [Key]
        public int LoanTypeNumber { get; set; }

        public string? LoanType { get; set; }

        public int LoanDuration { get; set; }

        public ICollection<LoanModel> Loans { get; set; }

    }
}
