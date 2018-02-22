using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModel;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для OrdinalFieldView.xaml
    /// </summary>
    public partial class OrdinalFieldView : UserControl
    {
        public OrdinalFieldView()
        {
            InitializeComponent();
        }

        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != this) return;

            ModelRunner model = this.DataContext as ModelRunner;

            if (model == null) return;

            FillWithCells(model);
        }

        public GridLength CellsSize { get; } = new GridLength(10);

        private void FillWithCells(ModelRunner runner)
        {
            InitGrid(runner);

            FillGrid(runner);
        }

        private void InitGrid(ModelRunner runner)
        {
            for (int i = 0; i < runner.Model.Field.Size; i++)
            {
                AddColumn();
                AddRow();
            }
        }

        private void AddColumn()
        {
            ColumnDefinition column = new ColumnDefinition
            {
                Width = CellsSize
            };
            FieldGrid.ColumnDefinitions.Add(column);
        }

        private void AddRow()
        {
            RowDefinition row = new RowDefinition
            {
                Height = CellsSize
            };
            FieldGrid.RowDefinitions.Add(row);
        }

        private void FillGrid(ModelRunner runner)
        {
            int size = runner.Model.Field.Size;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CreateElementViewAt(runner, x, y);
                }
            }
        }

        private void CreateElementViewAt(ModelRunner runner, int x, int y)
        {
            Binding binding = ColorBinding(runner, x, y);
            CreateElementView(binding, x, y);
        }

        private static void CreateElementView(Binding binding, int x, int y)
        {
            Panel elementView = new Canvas();
            elementView.SetBinding(Panel.BackgroundProperty, binding);
            Grid.SetColumn(elementView, x);
            Grid.SetRow(elementView, y);
        }

        private Binding ColorBinding(ModelRunner runner, int x, int y)
        {
            Binding binding = new Binding("Standing")
            {
                Source = runner.Model.Field[x, y],
                Converter = (IValueConverter) this.Resources["ColorConverter"],
                Mode = BindingMode.OneWay
            };
            return binding;
        }
    }
}
