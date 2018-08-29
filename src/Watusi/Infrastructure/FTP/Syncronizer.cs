/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace Watusi.FTP
{
    public class Syncronizer
    {
        #region Properties

        public string Password { get; private set; }
        public string ServerAddress { get; private set; }
        public string Username { get; private set; }
        public string FileMask { get; private set; }
        public Protocol Protocol { get; private set; }

        #endregion

        public Syncronizer(string serverAddress
            , string username
            , string password
            ,Protocol protocol=Protocol.Sftp
            ,string fileMask= "*.BZ2>3D<5N" // Include files newer than 5 days ago, BZ2 Files and Exclude *Incomplete* files
             )
        {
            ServerAddress = serverAddress;
            Username = username;
            Password = password;
            Protocol = protocol;
            FileMask = fileMask;
        }

        public void Syncronize(string localPath,string remotePath, FileTransferredEventHandler fileTransferredEventHandler)
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol,
                HostName = ServerAddress,
                UserName = Username,
                Password = Password,
                GiveUpSecurityAndAcceptAnySshHostKey = Protocol == Protocol.Sftp

            };

            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                session.FileTransferred += fileTransferredEventHandler;

                TransferOptions transferOptions = new TransferOptions()
                {
                    TransferMode = TransferMode.Binary,
                    PreserveTimestamp=true,
                    OverwriteMode=OverwriteMode.Overwrite,
                    SpeedLimit=20480,//20 mgb/sec
                    FileMask= "*.BZ2>3D<5N",
                };

                var synchronizationResult =
                    session.SynchronizeDirectories(
                        SynchronizationMode.Local, localPath, remotePath,
                        removeFiles: false,
                        mirror:false,
                        criteria: SynchronizationCriteria.Time,
                        options:transferOptions);

                synchronizationResult.Check();
            }
        }

    }
}
*/