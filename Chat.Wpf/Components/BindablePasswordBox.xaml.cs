// ------------------------------------------------------------
// <copyright file="BindablePasswordBox.xaml.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace Chat.Wpf
{
    /// <summary>
    /// Extension to PasswordBox for bind to password text.
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        /// <summary>
        /// Password dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached(
        "Password", typeof(string), typeof(BindablePasswordBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Initializes a new instance of the <see cref="BindablePasswordBox"/> class.
        /// </summary>
        public BindablePasswordBox()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets password property.
        /// </summary>
        public string Password
        {
            get => (string)this.GetValue(PasswordProperty);
            set => this.SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Method of password changed for update password binded property.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Args.</param>
        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Password = this.passwordBox.Password;
        }
    }
}
