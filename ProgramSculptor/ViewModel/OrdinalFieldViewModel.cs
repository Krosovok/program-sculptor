using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ProgramSculptor.Core;
using ProgramSculptor.Initialization;
using ProgramSculptor.Model;

namespace ViewModel
{
    public class OrdinalFieldViewModel
    {
        private readonly OrdinalModel model;
        private readonly CellViewModel[,] fieldViewModel;
        private readonly Dictionary<Type, Color> elementColors = new Dictionary<Type, Color>();

        public OrdinalFieldViewModel(OrdinalModel model)
        {
            this.model = model;
            
            int fieldSize = model.Field.Size;
            fieldViewModel = new CellViewModel[fieldSize, fieldSize];
            foreach (Cell cell in model.Field.AllCells)
            {
                fieldViewModel[cell.X, cell.Y] = new CellViewModel(this, cell);
            }
        }

        public OrdinalFieldViewModel(FieldParameters parameters) : this(new OrdinalModel(parameters)) { }

        public CellViewModel this[int x, int y] => fieldViewModel[x, y];
        public int Size => model.Field.Size;

        public void Initialize(IEnumerable<Initializer> initializers)
        {
            foreach (Initializer initializer in initializers)
            {
                elementColors.Add(initializer.ElementType, initializer.Color);
            }
            model.Initialize(initializers);
        }

        public void NextTurn() => model.NextTurn();

        public Color TypeColor(Type elementType)
        {
            return IsElementPresent(elementType) ? elementColors[elementType] : new Color();
        }

        private bool IsElementPresent(Type elementType)
        {
            return elementType != null && elementColors.ContainsKey(elementType);
        }
    }
}