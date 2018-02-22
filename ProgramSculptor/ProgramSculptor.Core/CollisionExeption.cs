using System;

namespace ProgramSculptor.Core
{
    public class CollisionExeption : Exception
    {
        public CollisionExeption(string message) : base(message) { }
    }
}