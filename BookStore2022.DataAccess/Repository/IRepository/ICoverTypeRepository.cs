using BookStore2022.DataAccess.Repository.IRepository;
using BookStore2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2022.DataAccess.Repository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {

        void Update(CoverType obj);

       
    }
}
