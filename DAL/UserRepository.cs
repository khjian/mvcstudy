using MVCStudy.IDAL;
using MVCStudy.Models;

namespace MVCStudy.DAL
{
    public class UserRepository:BaseRepository<User>, InterfaceUserRepository
    {
    }
}
