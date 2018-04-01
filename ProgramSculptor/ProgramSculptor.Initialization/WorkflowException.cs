using System;

namespace ProgramSculptor.Initialization
{
    public class WorkflowException : Exception
    {
        public WorkflowException(string message) : base(message)
        {
            
        }

        public WorkflowException(string message, Exception exception) :base(message, exception)
        {
            
        }
    }
}