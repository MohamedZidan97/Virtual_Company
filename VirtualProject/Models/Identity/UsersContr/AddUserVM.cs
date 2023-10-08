namespace VirtualProject.Models.Identity.UsersContr
{
    public class AddUserVM :RegisterVM
    {
        public List<RolesVM> Roles { get; set; }
    }
}
