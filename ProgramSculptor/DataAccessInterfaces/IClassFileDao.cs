using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    public interface IClassFileDao
    {
        // TODO: Get files from others solutions? 
        
        // TODO: For all DAOs, create a thrown exception in methods????
        
        IEnumerable<ClassFile> SolutionFiles(Solution solution);

        string FileContents(Solution solution, ClassFile classFile);

        string TestFileContents(Task task, ClassFile classFile);
        
        string GivenTypesFileContents(Task task, ClassFile classFile);
        
        void AddFileToSolution(Solution target, ClassFile newClassFile);

        void UpdateFileContents(Solution target, ClassFile classFile, string contents);

        void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete);

        IEnumerable<ClassFile> GetTests(Task task);

        IEnumerable<ClassFile> GetGivenTypes(Task task);
    }
}