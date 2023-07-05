//-----------------------------------------------------------------------
// <copyright file="MainWindowVM.Command.cs" company="www.pta.de">
//     Class: MainWindowVM
//     Copyright © www.pta.de 2022
// </copyright>
//
// <author>Gerhard Ahrens - www.pta.de</author>
// <email>gerhard.ahrens@pta.de</email>
// <date>03.11.2022 15:09:23</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

namespace WPF.Template.ViewModel
{
    using System.Windows;
    using System.Windows.Input;

    using EasyPrototypingNET.BaseClass;
    using EasyPrototypingNET.Interface;
    using EasyPrototypingNET.WPF;

    using WPF.Template.Core;

    public partial class MainWindowVM : ViewModelBase<MainWindowVM>, IViewModel
    {
        private void WindowCloseHandler()
        {
            if (this.ExitQuestion == true)
            {
                DialogResultsEx dialogResult = AppMsgDialog.ApplicationExit();
                if (dialogResult == DialogResultsEx.Yes)
                {
                    Window currentWindow = Application.Current.Windows.LastActiveWindow();
                    if (currentWindow != null)
                    {
                        currentWindow.Close();
                    }
                }
            }
            else
            {
                Window currentWindow = Application.Current.Windows.LastActiveWindow();
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
        }
    }
}
