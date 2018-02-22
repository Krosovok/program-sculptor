using System;
using System.Collections.Generic;
using System.Windows.Input;
using DataAccessInterfaces;
using Model;
using Services.Dialog;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Services;

namespace ViewModel
{
    public class LoadedClasses
    {
        // TODO: Maybe some more flexible way? 

        private static readonly string[] Extenstions = { "cs" };

        internal readonly IMessageService Messages;
        internal readonly IDialogFactory DialogFactory;

        public LoadedClasses(IMessageService messages, IDialogFactory dialogFactory)
        {
            this.DialogFactory = dialogFactory;
            this.Messages = messages;
            
            AddEmptyFileCommand = new RelayCommand<object>(AddEmptyFile);
            OpenFilesCommand = new RelayCommand<object>(AddExistingFiles);
            AddFilesCommand = new RelayCommand<string[]>(AddFiles);
        }

        public LoadedClasses(Solution solution, IMessageService messages, IDialogFactory dialogFactory) 
            : this(messages, dialogFactory)
        {
            Solution = solution;
            IClassFileDao dao = Dao.Factory.ClassFileDao;
            IEnumerable<ClassFile> solutionFiles = dao.SolutionFiles(solution);

            foreach (ClassFile solutionFile in solutionFiles)
            {
                string fileContents = dao.FileContents(solution, solutionFile);
                AddFile(solutionFile, fileContents);
            }
        }

        public Solution Solution { get; }
        public ObservableDictionary<ClassFile, FileContents> Files { get; } =
            new ObservableDictionary<ClassFile, FileContents>();
        public ICommand AddEmptyFileCommand { get; set; }
        public ICommand OpenFilesCommand { get; set; }
        public RelayCommand<string[]> AddFilesCommand { get; set; }

        public void AddEmptyFile(object parameter)
        {
            string chosenName = AskNameInDialog();
            if (chosenName != null)
            {
                AddFile(chosenName, string.Empty);
            }
        }

        public void AddExistingFiles(object parameter)
        {
            string[] filePaths = AskFilesDialog();

            AddFiles(filePaths);
        }

        private string AskNameInDialog()
        {
            IDialog dialog = DialogFactory.NewDialog();
            dialog.Message = "Choose file name:";
            dialog.Title = "New class file";
            
            return dialog.ShowNameDialog();
        }

        private string[] AskFilesDialog()
        {
            IDialog dialog = DialogFactory.NewDialog();
            dialog.Title = "Choose code files";
            string[] filePaths = dialog.ShowOpenFileDialog();
            return filePaths;
        }

        private void AddFiles(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                TryReadFile(filePath);
            }
        }

        private void TryReadFile(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string fileContetns = GetFileContetns(filePath, fileName);

            if (fileContetns != null)
            {
                AddFile(fileName, fileContetns);
            }
        }

        private string GetFileContetns(string filePath, string fileName)
        {
            if (!Extenstions.Any(filePath.EndsWith))
            {
                string message = string.Format(
                    "Wrong file type \'*.{0}\'. Expexted on of: {1}.", 
                    Path.GetExtension(filePath),
                    string.Join(", ", Extenstions));
                Messages.Show(message);
            }
            
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (IOException)
            {
                string message = string.Format("Failed to load file: {0}", fileName);
                Messages.Show(message);
                return null;
            }
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