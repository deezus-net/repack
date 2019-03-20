using System.ComponentModel.DataAnnotations;

namespace repack.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "RequiredId")]
        public string Id { get; set; }
        [Required(ErrorMessage = "RequiredPassword")]
        public string Password { get; set; }
    }
}