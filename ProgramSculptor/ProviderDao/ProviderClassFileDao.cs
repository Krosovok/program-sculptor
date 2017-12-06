using System.Collections.Generic;
using DataAccessInterfaces;
using Model;

namespace ProviderDao
{
    public class ProviderClassFileDao : IClassFileDao
    {
        // Is it needed at all?
        
        public IReadOnlyList<ClassFile> SolutionFiles(Solution solution)
        {
            throw new System.NotImplementedException();
        }

        public void AddClassFile(ClassFile newClassFile)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteClassFile(ClassFile classFileToDelete)
        {
            throw new System.NotImplementedException();
        }
    }
}