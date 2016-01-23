using MVCStudy.IDAL;

namespace MVCStudy.DAL
{
    public static class RepositoryFactory
    {
        /// <summary>
        /// 用户仓储
        /// </summary>
        public static InterfaceUserRepository UserRepository { get { return new UserRepository(); } }
    }
}
