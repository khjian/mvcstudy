using MVCStudy.IDAL;

namespace MVCStudy.DAL
{
    public static class RepositoryFactory
    {
        /// <summary>
        /// 文章仓储
        /// </summary>
        public static InterfaceArticleRepository ArticleRepository { get { return new ArticleRepository(); } }

        /// <summary>
        /// 附件仓储
        /// </summary>
        public static InterfaceAttachmentRepository AttachmentRepository { get { return new AttachmentRepository(); } }

        /// <summary>
        /// 栏目仓储
        /// </summary>
        public static InterfaceCategoryRepository CategoryRepository { get { return new CategoryRepository(); } }

        /// <summary>
        /// 用户仓储
        /// </summary>
        public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }


        /// <summary>
        /// 公共模型仓储
        /// </summary>
        public static InterfaceCommonModelRepository CommondModelRepository { get { return new CommonModelRepository(); } }
    }
}
