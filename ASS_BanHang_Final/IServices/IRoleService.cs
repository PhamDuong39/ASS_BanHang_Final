using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.IServices
{
    public interface IRoleService
    {
        public bool CreateRole(Role obj);
        public bool DeleteRole(Guid RoleId);
        public bool UpdateRole(Role Obj);
        public List<Role> GetAllRoles();
        public Role GetRoleById(Guid roleId);
        public List<Role> GetRoleByName(string roleName);
    }
}
