using Model;

namespace ViewModel.Core
{
    public class TaskViewModel
    {
        private readonly Task model;

        public TaskViewModel(Task model)
        {
            this.model = model;
            InChain = new TaskChainPosition(model);
        }

        public string TaskName => model.TaskName;
        public string Description => model.Description;
        public TaskChainPosition InChain { get; }
    }
}
