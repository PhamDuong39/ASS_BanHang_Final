using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;


namespace ASS_BanHang_Final.Services
{
    public class UserService : IUserService
    {
        private ASS_BanHang_DbContext DbContext;
        public UserService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }
        public bool CreateUser(User obj)
        {
            try
            {
                var newUser = new User();
                newUser.Id = obj.Id;
                newUser.Username = obj.Username;
                newUser.RoleId = obj.RoleId;
                newUser.Password = obj.Password;
                newUser.Status = obj.Status;
                DbContext.Users.Add(newUser);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(Guid IdUser)
        {
            try
            {
                var obj = DbContext.Users.FirstOrDefault(p => p.Id == IdUser);
                DbContext.Users.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<User> GetAllUser()
        {
            return DbContext.Users.ToList();
        }

        public User GetUserById(Guid idUser)
        {
            var user = GetAllUser().FirstOrDefault(u => u.Id == idUser);
            return user;
        }

        public List<User> GetUserByName(string name)
        {
            return GetAllUser().Where(p => p.Username.Contains(name)).ToList();
        }

        public bool UpdateUser(User obj)
        {
            try
            {
                var UserUpdated = DbContext.Users.ToList().FirstOrDefault(p => p.Id == obj.Id);
                UserUpdated.Username = obj.Username;
                UserUpdated.Password = obj.Password;
                UserUpdated.RoleId = obj.RoleId;
                UserUpdated.Status = obj.Status;
                DbContext.Users.Update(UserUpdated);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Login(string username, string password)
        {
            var account = DbContext.Users.FirstOrDefault(p => p.Username == username && p.Password == password);
            if (password.Length <= username.Length)
            {
                return false;
            }
            if (account == null || username.Length > password.Length)
            {
                return false;
            }
            else return true;
        }

        public Guid GetRoleIdByUsername(string username)
        {
            var account = DbContext.Users.FirstOrDefault(p => p.Username == username);
            return account.RoleId;
        }

        public Guid GetUserIdByUsername(string username)
        {
            var User = DbContext.Users.FirstOrDefault(p => p.Username == username);

            return User.Id;
            // Chỗ này lấy được ID
        }
    }
}
