using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using DataAccessInterfaces;
using FileManaging;
using Model;

namespace ProviderDao.Implementation
{
    public class ProviderClassFileDao : IClassFileDao
    {
        private const string InsertSqlKey = "ProviderClassFileDao.AddFileToSolution";
        private const string DeleteSqlKey = "ProviderClassFileDao.DeleteFileFromSolution";

        private static readonly string[] Parameters = new string[]
        {
            Db.Instance.Param(Db.Solutions.Id),
            Db.Instance.Param(Db.CodeFiles.Name)
        };
        private static IClassFileDao instance;
        
        private ProviderClassFileDao() { }
        
        internal static IClassFileDao Instance => instance ?? (instance = new ProviderClassFileDao());

        public IEnumerable<ClassFile> SolutionFiles(Solution solution)
        {
            IEnumerable<FileInfo> allFiles = GetAllSolutionFiles(solution);

            return ClassFiles(allFiles);
        }

        public string FileContents(Solution solution, ClassFile classFile)
        {
            FileReader reader = new FileReader(solution.Task);
            return reader.GetSolutionFileSource(solution.Name, classFile.FileName);
        }

        public IEnumerable<ClassFile> GetTests(Task task)
        {
            IEnumerable<FileInfo> allFiles = new FolderContents(task)
                .GetAllTests();

            return ClassFiles(allFiles);
        }

        public IEnumerable<ClassFile> GetGivenTypes(Task task)
        {
            IEnumerable<FileInfo> allFiles = new FolderContents(task)
                .GetAllGivenTypes();

            return ClassFiles(allFiles);
        }

        public void AddFileToSolution(Solution target, ClassFile newClassFile)
        {
            string classFileName = newClassFile.FileName;
            InsertIntoDb(target, classFileName);
            SolutionManager.CreateFile(target, classFileName);
        }

        public void UpdateFileContents(Solution target, ClassFile classFile, string contents)
        {
            FileWriter fileWriter = new FileWriter(target);
            fileWriter.WriteToFile(classFile, contents);
        }

        public void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete)
        {
            string classFileName = classFileToDelete.FileName;

            DeleteFromDb(target, classFileName);
            SolutionManager.DeleteFile(target, classFileName);
        }

        // TODO: Make paramter null checks in dao!!! 
        private static IEnumerable<FileInfo> GetAllSolutionFiles(Solution solution)
        {
            return new FolderContents(solution.Task)
                .GetAllSolutionFiles(solution);
        }

        private static IEnumerable<ClassFile> ClassFiles(IEnumerable<FileInfo> allFiles)
        {
            return allFiles.Select(NewClassFile);
        }

        private static ClassFile NewClassFile(FileSystemInfo codeFile)
        {
            string name = codeFile.Name;
            return new ClassFile(name);
        }

        private static void InsertIntoDb(Solution target, string fileName)
        {
            DbCommand insertProcedure = Db.Instance.CreatePrecedureCommand(InsertSqlKey);
            ExecuteWithParameters(insertProcedure, target, fileName);
        }

        private static void DeleteFromDb(Solution target, string fileName)
        {
            Db db = Db.Instance;
            DbCommand delete = db.CreateTextCommand(DeleteSqlKey, Parameters);
            ExecuteWithParameters(delete, target, fileName);
        }

        private static void ExecuteWithParameters(DbCommand command, Solution target, string fileName)
        {
            command.Parameters.AddRange(new DbParameter[]
            {
                Db.Instance.CreateParameter(target.Id, DbType.Int32, Parameters[0]),
                Db.Instance.CreateParameter(fileName, DbType.String, Parameters[1])
            });

            command.ExecuteNonQuery();
            Db.Instance.CloseCommand(command);
        }
    }
}