using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataAccessInterfaces;
using Model;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Running;
using Services;

namespace ViewModel.Core
{
    public class ModelInitialization : IWorkflowStep
    {
        private const string ZeroTypesMessage = "There should be at least one type!";
        
        private readonly IMessageService messageService;
        private const int DefaultSize = 30;

        public ModelInitialization(LoadedClasses classes, IMessageService messageService)
        {
            this.messageService = messageService;
            this.Solution = classes.Solution;
        }

        public Solution Solution { get; }

        public FieldParameters FieldParameters { get; }
            = new FieldParameters {Size = DefaultSize};

        public ObservableDictionary<string, InitializationData> InitializersData { get; } =
            new ObservableDictionary<string, InitializationData>();

        public CompiledModel CompiledModel { get; private set; }

        public void Update(IWorkflowStep previousStepData)
        {
            LoadedClasses loadedClasses = (LoadedClasses) previousStepData;
            Update(loadedClasses);
        }

        public void Clear()
        {
            CompiledModel?.Delete();
        }
        
        public void Update(LoadedClasses classes)
        {
            if (classes.Files.Count == 0)
            {
                throw new WorkflowException(ZeroTypesMessage);
            }
            
            classes.SaveFilesContents(null);

            CompiledModel = ComileAssembly(
                classes.Files.Values
                    .Select(file => file.Contents),
                Solution);

            AddAssemblyClasses(classes, CompiledModel);
        }

        private CompiledModel ComileAssembly(IEnumerable<string> classesContents, Solution solution)
        {
            TypeCompiler compiler = new TypeCompiler(solution.Name + ".dll");
            AddReferencesToCompiler(compiler, solution);
            try
            {
                return compiler.Compile(classesContents);
            }
            catch (CodeException e)
            {
                throw new WorkflowException(e.Message, e);
            }
        }

        private void AddReferencesToCompiler(TypeCompiler compiler, Solution solution)
        {
            // TODO: dll making.
            // 1. Make dll for given types of first solution.
            // 2. Make dll for solution itslf.
            // 3. Repeat using comiled assamblies.

            if (solution.BaseSolution != null)
            {
                string baseAssemblyName = CompileBaseSolution(solution);
                compiler.AddReference(baseAssemblyName);
            }

            compiler.AddGivenTypes(GetGivenTypesContents(solution.Task));
        }

        private string CompileBaseSolution(Solution solution)
        {
            Solution baseSolution = GetBaseSolution(solution);
            
            IEnumerable<string> files = GetBaseSolutionFiles(baseSolution);

            ComileAssembly(files, baseSolution);

            return $"{baseSolution.Name}.dll"; // TODO: Finish compilation of the base solution.
        }

        private void AddAssemblyClasses(LoadedClasses classes, CompiledModel compiledModel)
        {
            InitializersData.Clear();
            foreach (KeyValuePair<ClassFile, FileContents> pair in classes.Files)
            {
                AddType(pair, compiledModel);
            }
        }

        private void AddType(KeyValuePair<ClassFile, FileContents> pair,
            CompiledModel compiledModel)
        {
            string typeName = pair.Key.TypeName;
            Type userType = GetType(compiledModel, typeName);
            InitializationData data = new InitializationData(userType);
            InitializersData.Add(typeName, data);
        }

        private static Type GetType(CompiledModel compiledModel, string typeName)
        {
            try
            {
                return compiledModel.GetType(typeName);
            }
            catch (InvalidOperationException)
            {
                throw new WorkflowException(
                    "Error: Not found one of the types or names of classes are't same as file names without extension.");
            }
        }

        private static Solution GetBaseSolution(Solution solution)
        {
            ISolutionDao solutionDao = Dao.Factory.SolutionDao;
            Solution baseSolution = solutionDao.GetBaseSolution(solution);
            return baseSolution;
        }

        private static IEnumerable<string> GetBaseSolutionFiles(Solution baseSolution)
        {
            IClassFileDao fileDao = Dao.Factory.ClassFileDao;
            IEnumerable<ClassFile> otherSolutionFiles = fileDao.GetOtherSolutionFiles(baseSolution);
            IEnumerable<string> files = otherSolutionFiles
                .Select(file => fileDao.OtherSolutionFileContents(baseSolution, file));
            return files;
        }

        private IEnumerable<string> GetGivenTypesContents(Task task)
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            return TryGetGivenTypesContents(dao, task);
        }

        private IEnumerable<string> TryGetGivenTypesContents(IClassFileDao dao, Task task)
        {
            try
            {
                // TODO: We should add base solution files (and it's given types!) to current solution given/additional files. Maybe, recursively?

                IEnumerable<ClassFile> givenTypes = dao.GetGivenTypes(task);
                return givenTypes.Select(
                    typeFile => dao.GivenTypesFileContents(task, typeFile));
            }
            catch (DataAccessException e)
            {
                messageService.Show(e.Message);
                return new string[0];
            }
        }

    }
}