using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public long Price { get; set; }
    }
}
