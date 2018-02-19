using System.Collections.Generic;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services.Dialog;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class LoadedClasses 
    {
        public LoadedClasses()
        {
            AddEmptyFileCommand = new RelayCommand<object>(AddEmptyFile);
        }

        public LoadedClasses(Solution solution) : this()
        {
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            IEnumerable<ClassFile> solutionFiles = dao.SolutionFiles(solution);

            foreach (ClassFile solutionFile in solutionFiles)
            {
                string fileContents = dao.FileContents(solution, solutionFile);
                AddFile(solutionFile, fileContents);
            }
        }

        public ObservableDictionary<ClassFile, FileContents> Files { get; } = 
            new ObservableDictionary<ClassFile, FileContents>();
        public ICommand AddEmptyFileCommand { get; set; }

        public void AddEmptyFile(object parameter)
        {
            Dialog dialog = new Dialog {Message = "Choose file name:", Title = "New class file"};
            string chosenName = dialog.ShowNameDialog();
            if (chosenName != null)
            {
                AddFile(chosenName, string.Empty);
            }
        }

        public void AddExistingFileCommand()
        {
            // TODO: todo-todo-todo-to-do-to-dooooo.

            Dialog dialog = new Dialog { Title = "Choose code files"};
            string[] filePaths = dialog.ShowOpenFileDialog();
            // ,,,,
        }
        
        private void AddFile(string fileName, string content)
        {
            AddFile(new ClassFile(fileName), content);
        }

        private void AddFile(ClassFile solutionFile, string content)
        {
            FileContents fileContents = new FileContents(content);
            Files.Add(solutionFile, fileContents);
        }
        
    }
}