using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketingApp.Models
{
    public class DataConcert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        [Required]
        public string NamaKonser { get; set; }
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be 2-25 characters")]
        public string Musisi { get; set; }
        public string TempatKonser { get; set; }
        public DateTime TanggalKonser { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Input must be a number")]
        public int KuotaTiket { get; set; }
        public string Status { get; set; }
        public string? Photo { get; set; }
    }
}
