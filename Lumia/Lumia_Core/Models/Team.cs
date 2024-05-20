using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumia_Core.Models
{
    public class Team: BaseEntity
    {
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Fullname { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Position { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }=null!;
    }
}
