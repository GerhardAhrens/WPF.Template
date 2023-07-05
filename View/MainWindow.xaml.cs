//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="PTA GmbH">
//     Class: <#= className #>
//     Copyright © PTA GmbH 2023
// </copyright>
//
// <author>Gerhard Ahrens - PTA GmbH</author>
// <email>gerhard.ahrens@pta.de</email>
// <date>05.07.2023</date>
//
// <summary>
// Class for MainWindow.xaml
// </summary>
//-----------------------------------------------------------------------

namespace WPF.Template
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Versioning;
    using System.Windows;
    using System.Windows.Threading;
    using EasyPrototypingNET.Core;
    using EasyPrototypingNET.Core.SystemMetrics;
    using EasyPrototypingNET.WPF;

    using WPF.Template.Core;
    using WPF.Template.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [SupportedOSPlatform("windows")]
    public partial class MainWindow : Window
    {
        private const string DateFormat = "dd.MM.yy HH:mm";
        private DispatcherTimer statusBarDate = null;
        private readonly MainWindowVM rootVM = null;

        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<Window, CancelEventArgs>.AddHandler(this, "Closing", this.OnClosing);
            WeakEventManager<Window, RoutedEventArgs>.AddHandler(this, "SizeChanged", this.OnSizeChanged);

            try
            {
                this.InitTimer();
                this.InfoDeviceType = SystemMetricsInfo.DetectingDeviceType();
                this.statusbarUserDomainName.Content = UserInfo.TS().CurrentDomainUser;

                if (this.rootVM == null)
                {
                    this.rootVM = new MainWindowVM();
                }

                this.DataContext = this.rootVM;
            }
            catch (Exception ex)
            {
                string errorText = ex.Message;
                throw;
            }
        }

        private InfoDeviceType InfoDeviceType { get; set; }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            DialogResultsEx dialogResult = AppMsgDialog.ApplicationExit();
            if (dialogResult == DialogResultsEx.No)
            {
                e.Cancel = true;
            }
        }

        private void OnSizeChanged(object sender, RoutedEventArgs e)
        {
            int countMonitors = SystemMetricsInfo.CountMonitors;
            string windowSize = $"{this.ActualWidth.ToInt()}x{this.ActualHeight.ToInt()}x{countMonitors}:{this.InfoDeviceType}";
            this.tbMonitorSize.Content = windowSize;
        }

        private void InitTimer()
        {
            this.statusBarDate = new DispatcherTimer();
            this.statusBarDate.Interval = new TimeSpan(0, 0, 1);
            this.statusBarDate.Start();
            this.statusBarDate.Tick += new EventHandler(
                delegate (object s, EventArgs a) {
                    this.dtStatusBarDate.Content = DateTime.Now.ToString(DateFormat);
                });
        }
    }
}
