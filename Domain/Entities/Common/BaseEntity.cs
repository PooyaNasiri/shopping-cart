using System;

namespace Domain.Entities.Common
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemove { get; set; }
    }
}
