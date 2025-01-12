using System.ComponentModel.DataAnnotations;

namespace WorkersMVC.Model
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Pole Nazwa użytkownika jest wymagane")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        public string Password { get; set; }
    }
}
