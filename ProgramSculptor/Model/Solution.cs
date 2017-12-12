using System;
using System.Collections.Generic;

namespace Model
{
    public class Solution
    {
        public const int NoId = -1;
        
        private int id;

        public Solution(string name, string user, Task task)
        {
            if (task == null || user == null)
            {
                throw new ArgumentException("Arguments can't be null.");
            }

            id = NoId;
            Name = name;
            User = user;
            Task = task;
        }

        public Solution(int id, string name, string user, Task task)
            : this(name, user, task)
        {
            this.id = id;
        }

        public Solution(int id, string name, string user, Task task, int? baseSolutionId)
            : this(id, name, user, task)
        {
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