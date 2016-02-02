using System;
using System.Linq;
using MVCStudy.Models;
using MVCStudy.IBLL;
using MVCStudy.DAL;

namespace MVCStudy.BLL
{
    /// <summary>
    /// 附件服务
    /// <remarks>
    /// 创建：2014.03.05
    /// </remarks>
    /// </summary>
    public class AttachmentService:BaseService<Attachment>,InterfaceAttachmentService
    {
        public AttachmentService() : base(RepositoryFactory.AttachmentRepository) { }

        public IQueryable<Models.Attachment> FindList(int? modelID, string owner, string type)
        {
            var _attachemts = CurrentRepository.Entities.Where(a => a.ModelID == modelID);
            if (!string.IsNullOrEmpty(owner)) _attachemts = _attachemts.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachemts = _attachemts.Where(a => a.Type == type);
            return _attachemts;
        }

        public IQueryable<Models.Attachment> FindList(int modelID, string owner, string type, bool withModelIDNull)
        {
            var _attachemts = CurrentRepository.Entities;
            if (withModelIDNull) _attachemts = _attachemts.Where(a => a.ModelID == modelID || a.ModelID == null);
            else _attachemts = _attachemts.Where(a => a.ModelID == modelID);
            if (!string.IsNullOrEmpty(owner)) _attachemts = _attachemts.Where(a => a.Owner == owner);
            if (!string.IsNullOrEmpty(type)) _attachemts = _attachemts.Where(a => a.Type == type);
            return _attachemts;
        }
    }
}
