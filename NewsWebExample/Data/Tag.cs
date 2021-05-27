using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.Data
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<NewsToTag> TagNews { get; set; }
    }
}
