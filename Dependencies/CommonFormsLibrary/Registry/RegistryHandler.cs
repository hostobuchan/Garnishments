using Microsoft.Win32;
using System.Text;

namespace System
{
    public static class RegistryHandler
    {
        public static object ReadValue(string path, string key)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(path);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return sk1.GetValue(key);
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    Console.WriteLine("Reading registry " + e+ key.ToUpper());
                    return null;
                }
            }
        }
        public static string ReadString(string path, string key)
        {
            return Convert.ToString(ReadValue(path, key));
        }
        public static string ReadProtectedString(string path, string key)
        {
            var value = ReadValue(path, key);
            if (value == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    byte[] data = System.Security.Cryptography.ProtectedData.Unprotect((byte[])value, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                    return Encoding.ASCII.GetString(data);
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        public static bool WriteValue(string path, string key, object value)
        {
            try
            {
                // Setting
                RegistryKey rk = Registry.CurrentUser;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(path);
                // Save the value
                sk1.SetValue(key, value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }
        public static bool WriteProtectedString(string path, string key, string value)
        {
            try
            {
                byte[] data = System.Security.Cryptography.ProtectedData.Protect(Encoding.ASCII.GetBytes(value), null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return WriteValue(path, key, data);
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }
    }
}
