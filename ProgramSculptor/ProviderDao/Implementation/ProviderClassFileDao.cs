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

        public IEnumerable<ClassFile> GetTests(Task task)
        {
            return GetTests(task.TaskName);
        }

        public IEnumerable<ClassFile> GetGivenTypes(Task task)
        {
            return GetGivenTypes(task.TaskName);
        }

        public void AddFileToSolution(Solution target, ClassFile newClassFile)
        {
            string classFileName = newClassFile.FileName;
            InsertIntoDb(target, classFileName);
            SolutionManager.SaveFile(target, classFileName);
        }

        public void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete)
        {
            string classFileName = classFileToDelete.FileName;

            DeleteFromDb(target, classFileName);
            SolutionManager.DeleteFile(target, classFileName);
        }

        private static IEnumerable<FileInfo> GetAllSolutionFiles(Solution solution)
        {
            return FolderContents.GetAllSolutionFiles(
                solution.Task.TaskName,
                solution.Name);
        }

        private static IEnumerable<ClassFile> GetTests(string taskName)
        {
            IEnumerable<FileInfo> allFiles = FolderContents.GetAllTests(taskName);

            return ClassFiles(allFiles);
        }

        private static IEnumerable<ClassFile> GetGivenTypes(string taskName)
        {
            IEnumerable<FileInfo> allFiles = FolderContents.GetAllGivenTypes(taskName);

            return ClassFiles(allFiles);
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