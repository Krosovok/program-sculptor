using System.Collections.Generic;

namespace Model
{
    public class Task
    {
        public Task(
            string taskName, 
            string description,
            IReadOnlyList<ClassFile> givenTypes, 
            IReadOnlyList<ClassFile> tests)
        {
            TaskName = taskName;
            Description = description;
            GivenTypes = givenTypes;
            Tests = tests;
        }

        public string TaskName { get; }
        public string Description { get; }
        public IReadOnlyList<ClassFile> GivenTypes { get; }
        public IReadOnlyList<ClassFile> Tests { get; }
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
