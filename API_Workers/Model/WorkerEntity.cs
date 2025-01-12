using System.ComponentModel.DataAnnotations;

namespace API_Workers.Model
{
    public class WorkerEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole Stanowisko jest wymagane")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Pole Zarobki jest wymagane")]
        [Range(1, int.MaxValue, ErrorMessage = "Pole Wynagrodzenie powinno być większe od 0")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Pole Adres E-Mail jest wymagane")]
        [EmailAddress(ErrorMessage = "Pole Adres E-mail zawiera nieodpowiednią wartość")]
        public string EmailAddress { get; set; }
    }
}
