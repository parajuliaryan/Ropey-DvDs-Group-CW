using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models
{
    public class ProducerModel
    {
        [Key]
        public int ProducerNumber { get; set; }

        public string? ProducerName { get; set; }

        public ICollection<DVDTitleModel> DVDTitles { get; set; }


    }
}
