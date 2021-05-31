using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NewsWebExample.ViewModels
{
    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(16)]
        public string Name { get; set; }

        public IFormFile File { get; set; }
    }

    public class TagEditViewModel : TagCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
