using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.Data
{
    public class News : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public List<NewsToTag> NewsTags { get; set; }
    }
}
