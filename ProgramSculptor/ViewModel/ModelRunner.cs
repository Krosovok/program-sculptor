using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Model;

namespace ViewModel
{
    public class ModelRunner
    {
        private readonly Dictionary<Type, Initializer> fieldInitializers = new Dictionary<Type, Initializer>();
        
        public OrdinalModel Model { get; set; }

        public void Update(ModelInitialization initialization)
        {
            foreach (KeyValuePair<string, InitializationData> pair
                in initialization.InitializersData)
            {
                Initializer initializer = Initializer.FromInitializationData(pair.Value);
                fieldInitializers.Add(initializer.ElementType, initializer);
            }

            Model = new OrdinalModel(initialization.FieldParameters);
            Model.Initialize(fieldInitializers.Values);
        }

        public Color TypeColor(Type type)
        {
            if (!fieldInitializers.ContainsKey(type))
            {
                return new Color();
            }
            
            return fieldInitializers[type].Color;
        }
    }
}