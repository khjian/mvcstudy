using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MVCStudy.DAL
{
    /// <summary>
    /// 上下文工厂
    /// </summary>
    public class ContextFactory
    {
        /// 获取当前数据上下文
        /// </summary>
        /// <returns></returns>
        public static MyDbContext GetCurrentContext()
        {
            MyDbContext _nContext = CallContext.GetData("MyDbContext") as MyDbContext;
            if (_nContext == null)
            {
                _nContext = new MyDbContext();
                CallContext.SetData("MyDbContext", _nContext);
            }
            return _nContext;
        }
    }
}
