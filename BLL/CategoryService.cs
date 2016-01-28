using MVCStudy.DAL;
using MVCStudy.IBLL;
using MVCStudy.Models;

namespace MVCStudy.BLL
{
    public class CategoryService:BaseService<Category>,InterfaceCategoryService
    {
        public CategoryService() : base(RepositoryFactory.CategoryRepository) { }
    }
}
