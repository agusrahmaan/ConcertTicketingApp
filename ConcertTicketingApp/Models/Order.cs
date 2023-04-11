using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketingApp.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(25, MinimumLength = 2, ErrorMessage = "Name must be 2-25 characters")]
        public string Name { get; set; }
        public string Email { get; set; }

        [StringLength(13, MinimumLength = 12, ErrorMessage = "No Handphone must be 12 or 13 characters")]
        public string NoTelepon { get; set; }
    }
}
