using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.ViewModels
{
    public class NewsShortViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
    public class NewsIndexViewModel
    {
        public List<NewsShortViewModel> News { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public int? CurrentTag { get; set; }
    }
}
