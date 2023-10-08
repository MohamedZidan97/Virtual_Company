namespace VirtualProject.DAL.Entities.Hr
{
    public class TaskHr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CvName { get; set; }
        public string HrId { get; set; }
        public HrEntity Hr { get; set; }
        public bool Done { get; set; } = false;
    }
}
