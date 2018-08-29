/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace Watusi.FTP
{
    public class Downloader
    {
        #region Properties

        public string Password { get; private set; }
        public string ServerAddress { get; private set; }
        public string Username { get; private set; }
        public int Ip { get; private set; }
        public Protocol Protocol { get; private set; }

        #endregion


        public Downloader(string serverAddress, string username, string password,int ip=22,Protocol protocol=Protocol.Sftp)
        {
            ServerAddress = serverAddress;
            Username = username;
            Password = password;
            Ip = ip;
            Protocol = protocol;
        }

        public void Download(string sourcePath,string destinationPath)
        { 
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol,
                HostName = ServerAddress,
                UserName = Username,
                Password = Password,
                PortNumber = Ip,
                GiveUpSecurityAndAcceptAnySshHostKey = true
            };

            using (Session session = new Session())
            {
                session.Open(sessionOptions); 
                                              
                TransferOptions transferOptions = new TransferOptions()
                {
                    TransferMode = TransferMode.Binary,
                    PreserveTimestamp=true,
                    OverwriteMode=OverwriteMode.Overwrite
                };

                var transferResult = session
                    .GetFiles(sourcePath, destinationPath, false, transferOptions);
                
                transferResult.Check();

                foreach (TransferEventArgs transfer in transferResult.Transfers)
                {
                    Console.WriteLine("Download of {0} succeeded", transfer.FileName);
                }

            }
        }
        
    }
}
*/