using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Repositories.Concrete
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AtSepeteDbContext Context):base(Context) 
        {
           
        }

    }
}
