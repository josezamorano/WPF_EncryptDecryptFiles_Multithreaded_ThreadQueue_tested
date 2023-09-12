using ServiceLayer.Messages;
using ServiceLayer.Utils.Interfaces;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;
using System.Windows;

using System.Windows.Interop;

namespace ServiceLayer.Helpers
{
    public class PresentationLayerHelper : IPresentationLayerHelper
    {

        public void SetFolderBrowserDialog(FolderBrowserDialog folderBrowserDialog)
        {
            folderBrowserDialog.Description = Notification.SELECT_A_FOLDER;
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.ShowHiddenFiles = true;
        }
    }
}
