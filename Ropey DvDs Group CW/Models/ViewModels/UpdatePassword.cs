using System.ComponentModel.DataAnnotations;

namespace Ropey_DvDs_Group_CW.Models.ViewModels
{
    public class UpdatePassword
    {
            [Required, DataType(DataType.Password), Display(Name = "Current Password")]
            public string? CurrentPassword { get; set; }
            [Required, DataType(DataType.Password), Display(Name = "New Password")]
            public string? NewPassword { get; set; }
            [Required, DataType(DataType.Password), Display(Name = "Confirm Password")]
            [Compare("NewPassword", ErrorMessage = "Confirm Password Doesn't Match")]
            public string? ConfirmPassword { get; set; }
    }
}
