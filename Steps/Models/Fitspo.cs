using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Steps.Models
{
    public class Fitspo
    {
        public Fitspo()
        {
        }
        public int Id { get; set; }
        public DateTime DateOfPost { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "You need to enter a title")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Title needs to be beteen 3 and 60 characters long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need to enter a blurb")]
        [StringLength(128, MinimumLength = 5, ErrorMessage = "Blurb needs to be beteen 5 and 128 characters long")]
        [DisplayName("Blurb (short recap)")]
        public string Blurb { get; set; }
        [Required(ErrorMessage = "You need to enter the post")]
        [MinLength(60, ErrorMessage = "The post needs to be beteen 60 and 1500 characters long")]
        [DisplayName("Full Post")]
        public string Post { get; set; }        
        public byte[] Image { get; set; }

        [Required(ErrorMessage = "You need to add an image")]
        [Display(Name = "Post Image")]
        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
