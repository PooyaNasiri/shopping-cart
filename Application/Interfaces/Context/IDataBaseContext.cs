using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Context
{
    public interface IDataBaseContext
    {
         DbSet<Product> Producs { get; set; }
         DbSet<Cart> Carts { get; set; }
         DbSet<CartItem> CartItems { get; set; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
    }
}
