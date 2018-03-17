using System;
using System.Collections.Generic;
using Model;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Data access interface for obtaining data about files in given types, tests and solutions,
    /// and their content.
    /// </summary>
    public interface IClassFileDao
    {
        /// <summary>
        /// Returns files from given Solution identifying it be Name. 
        /// </summary>
        /// <param name="solution">Solution which files should be returned.</param>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        IEnumerable<ClassFile> SolutionFiles(Solution solution);

        /// <summary>
        /// Returns a list of files in given solution of other person identified by it's id in the outer system.
        /// </summary>
        /// <param name="others">Solution which files should be returned.</param>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        IEnumerable<ClassFile> OtherSolutionFiles(Solution others);

        /// <summary>
        /// Returns contents of a file in the solution.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        string FileContents(Solution solution, ClassFile classFile);

        /// <summary>
        /// Returns contents of a file in a solution of other person.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        string OtherSolutionFileContents(Solution solution, ClassFile classFile);

        /// <summary>
        /// Returns contents of a test file of a task.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        string TestFileContents(Task task, ClassFile classFile);
        
        /// <summary>
        /// Returns contents of a file with given type.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        string GivenTypesFileContents(Task task, ClassFile classFile);

        /// <summary>
        /// Add a file to a solution: in db and in file system.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        void AddFileToSolution(Solution target, ClassFile newClassFile);

        /// <summary>
        /// Changes contents of a given solution file, overwrighting it's old contents.
        /// </summary>
        /// <param name="target">Solution containg given file.</param>
        /// <param name="classFile">A data about file to change.</param>
        /// <param name="contents">Text that will be written to a file.</param>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        void UpdateFileContents(Solution target, ClassFile classFile, string contents);

        /// <summary>
        /// Deletes given file from a solution: in db and in file system.
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        void DeleteFileFromSolution(Solution target, ClassFile classFileToDelete);

        /// <summary>
        /// Returns all test files from a given task. 
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        IEnumerable<ClassFile> GetTests(Task task);

        /// <summary>
        /// Returns all given types files from a given task. 
        /// </summary>
        /// <exception cref="DataAccessException">Occurs when there is an error accessing to data or in manipulatuing them.</exception>
        /// <exception cref="ArgumentNullException">One of the parameters is null.</exception>
        IEnumerable<ClassFile> GetGivenTypes(Task task);
    }
}