﻿using System;
using System.Runtime.Serialization;

namespace ProviderDao
{
    [Serializable]
    internal class DataIntegrityException : Exception
    {
        public DataIntegrityException()
        {
        }

        public DataIntegrityException(string message) : base(message)
        {
        }

        public DataIntegrityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DataIntegrityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}