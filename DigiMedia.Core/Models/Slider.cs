using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiMedia.Core.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Profession { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped] 
        public IFormFile? ImageFile { get; set; }


    }
}
