using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Model;
using ViewModel;

namespace UI.Controls
{
    /// <summary>
    /// Логика взаимодействия для TaskChainView.xaml
    /// </summary>
    public partial class TaskChainView 
    {
        public TaskChainView()
        {
            InitializeComponent();
        }
        
        private void ContextMenuOpenClick(object sender, RoutedEventArgs e)
        {
            // TODO: Find some way of CORRECT displaying on left-click. (See Styles.xaml also.)

            //otherTasksMenu.PlacementTarget = null;
            //otherTasksMenu.PlacementTarget = sender as UIElement;
            //MessageBox.Show((otherTasksMenu.PlacementTarget as Control)?.Name);
        }
    }
}