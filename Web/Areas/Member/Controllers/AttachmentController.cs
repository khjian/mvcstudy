using MVCStudy.BLL;
using MVCStudy.IBLL;
using MVCStudy.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStudy.Web.Areas.Member.Controllers
{
    [Authorize]
    public class AttachmentController : Controller
    {
        private InterfaceAttachmentService attachmentService;

        public AttachmentController()
        {
            attachmentService = new AttachmentService();
        }
        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            var _uploadConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~").GetSection("UploadConfig") as MVCStudy.Models.Config.UploadConfig;
            //文件最大限制
            int _maxSize = _uploadConfig.MaxSize;
            //保存路径
            string _savePath;
            //文件路径
            string _fileParth = "~/" + _uploadConfig.Path + "/";
            //文件名
            string _fileName;
            //扩展名
            string _fileExt;
            //文件类型
            string _dirName;
            //允许上传的类型
            Hashtable extTable = new Hashtable();
            extTable.Add("image", _uploadConfig.ImageExt);
            extTable.Add("flash", _uploadConfig.FileExt);
            extTable.Add("media", _uploadConfig.MediaExt);
            extTable.Add("file", _uploadConfig.FileExt);
            //上传的文件
            HttpPostedFileBase _postFile = Request.Files["imgFile"];
            if (_postFile == null) return Json(new { error = '1', message = "请选择文件" });
            _fileName = _postFile.FileName;
            _fileExt = Path.GetExtension(_fileName).ToLower();
            //文件类型
            _dirName = Request.QueryString["dir"];
            if (string.IsNullOrEmpty(_dirName))
            {
                _dirName = "image";
            }
            if (!extTable.ContainsKey(_dirName)) return Json(new { error = 1, message = "目录类型不存在" });
            //文件大小
            if (_postFile.InputStream == null || _postFile.InputStream.Length > _maxSize) return Json(new { error = 1, message = "文件大小超过限制" });
            //检查扩展名
            if (string.IsNullOrEmpty(_fileExt) || Array.IndexOf(((string)extTable[_dirName]).Split(','), _fileExt.Substring(1).ToLower()) == -1) return Json(new { error = 1, message = "不允许上传此类型的文件。 \n只允许" + ((String)extTable[_dirName]) + "格式。" });
            _fileParth += _dirName + "/" + DateTime.Now.ToString("yyyy-MM") + "/";
            _savePath = Server.MapPath(_fileParth);
            //检查上传目录
            if (!Directory.Exists(_savePath)) Directory.CreateDirectory(_savePath);
            string _newFileName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + _fileExt;
            _savePath += _newFileName;
            _fileParth += _newFileName;
            //保存文件
            _postFile.SaveAs(_savePath);
            //保存数据库记录
            attachmentService.Add(new Attachment() { Extension = _fileExt.Substring(1), FileParth = _fileParth, Owner = User.Identity.Name, UploadDate = DateTime.Now, Type = _dirName });
            return Json(new { error = 0, url = Url.Content(_fileParth) });
        }

        /// <summary>
        /// 附件管理列表
        /// </summary>
        /// <param name="id">公共模型ID</param>
        /// <param name="dir">目录（类型）</param>
        /// <returns></returns>
        public ActionResult FileManagerJson(int? id, string dir)
        {
            Models.AttachmentManagerViewModel _attachmentViewModel;
            IQueryable<Attachment> _attachments;
            //id为null，表示是公共模型id为null，此时查询数据库中没有跟模型对应起来的附件列表（以上传，但上传的文章……还未保存）
            if (id == null) _attachments = attachmentService.FindList(null, User.Identity.Name, dir);
            //id不为null，返回指定模型id和id为null（新上传的）附件了列表
            else _attachments = attachmentService.FindList((int)id, User.Identity.Name, dir, true);
            //循环构造AttachmentManagerViewModel
            var _attachmentList = new List<Models.AttachmentManagerViewModel>(_attachments.Count());
            foreach (var _attachment in _attachments)
            {
                _attachmentViewModel = new Models.AttachmentManagerViewModel() { datetime = _attachment.UploadDate.ToString("yyyy-MM-dd HH:mm:ss"), filetype = _attachment.Extension, has_file = false, is_dir = false, is_photo = _attachment.Type.ToLower() == "image" ? true : false, filename = Url.Content(_attachment.FileParth) };
                FileInfo _fileInfo = new FileInfo(Server.MapPath(_attachment.FileParth));
                _attachmentViewModel.filesize = (int)_fileInfo.Length;
                _attachmentList.Add(_attachmentViewModel);
            }
            return Json(new { moveup_dir_path = "", current_dir_path = "", current_url = "", total_count = _attachmentList.Count, file_list = _attachmentList }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="originalPicture">原图地址</param>
        /// <returns>缩略图地址。生成失败返回null</returns>
        public ActionResult CreateThumbnail(string originalPicture)
        {
            //原图为缩略图直接返回其地址
            if (originalPicture.IndexOf("_s") > 0) return Json(originalPicture);
            //缩略图地址
            string _thumbnail = originalPicture.Insert(originalPicture.LastIndexOf('.'), "_s");
            //创建缩略图
            if (Common.Picture.CreateThumbnail(Server.MapPath(originalPicture), Server.MapPath(_thumbnail), 160, 120))
            {
                //记录保存在数据库中
                attachmentService.Add(new Attachment() { Extension = _thumbnail.Substring(_thumbnail.LastIndexOf('.') + 1), FileParth = "~" + _thumbnail, Owner = User.Identity.Name, Type = "image", UploadDate = DateTime.Now });
                return Json(_thumbnail);
            }
            return Json(null);
        }
    }
}