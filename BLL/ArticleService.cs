using MVCStudy.DAL;
using MVCStudy.IBLL;
using MVCStudy.Models;

namespace MVCStudy.BLL
{
    public class ArticleService:BaseService<Article>, InterfaceArticleService
    {
        public ArticleService() : base(RepositoryFactory.ArticleRepository) { }
    }
}
