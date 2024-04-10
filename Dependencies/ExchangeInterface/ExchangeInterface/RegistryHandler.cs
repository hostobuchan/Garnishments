using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ExchangeInterface
{
    public static class RegistryHandler
    {
        public static string ReadProtectedString(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey("SOFTWARE\\Hosto & Buchan, P.L.L.C.\\Exchange\\Data");
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {

                string subKeyEmail = "SOFTWARE\\Hosto & Buchan, P.L.L.C.\\Exchange\\Data\\Email";
                RegistryKey subk1 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKeyEmail);
                
                string subKeyPass= "SOFTWARE\\Hosto & Buchan, P.L.L.C.\\Exchange\\Data\\PasswordHash";
                RegistryKey subk2 = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKeyPass);
                // subk1.SetValue(KeyName.ToUpper(), value);
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    byte[] data = System.Security.Cryptography.ProtectedData.Protect((byte[])sk1.GetValue(KeyName.ToUpper()), null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                  
                        return Encoding.ASCII.GetString(data);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    // AAAAAAAAAAARGH, an error!
                    //MessageBox.Show(e.ToString(), "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        public static bool WriteProtectedString(string KeyName, string Value)
        {
            try
            {
                // Setting
                RegistryKey rk = Registry.CurrentUser;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey("SOFTWARE\\Hosto & Buchan, P.L.L.C.\\Exchange\\Data");
                // Save the value
                byte[] data = System.Security.Cryptography.ProtectedData.Protect(Encoding.ASCII.GetBytes(Value), null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                sk1.SetValue(KeyName.ToUpper(), data);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                MessageBox.Show(e.ToString(), "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }
    }
}
