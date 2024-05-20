using System.ComponentModel.DataAnnotations;

namespace DigiMedia.ViewModels
{
    public class MemberLoginVm
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }        

    }
}
