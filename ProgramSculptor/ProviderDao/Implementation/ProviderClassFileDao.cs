using System;
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

        private ProviderClassFileDao()
        {
        }

        internal static IClassFileDao Instance => instance ?? (instance = new ProviderClassFileDao());

        public IEnumerable<ClassFile> SolutionFiles(Solution solution)
        {
            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }

            IEnumerable<FileInfo> allFiles = GetAllSolutionFiles(solution);

            return ClassFiles(allFiles);
        }

        public IEnumerable<ClassFile> OtherSolutionFiles(Solution others)
        {
            if (others == null)
            {
                throw new ArgumentNullException(nameof(others));
            }

            OthersFolderContents folder = new OthersFolderContents(others.Id);
            if (!folder.IsLoaded)
            {
                LoadSolution(folder, others);
            }
            IEnumerable<FileInfo> solutionFiles = folder.OthersSolutionFiles();

            return ClassFiles(solutionFiles);
        }

        public string FileContents(Solution solution, ClassFile classFile)
        {
            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }
            if (classFile == null)
            {
                throw new ArgumentNullException(nameof(classFile));
            }

            return SolutionFileSource(solution, classFile);
        }

        public string OtherSolutionFileContents(Solution solution, ClassFile classFile)
        {
            if (solution == null)
            {
                throw new ArgumentNullException(nameof(solution));
            }
            if (classFile == null)
            {
                throw new ArgumentNullException(nameof(classFile));
            }

            return ReadFile(solution, classFile);
        }

        public string TestFileContents(Task task, ClassFile classFile)
        {
            TaskFileReader reader = new TaskFileReader(task);
            try
            {
                return reader.GetTestSource(classFile.FileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get tests.", e);
            }
        }

        public string GivenTypesFileContents(Task task, ClassFile classFile)
        {
            TaskFileReader reader = new TaskFileReader(task);
            try
            {
                return reader.GetGivenTypeSource(classFile.FileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get given types.", e);
            }
        }

        public IEnumerable<ClassFile> GetTests(Task task)
        {
            try
            {
                IEnumerable<FileInfo> allFiles = new TaskFolderContents(task)
                    .GetAllTests();
                return ClassFiles(allFiles);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get tests.", e);
            }
        }

        public IEnumerable<ClassFile> GetGivenTypes(Task task)
        {
            try
            {
                IEnumerable<FileInfo> allFiles = new TaskFolderContents(task)
                    .GetAllGivenTypes();
                return ClassFiles(allFiles);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get given types.", e);
            }
        }

        public void AddFileToSolution(Solution target, ClassFile newClassFile)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (newClassFile == null)
            {
                throw new ArgumentNullException(nameof(newClassFile));
            }

            AddFile(target, newClassFile);
        }

        public void UpdateFileContents(Solution target, ClassFile classFile, string contents)
        {
            SolutionFileWriter solutionFileWriter = new SolutionFileWriter(target);
            try
            {
                solutionFileWriter.WriteToFile(classFile, contents);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't update file.", e);
            }
        }

        public void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (classFileToDelete == null)
            {
                throw new ArgumentNullException(nameof(classFileToDelete));
            }

            DeleteFile(target, classFileToDelete);
        }
        
        private static IEnumerable<FileInfo> GetAllSolutionFiles(Solution solution)
        {
            try
            {
                return new TaskFolderContents(solution.Task)
                    .GetAllSolutionFiles(solution);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get Solution files.", e);
            }
        }

        private static string SolutionFileSource(Solution solution, ClassFile classFile)
        {
            TaskFileReader reader = new TaskFileReader(solution.Task);

            try
            {
                return reader.GetSolutionFileSource(solution.Name, classFile.FileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get solution file contents.", e);
            }
        }

        private static string ReadFile(Solution solution, ClassFile classFile)
        {
            OthersFolderContents folderContents = new OthersFolderContents(solution.Id);

            try
            {
                return folderContents.ReadFile(classFile.FileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Can't get other's solution file contents.", e);
            }
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

        private static void AddFile(Solution target, ClassFile newClassFile)
        {
            string classFileName = newClassFile.FileName;
            try
            {
                InsertIntoDb(target, classFileName);
                SolutionManager.CreateFile(target, classFileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Failed to save file.", e);
            }
            catch (DbException e)
            {
                throw new DataAccessException("Failed to save calss file into DB.", e);
            }
        }

        private static void DeleteFile(Solution target, ClassFile classFileToDelete)
        {
            string classFileName = classFileToDelete.FileName;
            try
            {
                DeleteFromDb(target, classFileName);
                SolutionManager.DeleteFile(target, classFileName);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Failed to delete a file.", e);
            }
            catch (DbException e)
            {
                throw new DataAccessException("Failed to delete calss file from DB.", e);
            }
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
            Db db = Db.Instance;
            command.Parameters.AddRange(new DbParameter[]
            {
                db.CreateParameter(target.Id, DbType.Int32, Parameters[0]),
                db.CreateParameter(fileName, DbType.String, Parameters[1])
            });

            db.TryExecuteNonQuery(command);
        }

        private static void LoadSolution(OthersFolderContents folder, Solution others)
        {
            // TODO: in future, it will loaded from server, now from local db and file system.

            IEnumerable<FileInfo> solutionFiles = GetAllSolutionFiles(others);

            Dictionary<string, string> fileNamesToContents = GetFileNamesToContents(solutionFiles);

            LoadFiles(folder, fileNamesToContents);
        }

        private static Dictionary<string, string> GetFileNamesToContents(IEnumerable<FileInfo> solutionFiles)
        {
            try
            {
                return solutionFiles.ToDictionary(
                    file => file.Name,
                    file => File.ReadAllText(file.FullName));
            }
            catch (SystemException e)
            {
                throw new DataAccessException("Failed to get other's solution files.", e);
            }
        }

        private static void LoadFiles(OthersFolderContents folder, Dictionary<string, string> fileNamesToContents)
        {
            try
            {
                folder.LoadFiles(fileNamesToContents);
            }
            catch (FileSystemException e)
            {
                throw new DataAccessException("Error during Solution loading.", e);
            }
        }
    }
}