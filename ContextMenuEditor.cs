using System;
using Microsoft.Win32;

namespace PhotoFinder
{
    class ContextMenuEditor
    {
        public void EditRegistry()
        {
            RegistryKey Jpg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpg\Shell", true);
            RegistryKey Png = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.png\Shell", true);

            if (!Array.Exists(Registry.CurrentUser.OpenSubKey(@"SOFTWARE").GetSubKeyNames(), element => element == "PhotoFinder"))
            {
                
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true).CreateSubKey("PhotoFinder", true);
                Console.WriteLine("saved");
                RegistryKey PhotoFinder = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PhotoFinder", true);
                PhotoFinder.SetValue("DefaultBrowserPath", new BrowserHandler().GetDefaultBrowserPath());

            }

            if (!Array.Exists(Jpg.GetSubKeyNames(), element => element == "Find with google"))
            {

                try
                {
                    Jpg.CreateSubKey("Find with google", true);
                    var Find = Jpg.OpenSubKey(@"Find with google", true);
                    Find.CreateSubKey("command", true);
                    Find.OpenSubKey(@"command", true).SetValue("", $"\"{Environment.CurrentDirectory + @"\PhotoFinder.exe""" + " %v%" }\"");
                }
                catch (Exception) { }
            }

            if (!Array.Exists(Png.GetSubKeyNames(), element => element == "Find with google"))
            {
                try
                {
                    Png.CreateSubKey("Find with google", true);
                    var Find = Png.OpenSubKey(@"Find with google", true);
                    Find.CreateSubKey("command", true);
                    Find.OpenSubKey(@"command", true).SetValue("", $"\"{Environment.CurrentDirectory}" + "\\PhotoFinder.exe\"" + " %v%");
                }
                catch (Exception) { }
            }
            
        }


    }
}
