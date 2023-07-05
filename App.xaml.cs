﻿namespace WPF.Template
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string DEFAULTLANGUAGE = "de-DE";
        private static readonly string MessageBoxTitle = "WPF Template Application";
        private static readonly string UnexpectedError = "An unexpected error occured.";

        public App()
        {
            string exePath = string.Empty;
            string exeName = string.Empty;

            WeakEventManager<Application, DispatcherUnhandledExceptionEventArgs>.AddHandler(this, "DispatcherUnhandledException", this.OnDispatcherUnhandledException);

            try
            {
                InitializeCultures(DEFAULTLANGUAGE);

                exePath = Assembly.GetExecutingAssembly().Location.Replace("dll", "exe", StringComparison.OrdinalIgnoreCase);
                exeName = Path.GetFileName(exePath).Replace("dll", "exe", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                ex.Data.Add("UserDomainName", Environment.UserDomainName);
                ex.Data.Add("UserName", Environment.UserName);
                ex.Data.Add("exePath", exePath);
                ErrorMessage(ex, "General Error: ");
                Application.Current.Shutdown(0);
            }
        }

        public static string DatePattern { get; set; }

        public static void ErrorMessage(Exception ex, string message = "")
        {
            string expMsg = ex.Message;
            var aex = ex as AggregateException;

            if (aex != null && aex.InnerExceptions.Count == 1)
            {
                expMsg = aex.InnerExceptions[0].Message;
            }

            if (string.IsNullOrEmpty(message) == true)
            {
                message = UnexpectedError;
            }

            StringBuilder errorText = new StringBuilder();
            if (ex.Data != null && ex.Data.Count > 0)
            {
                foreach (DictionaryEntry item in ex.Data)
                {
                    errorText.AppendLine($"{item.Key} : {item.Value}");
                }
            }

            MessageBox.Show(
                message + $"{expMsg}\n{ex.Message}\n{errorText.ToString()}",
                MessageBoxTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private static void InitializeCultures(string language)
        {
            if (string.IsNullOrEmpty(language) == false)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(DEFAULTLANGUAGE);
            }

            if (string.IsNullOrEmpty(language) == false)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(DEFAULTLANGUAGE);
            }

            DatePattern = $"{DateTimeFormatInfo.CurrentInfo.ShortDatePattern}";

            FrameworkPropertyMetadata frameworkMetadata = new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(new CultureInfo(language).IetfLanguageTag));
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), frameworkMetadata);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string app = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            Debug.WriteLine($"{app}-{(e.Exception as Exception).Message}");
        }

        private string CurrentAssemblyName()
        {
            string assmName = Assembly.GetExecutingAssembly().GetName().Name;

            if (assmName.Contains(".") == true)
            {
                return assmName.Split('.')[0];
            }
            else
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        private void ApplicationExit()
        {
            Application.Current.Shutdown();
            Process.GetCurrentProcess().Kill();
        }
    }
}
