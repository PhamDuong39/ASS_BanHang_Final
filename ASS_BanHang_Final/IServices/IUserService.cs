using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.IServices
{
    public interface IUserService
    {
        public bool CreateUser(User obj);
        public bool UpdateUser(User obj);
        public bool DeleteUser(Guid IdUser);
        public User GetUserById(Guid idUser);
        public List<User> GetUserByName(string name);
        public List<User> GetAllUser();

        public bool Login(string username, string password);
        
        public Guid GetRoleIdByUsername(string username);


        public Guid GetUserIdByUsername(string username);
    }
}