using BookStore2022.DataAccess.Repository.IRepository;
using BookStore2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2022.DataAccess.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DeccrementCount(ShoppingCart shoppingCart, int count);


    }
}
