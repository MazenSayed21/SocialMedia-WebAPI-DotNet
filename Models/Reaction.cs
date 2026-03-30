using SOCIALIZE.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALIZE.Models
{
    public class Reaction
    {
        [Key]
        public int Id { get; set; }
        public ReactType Type { get; set; }

        public string ?UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

        public int? PostId { get; set; }
        [ForeignKey("PostId")]
        public Post? Post { get; set; }


    }
}
