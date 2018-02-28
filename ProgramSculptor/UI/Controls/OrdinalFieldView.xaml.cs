using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ProgramSculptor.Core;
using ProgramSculptor.Model;
using UI.Content;
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
            OrdinalFieldViewModel field = this.DataContext as OrdinalFieldViewModel;

            if (field != null)
            {
                FillWithCells(field);
            }
            else
            {
                Clear();
            }
        }

        private void Clear()
        {
            FieldGrid.Children.Clear();
            FieldGrid.ColumnDefinitions.Clear();
            FieldGrid.RowDefinitions.Clear();
        }

        public GridLength CellsSize { get; } = new GridLength(10);

        private void FillWithCells(OrdinalFieldViewModel field)
        {
            InitGrid(field);

            FillGrid(field);
        }

        private void InitGrid(OrdinalFieldViewModel field)
        {
            for (int i = 0; i < field.Size; i++)
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

        private void FillGrid(OrdinalFieldViewModel field)
        {
            int size = field.Size;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CreateElementViewAt(field, x, y);
                }
            }
        }

        private void CreateElementViewAt(OrdinalFieldViewModel field, int x, int y)
        {
            Binding binding = ColorBinding(field, x, y);
            CreateElementView(binding, x, y);
        }

        private void CreateElementView(Binding binding, int x, int y)
        {
            Panel elementView = new Canvas();
            elementView.SetBinding(Panel.BackgroundProperty, binding);
            Grid.SetColumn(elementView, x);
            Grid.SetRow(elementView, y);
            FieldGrid.Children.Add(elementView);
        }

        private Binding ColorBinding(OrdinalFieldViewModel field, int x, int y)
        {
            IValueConverter converter = (IValueConverter) this.Resources["ColorConverter"];
            Binding binding = new Binding(nameof(CellViewModel.Background))
            {
                Source = field[x, y],
                Converter = converter,
                Mode = BindingMode.OneWay
            };
            return binding;
        }
    }
}