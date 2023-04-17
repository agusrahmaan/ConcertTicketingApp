using System.ComponentModel.DataAnnotations;

namespace ConcertTicketingApp.Models.ViewModels
{
    public class OrderForm
    {
        public int  DataConcertId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string NoTelepon { get; set; }
    }
}
