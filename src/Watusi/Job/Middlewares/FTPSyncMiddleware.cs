/*
using Sohato.ElectronicPayment.TransactionsEtl.JobFramework.FTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;

namespace Watusi.Middlewares
{
    public class FTPSyncMiddleware : IMiddleware
    {
        public void Run(IJobContext jobContext)
        {
            var protocol = (Protocol)jobContext["ftp.protocol"];
            var serverAddress = jobContext["ftp.serverAddress"] as string;
            var username = jobContext["ftp.username"] as string;
            var password = jobContext["ftp.password"] as string;
            var localAddress = jobContext["ftp.localAddress"] as string;
            var remoteAddress = jobContext["ftp.remoteAddress"] as string;
            var fileMask = jobContext["ftp.fileMask"] as string;

            var syncronizer = new Syncronizer(serverAddress, username, password, protocol: protocol,fileMask: fileMask);
            syncronizer.Syncronize(localAddress, remoteAddress, null);
        }
    }
}
*/