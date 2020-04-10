using System;
using System.Diagnostics;
using Microsoft.Win32;


namespace PhotoFinder
{
    class BrowserHandler
    {
        private string BrowserFolder;
        private RegistryKey BrowserPath;
        private string BrowserPathToExe;
        public string GetDefaultBrowserPath()
        {
            var smi = Registry.CurrentUser.OpenSubKey(@"Software\Clients\StartMenuInternet");
            var DefaultBrowser = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice").GetValue("ProgId");
            string subDefaultBrowser = ((string)DefaultBrowser).Substring(0, 3);
            if (smi.GetSubKeyNames().Length == 1 && smi.GetSubKeyNames()[0].StartsWith(subDefaultBrowser)) // if StartMenuInternet contains only one folder and this folder starts with 3 chars from defaultbrowser 
            {
                BrowserFolder = smi.GetSubKeyNames()[0];
                BrowserPath = Registry.CurrentUser.OpenSubKey(@$"Software\Clients\StartMenuInternet\{BrowserFolder}\shell\open\command");
                BrowserPathToExe = BrowserPath.GetValue(BrowserPath.GetValueNames()[0]).ToString();
                return BrowserPathToExe;
            }
            else
            {
                for (int i = 0; i < smi.GetSubKeyNames().Length; ++i)
                    {
                        if (smi.GetSubKeyNames()[i].StartsWith(subDefaultBrowser))
                        {
                            BrowserFolder = smi.GetSubKeyNames()[i];
                            BrowserPath = Registry.CurrentUser.OpenSubKey(@$"Software\Clients\StartMenuInternet\{BrowserFolder}\shell\open\command");
                            BrowserPathToExe = BrowserPath.GetValue(BrowserPath.GetValueNames()[0]).ToString();
                            return BrowserPathToExe;
                        }
                    }
                return BrowserPathToExe;
            }
           
        }

        public static string GetBrowserFromRegister()
        {
            return Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PhotoFinder").GetValue("DefaultBrowserPath").ToString();
        }
    }
}
