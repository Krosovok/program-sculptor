using System.Collections.Generic;
using System.Collections.Specialized;

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

        public string TaskName { get; }
        public string Description { get; }
        private int Id { get; }

        public IEnumerable<ClassFile> GivenTypes => ClassFile.GetGivenTypes(TaskName);

        public IEnumerable<ClassFile> Tests => ClassFile.GetTests(TaskName);

        private TaskChain Chain { get; set; }

        public TaskChainPosition InChain()
        {
            return Chain?.PositionOf(this);
        }
        
        internal void AddToChain(TaskChain chain)
        {
            this.Chain = chain;
        }
    }
}
