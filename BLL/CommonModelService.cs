using System;
using System.Linq;
using MVCStudy.Models;
using MVCStudy.DAL;

namespace MVCStudy.BLL
{
    /// <summary>
    /// 公共服务
    /// <remarks>
    /// 创建：2014.02.23
    /// </remarks>
    /// </summary>
    public class CommonModelService: BaseService<CommonModel>, IBLL.InterfaceCommonModelService
    {

        public CommonModelService():base (RepositoryFactory.CommondModelRepository) { }

        public IQueryable<CommonModel> FindList(int number, string model, string title, int categoryID, string inputer, Nullable<DateTime> fromDate, Nullable<DateTime> toDate, int orderCode)
        {
            return FindPageList(out number,1,number,model,title, categoryID, inputer, fromDate, toDate,orderCode);
        }

        public IQueryable<CommonModel> FindPageList(out int totalRecord, int pageIndex, int pageSize, string model, string title, int categoryID, string inputer, Nullable<DateTime> fromDate, Nullable<DateTime> toDate, int orderCode)
        {
            //获取实体列表
            IQueryable<CommonModel> _commonModels = CurrentRepository.Entities;
            if (model == null || model != "All") _commonModels = _commonModels.Where(cm => cm.Model == model);
            if (!string.IsNullOrEmpty(title)) _commonModels = _commonModels.Where(cm => cm.Title.Contains(title));
            if (categoryID > 0) _commonModels = _commonModels.Where(cm => cm.CategoryID == categoryID);
            if (!string.IsNullOrEmpty(inputer)) _commonModels = _commonModels.Where(cm => cm.Inputer == inputer);
            if (fromDate != null) _commonModels = _commonModels.Where(cm => cm.ReleaseDate >= fromDate);
            if (toDate != null) _commonModels = _commonModels.Where(cm => cm.ReleaseDate <= toDate);
            _commonModels = Order(_commonModels, orderCode);
            totalRecord = _commonModels.Count();
            return PageList(_commonModels, pageIndex, pageSize).AsQueryable();
        }

        public IQueryable<CommonModel> Order(IQueryable<CommonModel> entitys, int orderCode)
        {
            switch(orderCode)
            {
                //默认排序
                default:
                    entitys = entitys.OrderByDescending(cm => cm.ReleaseDate);
                    break;
            }
            return entitys;
        }
    }
}
