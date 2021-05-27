using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.ViewModels
{
    public class NewsCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(6)]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(16)]
        public string Content { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<int> SelectedTags { get; set; }
    }

    public class NewsEditViewModel : NewsCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
