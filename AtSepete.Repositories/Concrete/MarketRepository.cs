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
    public class MarketRepository:EFBaseRepository<Market>, IMarketRepository
    {
        public MarketRepository(AtSepeteDbContext Context):base(Context)
        {

        }
    }
}
