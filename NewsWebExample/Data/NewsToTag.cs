using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.Data
{
    public class NewsToTag : BaseEntity
    {
        public News News { get; set; }
        public Tag Tag { get; set; }
    }
}
