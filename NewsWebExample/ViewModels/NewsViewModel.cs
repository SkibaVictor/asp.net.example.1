using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public DateTime NewsDate { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
