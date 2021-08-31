using Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cart: BaseEntity<Guid>
    {
        //--------Relations----CartItem---------|
        public List<CartItem> Items { get; set; }
    }
}
