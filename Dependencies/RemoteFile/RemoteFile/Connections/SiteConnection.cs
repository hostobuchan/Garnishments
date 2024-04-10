using System;

namespace RemoteFile.Connections
{
    public static class SiteConnection
    {
        public static Interfaces.SiteConnectionInterface CreateSiteConnection(SiteLogin login)
        {
            switch (login.ProtocolUsed)
            {
                case Enums.ConnectionMethod.SFTP:
                    return new Methods.SFTP.SFTP(login);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
