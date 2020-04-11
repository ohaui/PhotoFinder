using System;
using Microsoft.Win32;

namespace PhotoFinder
{
    class ContextMenuEditor
    {

        private RegistryKey[] RegistryAssociations;
        public void EditRegistry()
        {
            try
            {
                RegistryAssociations = new RegistryKey[] // associations to change
                {
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.jpg\Shell", true),
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.png\Shell", true),
                    Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\SystemFileAssociations\.gif\Shell", true)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Administrator permissions is required");
                Console.WriteLine("Start this app as administrator \n");
                return;
            }
            
            for (int i = 0; i < RegistryAssociations.Length; ++i)
            {
                if (!Array.Exists(RegistryAssociations[i].GetSubKeyNames(), element => element == "Find with google"))
                {
                    try
                    {
                        RegistryAssociations[i].CreateSubKey("Find with google", true);
                        var Find = RegistryAssociations[i].OpenSubKey(@"Find with google", true);
                        Find.CreateSubKey("command", true);
                        Find.OpenSubKey(@"command", true).SetValue("", $"\"{Environment.CurrentDirectory + @"\PhotoFinder.exe""" + " %v%" }\"");
                    }
                    catch {}
                }
            }
        }


    }
}
//if (!Array.Exists(Registry.CurrentUser.OpenSubKey(@"SOFTWARE").GetSubKeyNames(), element => element == "PhotoFinder"))
//{

//    Registry.CurrentUser.OpenSubKey(@"SOFTWARE", true).CreateSubKey("PhotoFinder", true);
//    Console.WriteLine("saved");
//    RegistryKey PhotoFinder = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PhotoFinder", true);
//    PhotoFinder.SetValue("DefaultBrowserPath", new BrowserHandler().GetDefaultBrowserPath());

//}