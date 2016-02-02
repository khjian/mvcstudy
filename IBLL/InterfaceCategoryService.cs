using MVCStudy.Models;
using System.Collections.Generic;

namespace MVCStudy.IBLL
{
    public interface InterfaceCategoryService:InterfaceBaseService<Category>
    {
        /// <summary>
        /// 获取easyuiTree数据
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns></returns>
        List<Models.EasyuiTreeNodeViewModel> EasyuiTreeData(string model);
    }
}
