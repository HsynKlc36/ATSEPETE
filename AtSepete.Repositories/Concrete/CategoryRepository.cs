using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Core.CoreInterfaces;
using AtSepete.Core.GenericRepository;
using AtSepete.DataAccess.Context;

namespace AtSepete.Repositories.Concrete
{
    public class CategoryRepository: EFBaseRepository<Category> ,ICategoryRepository
    {
        public CategoryRepository(AtSepeteDbContext context) : base(context) { }
       


    }
}
