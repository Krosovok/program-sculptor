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
        }

        public int Id { get; }
        public string TaskName { get; }
        public string Description { get; }
        
        private TaskChain Chain { get; set; }

        public TaskChainPosition InChain => Chain?.PositionOf(this);
        
        internal void AddToChain(TaskChain chain)
        {
            this.Chain = chain;
        }
    }
}
