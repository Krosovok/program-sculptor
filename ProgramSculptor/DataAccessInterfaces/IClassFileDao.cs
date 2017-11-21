using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface IClassFileDao
    {
        IReadOnlyList<ClassFile> SolutionFiles(Solution solution);
        
        void AddClassFile(ClassFile newClassFile);

        void DeleteClassFile(ClassFile classFileToDelete);
    }
}