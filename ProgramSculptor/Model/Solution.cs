using System;
using System.Collections.Generic;

namespace Model
{
    public class Solution
    {
        private readonly List<ClassFile> files = new List<ClassFile>();

        public Solution(string user, Task task)
        {
            User = user;
            Task = task;
        }
        
        public Solution(string user, Task task, Solution baseSolution)
            : this(user, task)
        {
            if (!baseSolution.Task.Equals(task.InChain().Previous))
            {
                throw new ArgumentException("Base solution of not base task.", nameof(baseSolution));
            }
            
            BaseSolution = baseSolution;
        }

        public string User { get; }
        public Task Task { get; }
        public IReadOnlyList<ClassFile> Files => files.AsReadOnly();
        public Solution BaseSolution { get; }
        
        
        public void AddFile(ClassFile newFile) => files.Add(newFile);
        public void RemoveFile(ClassFile fileToRemove) => files.Remove(fileToRemove);
    }
}
