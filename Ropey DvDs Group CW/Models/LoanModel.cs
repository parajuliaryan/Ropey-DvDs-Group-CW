namespace Ropey_DvDs_Group_CW.Models
{
    public class LoanModel
    {
        public int LoanNumber { get; set; }

        public int LoanTypeNumber { get; set; }

        public int CopyNumber { get; set; }

        public int MemberNumber { get; set; }

        public DateTime DateOut { get; set; }

        public DateTime DateDue { get; set; }

        public DateTime DateReturned { get; set; }
    }
}
