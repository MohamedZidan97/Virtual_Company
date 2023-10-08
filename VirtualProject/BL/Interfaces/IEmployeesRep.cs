using VirtualProject.Models;

namespace VirtualProject.BL.Interfaces
{
    public interface IEmployeesRep
    {
        Task<IQueryable<EmployeeVM>> Get();
        Task Add(EmployeeVM emp);
    }
}
