using Microsoft.EntityFrameworkCore;
using VirtualProject.BL.Helper;
using VirtualProject.BL.Interfaces;
using VirtualProject.DAL;
using VirtualProject.DAL.Entities.Hr;
using VirtualProject.Models.Hr;

namespace VirtualProject.BL.Repositories
{
    public class HrRep : IHrRep
    {
        private readonly ApplicationDbContext db;

        public HrRep(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddTask(AddTasksVM addTasksVM)
        {
            var check = db.hrs.Any(e => e.Hrr == addTasksVM.HrId);

            if (!check)
            {
                HrEntity hr = new HrEntity { Hrr = addTasksVM.HrId };
                db.hrs.Add(hr);
                await db.SaveChangesAsync();
            }
            TaskHr taskHr = new TaskHr
            {
                HrId = addTasksVM.HrId,
                Description = addTasksVM.Description,
                CvName = UploadFiles.SaveFile(addTasksVM.CvUrl, "Decoments"),
                Name = addTasksVM.Name
            };
            db.tasks.Add(taskHr);
            await db.SaveChangesAsync();
        }

        public async Task<IQueryable<AddTasksVM>> GetTasks(string HrId)
        {
            var task = db.tasks.Select(e => new AddTasksVM
            {
                CvName = e.CvName,
                Name = e.Name,
                Description = e.Description,
                HrId = e.HrId,
                Done = e.Done,
                Id = e.Id




            }).Where(h => h.HrId == HrId && h.Done.Equals(false));

            return task;
        }

        public async Task DoneToTask(int TaskId)
        {
            var t = await db.tasks.FirstOrDefaultAsync(e => e.Id == TaskId);
            if (t == null) { return; }
            t.Done = true;
           
          await db.SaveChangesAsync();

        }
    }
}
