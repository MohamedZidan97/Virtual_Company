namespace VirtualProject.Models.Identity.UsersContr
{
    public class UserRolesVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RolesVM> Roles { get; set; }
    }
}
