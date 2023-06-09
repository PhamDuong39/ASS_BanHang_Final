using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;


namespace ASS_BanHang_Final.Services
{
    public class RoleService : IRoleService
    {
        private ASS_BanHang_DbContext DbContext;
        public RoleService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }
        public bool CreateRole(Role obj)
        {
            try
            {
                DbContext.Roles.Add(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool DeleteRole(Guid RoleId)
        {
            try
            {
                var obj = DbContext.Roles.FirstOrDefault(p => p.Id == RoleId);
                DbContext.Roles.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Role> GetAllRoles()
        {
            return DbContext.Roles.ToList();
        }

        public Role GetRoleById(Guid roleId)
        {
            var obj = DbContext.Roles.ToList().FirstOrDefault(p => p.Id == roleId);
            return obj;
        }

        public List<Role> GetRoleByName(string roleName)
        {
            return DbContext.Roles.ToList().Where(p => p.RoleName.ToLower().Contains(roleName.ToLower())).ToList();
        }

        public bool UpdateRole(Role Obj)
        {
            try
            {
                var RoleUpdated = DbContext.Roles.ToList().FirstOrDefault(p => p.Id == Obj.Id);
                RoleUpdated.Status = Obj.Status;
                RoleUpdated.Description = Obj.Description;
                RoleUpdated.RoleName = Obj.RoleName;
                DbContext.Roles.Update(RoleUpdated);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
