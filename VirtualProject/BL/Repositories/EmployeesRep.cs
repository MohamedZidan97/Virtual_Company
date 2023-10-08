using VirtualProject.BL.Interfaces;
using VirtualProject.DAL;
using VirtualProject.DAL.Entities;
using VirtualProject.Models;

namespace VirtualProject.BL.Repositories
{
    public class EmployeesRep : IEmployeesRep
    {
        private readonly ApplicationDbContext db;

        public EmployeesRep(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IQueryable<EmployeeVM>> Get()
        {
            var data = db.Employees.Select(e => new EmployeeVM
            {
                Id = e.Id,
                Name = e.Name,
                Salary = e.Salary,
                Address = e.Address,
                Note = e.Note,
                IsActive = e.IsActive,
                Email = e.Email,
                HireDate = e.HireDate,
            });

            return data;
        }
        public async Task Add(EmployeeVM emp)
        {
            Employee e = new Employee();
            e.Email = emp.Email;
            e.Salary = emp.Salary;
            e.Address = emp.Address;
            e.Note = emp.Note;
            e.IsActive = emp.IsActive;
            e.HireDate = emp.HireDate;
            e.Name = emp.Name;

            db.Employees.Add(e);
            db.SaveChanges();


        }
      
    }
}
