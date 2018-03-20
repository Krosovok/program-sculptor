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
    public class ModelInitialization
    {
        private readonly IMessageService messageService;
        private const int DefaultSize = 30;

        public ModelInitialization(LoadedClasses classes, IMessageService messageService)
        {
            this.messageService = messageService;
            this.Solution = classes.Solution;
        }
        
        public Solution Solution { get; }
        public FieldParameters FieldParameters { get; } 
            = new FieldParameters { Size = DefaultSize };
        public ObservableDictionary<string, InitializationData> InitializersData { get; } = 
            new ObservableDictionary<string, InitializationData>();
        public CompiledModel CompiledModel { get; private set; }

        public void Update(LoadedClasses classes)
        {
            classes.SaveFilesContents(null);
            
            CompiledModel = ComileAssembly(classes);

            AddAssemblyClasses(classes, CompiledModel);
        }

        private CompiledModel ComileAssembly(LoadedClasses classes)
        {
            TypeCompiler compiler = new TypeCompiler();
            compiler.AddAdditionalTypes(GetGivenTypesContents());
            return compiler.Compile(
                classes.Files.Values
                    .Select(file => file.Contents));
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
                throw new InitializationException(
                    "Error: Not found one of the types or names of classes are't same as file names without extension.");
            }
        }

        private IEnumerable<string> GetGivenTypesContents()
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            return TryGetGivenTypesContents(dao);
        }

        private IEnumerable<string> TryGetGivenTypesContents(IClassFileDao dao)
        {
            try
            {
                // TODO: We should add base solution files (and it's given types!) to current solution given/additional files. Maybe, recursively?
                //dao.GetOtherSolutionFiles()
                
                IEnumerable<ClassFile> givenTypes = dao.GetGivenTypes(Solution.Task);
                return givenTypes.Select(
                    typeFile => dao.FileContents(Solution, typeFile));
            }
            catch (DataAccessException e)
            {
                messageService.Show(e.Message);
                return new string[0];
            }
            
        }
    }
}