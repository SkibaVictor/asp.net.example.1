using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWebExample.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
    }
}
