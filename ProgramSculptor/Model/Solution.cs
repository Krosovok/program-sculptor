using System;
using System.Collections.Generic;

namespace Model
{
    public class Solution
    {
        public const int NoId = -1;
        
        private int id;

        public Solution(string name, string user, Task task)
            : this(NoId, name, user, task) { }

        public Solution(int id, string name, string user, Task task)
            : this(id, name, user, task, null) { }

        public Solution(string name, string user, Task task, int? baseSolutionId)
            : this(NoId, name, user, task, baseSolutionId) { }
        
        public Solution(int id, string name, string user, Task task, int? baseSolutionId)
        {
            if (task == null || user == null)
            {
                throw new ArgumentException("Solution task and user can't be null.");
            }
            
            this.id = id;
            Name = name;
            User = user;
            Task = task;
            BaseSolution = baseSolutionId;
        }
        
        public int Id
        {
            get { return id; }
            set
            {
                if (id != NoId)
                {
                    throw new ArgumentException("Assigning Id to not new Solution.");
                }
                id = value;
            }
        }

        public string Name { get; }
        public string User { get; }
        public Task Task { get; }
        public int? BaseSolution { get; }
    }
}