using VirtualProject.Models.Hr;

namespace VirtualProject.BL.Interfaces
{
    public interface IHrRep
    {
        Task AddTask(AddTasksVM addTasksVM);
        Task<IQueryable<AddTasksVM>> GetTasks(string HrId);

        Task DoneToTask(int TaskId);
    }
}
