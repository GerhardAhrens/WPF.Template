//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="www.pta.de">
//     Class: MainWindowViewModel
//     Copyright © www.pta.de 2022
// </copyright>
//
// <author>Gerhard Ahrens - www.pta.de</author>
// <email>gerhard.ahrens@pta.de</email>
// <date>31.10.2022 12:31:56</date>
//
// <summary>
// Klasse für ViewModel
// </summary>
//-----------------------------------------------------------------------

namespace WPF.Template.ViewModel
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using EasyPrototypingNET.BaseClass;
    using EasyPrototypingNET.Core;
    using EasyPrototypingNET.Interface;
    using WPF.Template.Core;

    public sealed partial class MainWindowVM : ViewModelBase<MainWindowVM>, IViewModel
    {
        private readonly Window mainWindow = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowVM"/> class.
        /// </summary>
        public MainWindowVM()
        {
            this.mainWindow = Application.Current.Windows.LastActiveWindow();
            this.ApplicationVersion = ApplicationProperties.VersionWithName;

            using (LocalSettingsManager lsm = new LocalSettingsManager())
            {
                this.ExitQuestion = lsm.ExitQuestion;
            }

            Mouse.OverrideCursor = null;
            this.InitCommands();
        }


        #region Get/Set Properties
        [PropertyBinding]
        public string ApplicationVersion
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }

        [PropertyBinding]
        public string StatuslineDescription
        {
            get { return this.Get<string>(); }
            set { this.Set(value); }
        }


        private bool ExitQuestion { get; set; }
        #endregion Get/Set Properties
    }
}
