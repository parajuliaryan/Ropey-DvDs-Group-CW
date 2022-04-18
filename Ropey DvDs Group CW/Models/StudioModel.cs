using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models
{
    public class StudioModel
    {
        [Key]
        public int StudioNumber { get; set; }

        public string? StudioName { get; set; }  

        public ICollection<DVDTitleModel> DVDTitles { get; set; }
    }
}
