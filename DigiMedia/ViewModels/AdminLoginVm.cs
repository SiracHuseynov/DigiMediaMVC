using System.ComponentModel.DataAnnotations;

namespace DigiMedia.ViewModels
{
    public class AdminLoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }      

    }
}
