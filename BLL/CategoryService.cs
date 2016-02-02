using MVCStudy.DAL;
using MVCStudy.IBLL;
using MVCStudy.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCStudy.BLL
{
    public class CategoryService:BaseService<Category>,InterfaceCategoryService
    {
        public CategoryService() : base(RepositoryFactory.CategoryRepository) { }

        public List<Models.EasyuiTreeNodeViewModel> EasyuiTreeData(string model)
        {
            //栏目ID列表
            Dictionary<string, int> _categoryIDList = new Dictionary<string, int>();
            //查询栏目列表
            IQueryable<Category> _categoryList = CurrentRepository.Entities.OrderBy(c => c.Order);
            if (!string.IsNullOrEmpty(model)) _categoryList = _categoryList.Where(c => c.Model == model);
            //栏目parentParth
            var _partentParthList = _categoryList.Select(c => c.ParentPath).ToList();
            //遍历partentParth
            foreach (var _partentParth in _partentParthList)
            {   //将partentParth分割为ID字符串列表
                var _strCategoryIDList = _partentParth.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //将CategoryID循环添加到栏目ID列表
                foreach (var _strCategoryID in _strCategoryIDList)
                {
                    if (!_categoryIDList.ContainsKey(_strCategoryID)) _categoryIDList.Add(_strCategoryID, int.Parse(_strCategoryID));
                }
            }
            //栏目树
            List<Models.EasyuiTreeNodeViewModel> _tree = new List<EasyuiTreeNodeViewModel>();
            //树栏目列表
            IQueryable<Category> _categoryTreeList = CurrentRepository.Entities.Where(c => _categoryIDList.Values.Contains(c.ParentId)).OrderByDescending(c => c.ParentPath).ThenBy(c => c.Order);
            //遍历树栏目列表
            foreach (var _categoryTree in _categoryTreeList)
            {
                //树中节点父栏目为当前栏目
                if (_tree.Exists(n => n.parentid == _categoryTree.CategoryID))
                {
                    var _children = _tree.Where(n => n.parentid == _categoryTree.CategoryID).ToList();
                    _tree.RemoveAll(n => n.parentid == _categoryTree.CategoryID);
                    _tree.Add(new EasyuiTreeNodeViewModel() { id = _categoryTree.CategoryID, parentid = _categoryTree.ParentId, text = _categoryTree.Name, children = _children });
                }
                else _tree.Add(new EasyuiTreeNodeViewModel() { id = _categoryTree.CategoryID, parentid = _categoryTree.ParentId, text = _categoryTree.Name });
            }
            return _tree;
        }
    }
}
