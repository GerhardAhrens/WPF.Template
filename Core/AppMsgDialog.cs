//-----------------------------------------------------------------------
// <copyright file="AppMsgDialog.cs" company="Lifeprojects.de">
//     Class: AppMsgDialog
//     Copyright © Lifeprojects.de 2020
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>14.07.2020</date>
//
// <summary>Class with MessageBox Dialogs Texts</summary>
//-----------------------------------------------------------------------

namespace WPF.Template.Core
{
    using System.Runtime.Versioning;
    using System.Windows;
    using System.Windows.Input;

    using EasyPrototypingNET.WPF;

    [SupportedOSPlatform("windows")]
    public class AppMsgDialog
    {
        private const string APPLICATIONNAME = "WPF Template";
        public static DialogResultsEx ApplicationExit()
        {
            DialogResultsEx dialogResult = DialogResultsEx.None;
            CleanUp();
            dialogResult = MessageBoxEx.Show(APPLICATIONNAME, "Wollen Sie das Programm beenden?", string.Empty, MessageBoxButton.YesNo, InstructionIcon.Question, DialogResultsEx.No);

            return dialogResult;
        }

        private static void CleanUp()
        {
            Mouse.OverrideCursor = null;
        }
    }
}
