using System;
using System.Collections.Generic;

namespace Model
{
    public class Solution
    {
        private readonly List<ClassFile> files = new List<ClassFile>();

        public Solution(int id, string name, string user, Task task)
        {
            Id = id;
            Name = name;
            User = user;
            Task = task;
        }
        
        public Solution(int id, string name, string user, Task task, int? baseSolutionId)
            : this(id, name, user, task)
        {
            BaseSolution = baseSolutionId;
        }

        public int Id { get; }
        public string Name { get; }
        public string User { get; }
        public Task Task { get; }
        public IReadOnlyList<ClassFile> Files => files.AsReadOnly();
        public int? BaseSolution { get; }
        
        public void AddFile(ClassFile newFile) => files.Add(newFile);
        public void RemoveFile(ClassFile fileToRemove) => files.Remove(fileToRemove);
    }
}
