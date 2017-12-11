using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface IClassFileDao
    {
        // TODO: Get files from others solutions? 
        
        IEnumerable<ClassFile> SolutionFiles(Solution solution);
        
        void AddFileToSolution(Solution target, ClassFile newClassFile);

        void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete);

        IEnumerable<ClassFile> GetTests(Task task);

        IEnumerable<ClassFile> GetGivenTypes(Task task);
    }
}