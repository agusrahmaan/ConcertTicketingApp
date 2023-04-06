using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketingApp.Models
{
    public class DataConcert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }
        public string NamaKonser { get; set; }
        public string Musisi { get; set; }
        public string TempatKonser { get; set; }
        public DateTime TanggalKonser { get; set; }
        public int KuotaTiket { get; set; }
        public string Status { get; set; }

    }
}
