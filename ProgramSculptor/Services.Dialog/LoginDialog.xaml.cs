using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DialogViewModel;

namespace Services.Dialog
{
    /// <summary>
    /// Логика взаимодействия для LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog
    {
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register(
                nameof(Result),
                typeof(bool?),
                typeof(LoginDialog),
                new PropertyMetadata(Target));

        private static void Target(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            LoginDialog loginDialog = dependencyObject as LoginDialog;
            bool? value = args.NewValue as bool?;

            if (loginDialog == null) return;
            
            loginDialog.DialogResult = value;
            if (loginDialog.DialogResult == true)
            {
                loginDialog.Close();
            }
        }

        public LoginDialog()
        {
            InitializeComponent();

            Binding binding = new Binding(nameof(LoginViewModel.Result))
            {
                Source = DataContext,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            SetBinding(ResultProperty, binding);
        }

        public bool? Result
        {
            get { return (bool?) GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginViewModel loginViewModel = DataContext as LoginViewModel;
            if (loginViewModel != null)
            {
                loginViewModel.Password = (sender as PasswordBox)?.SecurePassword;
            }
        }
    }
}