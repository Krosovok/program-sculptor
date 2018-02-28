using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Model;
using System.Windows.Input;

namespace ViewModel
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

        public ICommand NextTurnCommand => new RelayCommand<object>((o) => Model.NextTurn());

        private Dictionary<Type, Initializer> GetFieldInitializers(ModelInitialization initialization)
        {
            return initialization.InitializersData
                .Select(pair => Initializer.FromInitializationData(pair.Value))
                .ToDictionary(initializer => initializer.ElementType);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}