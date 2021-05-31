using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.Data
{
    public class Attachment : BaseEntity
    {
        public new Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
