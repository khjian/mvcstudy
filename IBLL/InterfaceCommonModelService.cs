using MVCStudy.Models;
using System;
using System.Linq;

namespace MVCStudy.IBLL
{
    public interface InterfaceCommonModelService:InterfaceBaseService<CommonModel>
    {
        // <summary>
        /// 排序
        /// </summary>
        /// <param name="entitys">数据实体集</param>
        /// <param name="roderCode">排序代码[默认：ID降序]</param>
        /// <returns></returns>
        IQueryable<CommonModel> Order(IQueryable<CommonModel> entitys, int roderCode);

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="number">返回记录数量</param>
        /// <param name="model">模型【All全部】</param>
        /// <param name="title">标题</param>
        /// <param name="categoryID">栏目ID【不使用设0】</param>
        /// <param name="inputer">录入者【不使用设置空字符串】</param>
        /// <param name="fromDate">起始日期【可为null】</param>
        /// <param name="toDate">截止日期【可为null】</param>
        /// <param name="orderCode">排序码</param>
        /// <returns>数据列表</returns>
        IQueryable<CommonModel> FindList(int number, string model, string title, int categoryID, string inputer, DateTime? fromDate, DateTime? toDate, int orderCode);


        /// <summary>
        /// 查询分页数据列表
        /// </summary>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="model">模型【All全部】</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="title">标题【不使用设置空字符串】</param>
        /// <param name="categoryID">栏目ID【不使用设0】</param>
        /// <param name="inputer">用户名【不使用设置空字符串】</param>
        /// <param name="fromDate">起始日期【可为null】</param>
        /// <param name="toDate">截止日期【可为null】</param>
        /// <param name="orderCode">排序码</param>
        /// <returns>分页数据列表</returns>
        IQueryable<CommonModel> FindPageList(out int totalRecord, int pageIndex, int pageSize, string model, string title, int categoryID, string inputer, DateTime? fromDate, DateTime? toDate, int orderCode);
    }
}
