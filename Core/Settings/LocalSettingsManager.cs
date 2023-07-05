
//-----------------------------------------------------------------------
// <copyright file="LocalSettingsManager.cs" company="Lifeprojects.de">
//     Class: LocalSettingsManager
//     Copyright © Lifeprojects.de 2022
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>gerhard.ahrens@lifeprojects.de</email>
// <date>27.06.2022</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

namespace WPF.Template.Core
{
    using System;
    using System.IO;
    using System.Runtime.Versioning;

    using EasyPrototypingNET.Pattern;
    using EasyPrototypingNET.Settings;

    [SupportedOSPlatform("windows")]
    public sealed class LocalSettingsManager : LocalSettingsManagerBase
    {
        private LocalSettings localSettings = null;
        private bool exitQuestion;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalSettingsManager"/> class.
        /// </summary>
        public LocalSettingsManager()
        {
            this.localSettings = new LocalSettings();
            if (this.localSettings != null)
            {
                this.InitSettings();
            }
        }

        public bool ExitQuestion
        {
            get { return this.exitQuestion; }
            set
            {
                this.exitQuestion = value;
                this.localSettings.AddOrSet("ExitQuestion", typeof(bool), this.exitQuestion);
                this.localSettings.Save();
            }
        }


        public override void UpdateSettings<T>(string key, T value)
        {
        }

        protected override void DisposeManagedResources()
        {
            /* Behandeln von Managed Resources bem verlassen der Klasse */
            if (this.localSettings != null)
            {
                this.localSettings = null;
            }
        }

        protected override void DisposeUnmanagedResources()
        {
            /* Behandeln von UnManaged Resources bem verlassen der Klasse */
        }

        public override void InitSettings()
        {
            if (this.localSettings.IsExitSettings() == false)
            {
                this.localSettings.AddOrSet("ExitQuestion", typeof(bool), false);
                this.localSettings.Save();
            }
            else
            {
                this.localSettings.Load();
            }

            if (localSettings.Exists("ExitQuestion") == true)
            {
                this.ExitQuestion = localSettings["ExitQuestion"].ToBool();
            }
        }
    }
}
