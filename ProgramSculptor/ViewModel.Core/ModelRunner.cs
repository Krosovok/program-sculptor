using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProgramSculptor.Initialization;
using ViewModel.Command;

namespace ViewModel.Core
{
    public class ModelRunner : INotifyPropertyChanged
    {
        public OrdinalFieldViewModel Model { get; private set; }

        public void Update(ModelInitialization initialization)
        {
            Dictionary<Type, Initializer> fieldInitializers = GetFieldInitializers(initialization);

            Model = new OrdinalFieldViewModel(initialization.FieldParameters);
            Model.Initialize(fieldInitializers.Values);
            OnPropertyChanged(nameof(Model));
        }

        public void Clear()
        {
            Model = null;
            OnPropertyChanged(nameof(Model));
        }

        public ICommand NextTurnCommand => new RelayCommand<object>(MoveToNextTurn);

        private void MoveToNextTurn(object o)
        {
            try
            {
                Model.NextTurn();
            }
            catch (Exception userError)
            {
                UserCodeException?.Invoke(userError);
            }
        }

        private Dictionary<Type, Initializer> GetFieldInitializers(ModelInitialization initialization)
        {
            return initialization.InitializersData
                .Select(pair => Initializer.FromInitializationData(pair.Value))
                .ToDictionary(initializer => initializer.ElementType);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<Exception> UserCodeException;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}