using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemove { get; set; }
    }
}
