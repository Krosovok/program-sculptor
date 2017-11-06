using System.Collections.Generic;

namespace Model
{
    public class Task
    {
        public Task(
            string taskName, 
            string description, 
            TaskChain chain, 
            IReadOnlyList<ClassFile> givenTypes, 
            IReadOnlyList<ClassFile> tests)
        {
            TaskName = taskName;
            Description = description;
            Chain = chain;
            GivenTypes = givenTypes;
            Tests = tests;
        }

        public string TaskName { get; }
        public string Description { get; }
        public TaskChain Chain { get; }
        public IReadOnlyList<ClassFile> GivenTypes { get; }
        public IReadOnlyList<ClassFile> Tests { get; }
    }
}
