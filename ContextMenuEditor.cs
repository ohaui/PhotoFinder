using System;
using Microsoft.Win32;

namespace PhotoFinder
{
    class ContextMenuEditor
    {

        private RegistryKey[] RegistryAssociations;
        public void EditRegistry()
        {
            OpenRegistryAssociations();
            for (int i = 0; i < RegistryAssociations.Length; ++i)
            {
                if (!Array.Exists(RegistryAssociations[i].GetSubKeyNames(), element => element == "Find with Google"))
                {
                    try
                    {
                        OpenRegistryAssociations();
                        RegistryAssociations[i].CreateSubKey("Find with Google", true);
                        var Find = RegistryAssociations[i].OpenSubKey("Find with Google", true);
                        Find.CreateSubKey("command", true);
                        Find.OpenSubKey("command", true).SetValue("", $"\"{Environment.CurrentDirectory + @"\PhotoFinder.exe""" + " %v%" }\"");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Something went wrong \n Error: {e.Message}");
                        return;
                       
                    }
                }
            }
            Console.WriteLine("Successfully added all needed changes to the registry");

        }
        public void ClearRegistry()
        {
            OpenRegistryAssociations();
            for (int i = 0; i < RegistryAssociations.Length; ++i)
            {
                try
                {
                    RegistryAssociations[i].DeleteSubKeyTree("Find with Google");
                }
                catch (Exception e)
                {
                    if (e == e as ArgumentException)
                    {
                        continue;
                    }
                    Console.WriteLine($"Something went wrong \n Error: {e.Message}");
                    return;
                }
            }
            Console.WriteLine("Successfully deleted all registry changes");

        }

        private void OpenRegistryAssociations() 
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
            catch (Exception)
            {
                Console.WriteLine("Administrator rights is required");
                Console.WriteLine("Start this app as administrator \n");
            }
        }

        


    }
}
