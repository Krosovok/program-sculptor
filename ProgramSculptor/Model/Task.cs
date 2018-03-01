namespace Model
{
    public class Task
    {
        
        public Task(
            int id,
            string taskName, 
            string description)
        {
            Id = id;
            TaskName = taskName;
            Description = description;
            Chain = new TaskChain(this);
        }

        public int Id { get; }
        public string TaskName { get; }
        public string Description { get; }
        public TaskChain Chain { get; internal set; }
    }
}
