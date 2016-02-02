using MVCStudy.BLL;
using MVCStudy.IBLL;
using MVCStudy.Models;
using System;
using System.Linq;
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
                article.CommonModel.ReleaseDate = DateTime.Now;
                article.CommonModel.Status = 99;
                article = articleService.Add(article);
                if (article.ArticleID > 0)
                {
                    //附件处理
                    InterfaceAttachmentService _attachmentService = new AttachmentService();
                    //查询相关附件
                    var _attachments = _attachmentService.FindList(null, User.Identity.Name, string.Empty).ToList();
                    //遍历附件
                    foreach (var _att in _attachments)
                    {
                        var _filePath = Url.Content(_att.FileParth);
                        //文章首页图片或内容中使用了该附件则更改ModelID为文章保存后的ModelID
                        if ((article.CommonModel.DefaultPicUrl != null && article.CommonModel.DefaultPicUrl.IndexOf(_filePath) >= 0) || article.Content.IndexOf(_filePath) > 0)
                        {
                            _att.ModelID = article.ModelID;
                            _attachmentService.Update(_att);
                        }
                        //未使用改附件则删除附件和数据库中的记录
                        else
                        {
                            System.IO.File.Delete(Server.MapPath(_att.FileParth));
                            _attachmentService.Delete(_att);
                        }
                    }
                    return View("AddSuccess", article);
                }
            }
            return View(article);
        }

        /// <summary>
        /// 用户导航栏
        /// </summary>
        /// <returns>分部视图</returns>
        public ActionResult Menu()
        {
            return PartialView();
        }

        /// <summary>
        /// 文章列表Json【注意权限问题，普通人员是否可以访问？】
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="input">录入</param>
        /// <param name="category">栏目</param>
        /// <param name="fromDate">日期起</param>
        /// <param name="toDate">日期止</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录</param>
        /// <returns></returns>
        public ActionResult JsonList(string title, string input, int? category, DateTime? fromDate, DateTime? toDate, int pageIndex = 1, int pageSize = 20)
        {
            if (category == null) category = 0;
            int _total;
            var _rows = commonModelService.FindPageList(out _total, pageIndex, pageSize, "Article", title, (int)category, input, fromDate, toDate, 0).Select(
                cm => new Web.Models.CommonModelViewModel()
                {
                    CategoryID = cm.CategoryID,
                    CategoryName = cm.Category.Name,
                    DefaultPicUrl = cm.DefaultPicUrl,
                    Hits = cm.Hits,
                    Inputer = cm.Inputer,
                    Model = cm.Model,
                    ModelID = cm.ModelID,
                    ReleaseDate = cm.ReleaseDate,
                    Status = cm.Status,
                    Title = cm.Title
                });
            return Json(new { total = _total, rows = _rows.ToList() });
        }

        /// <summary>
        /// 全部文章
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
    }
}