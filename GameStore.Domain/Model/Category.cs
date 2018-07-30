using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Model
{
    public partial class Category
    {
        public Category()
        {

        }
        [Required]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }
    }
}
