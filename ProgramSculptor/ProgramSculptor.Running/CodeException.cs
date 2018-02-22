using System;

namespace ProgramSculptor.Running
{
    public class CodeException : Exception
    {
        public CodeException(string message) : base(message) { }
    }
}