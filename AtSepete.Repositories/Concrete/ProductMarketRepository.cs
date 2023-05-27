using AtSepete.Core.GenericRepository;
using AtSepete.DataAccess.Context;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class ProductMarketRepository:EFBaseRepository<ProductMarket>,IProductMarketRepository
    {
        public ProductMarketRepository(AtSepeteDbContext Context):base(Context) 
        {

        }
        public async Task<IQueryable<ProductMarket>> GetAllQueryableAsync()//listede select yazabilmemizi sağlar IQueryable
        {
            return GetAllActives().AsQueryable();
        }
    }
}
