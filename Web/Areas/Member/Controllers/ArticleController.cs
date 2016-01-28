using MVCStudy.BLL;
using MVCStudy.IBLL;
using MVCStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStudy.Web.Areas.Member.Controllers
{
    public class ArticleController : Controller
    {
        private InterfaceArticleService articleService;
        private InterfaceCommonModelService commonModelService;
        public ArticleController()
        {
            articleService = new ArticleService();
            commonModelService = new CommonModelService();
        }

        public ActionResult Add()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Article article)
        {
            if (ModelState.IsValid)
            {
                //设置固定值
                article.CommonModel.Hits = 0;
                article.CommonModel.Inputer = User.Identity.Name;
                article.CommonModel.Model = "Article";
                article.CommonModel.ReleaseDate = System.DateTime.Now;
                article.CommonModel.Status = 99;
                article = articleService.Add(article);
                if (article.ArticleID > 0)
                {
                    return View("AddSucess", article);
                }
            }
            return View(article);
        }
    }
}