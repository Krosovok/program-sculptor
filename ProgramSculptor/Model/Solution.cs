using System;
using System.Collections.Generic;

namespace Model
{
    public class Solution
    {
        public Solution(int id, string name, string user, Task task)
        {
            if (task == null || user == null)
            {
                throw new ArgumentException("Arguments can't be null.");
            }
            
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
        public int? BaseSolution { get; }
    }
}
