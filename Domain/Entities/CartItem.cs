using Domain.Entities.Common;
using System;

namespace Domain.Entities
{
    public class CartItem: BaseEntity<Guid>
    {
      //  public long Number { get; set; }
        //-----Relations----Cart---------|
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        //-----Relations----Product-------|
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
