using MVCStudy.BLL;
using MVCStudy.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStudy.Web.Areas.Member.Controllers
{
    public class CategoryController : Controller
    {
        private InterfaceCategoryService categoryRepository;
        public CategoryController()
        {
            categoryRepository = new CategoryService();
        }

        /// <summary>
        /// 获取json格式栏目树
        /// </summary>
        /// <param name="model">模型名</param>
        /// <returns></returns>
        public ActionResult JsonTree(string model)
        {
            return Json(categoryRepository.EasyuiTreeData(model), JsonRequestBehavior.AllowGet);
        }
    }
}