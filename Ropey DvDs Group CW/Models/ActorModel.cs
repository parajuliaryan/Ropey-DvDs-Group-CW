using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models
{
    public class ActorModel
    {
        [Key]
        public int ActorNumber { get; set; }

        public string? ActorFirstName { get; set; }

        public string? ActorSurname { get; set; }

        public ICollection<CastMemberModel>? CastMembers { get; set; }
    }
}
