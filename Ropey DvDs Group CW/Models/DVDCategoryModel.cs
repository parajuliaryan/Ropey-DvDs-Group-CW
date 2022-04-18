using System.ComponentModel.DataAnnotations;
namespace Ropey_DvDs_Group_CW.Models
{
    public class DVDCategoryModel
    {
        [Key]
        public int CategoryNumber
        {
            get; set;
        }
        public string? CategoryDescription
        {
            get; set;
        }

        public ICollection<DVDTitleModel> DVDTitles { get; set; }
    }
}
