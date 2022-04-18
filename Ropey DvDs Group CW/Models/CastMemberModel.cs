using System.ComponentModel.DataAnnotations.Schema;

namespace Ropey_DvDs_Group_CW.Models
{
    public class CastMemberModel
    {
        public int Id { get; set; }
        public int DVDNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public DVDTitleModel? DVDTitleModel { get; set; }

        public int ActorNumber { get; set; }
        [ForeignKey("ActorNumber")]
        public ActorModel? ActorModel { get; set; }
        
    }
}
