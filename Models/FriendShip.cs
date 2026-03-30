using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALIZE.Models
{
    public class FriendShip
    {
        [Key]
        public int Id { get; set; }

        public string? RequestorId { get; set; }
        [ForeignKey("RequestorId")]
        public AppUser? Requestor { get; set; }

        public string? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public AppUser? Receiver { get; set; }

        public string Status { get; set; } = "Pending";

    
        public DateTime TimeStamp { get; set; }


    }
}
