using GameStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Model
{
    public partial class Review
    {
        public Review()
        {

        }
        [Required]
        public int ReviewId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comments { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser User { get; set; }
    }
}
