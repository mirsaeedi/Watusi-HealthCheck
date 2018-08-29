/*using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watusi.Middlewares
{
    public class BZ2ExtractMiddleware:IMiddleware
    {
        public BZ2ExtractMiddleware()
        {

        }

        private ConcurrentBag<string> _unsuccesfullExtractedFilePaths = new ConcurrentBag<string>();

        public Task Run(IJobContext jobContext)
        {
            var folders = jobContext["folders"] as string[];
            var fileNamePattern = jobContext["fileNamePattern"] as string;

            Parallel.ForEach(folders, (folder) =>
             {
                 var files = Directory.GetFiles(folder, fileNamePattern + ".bz2");
                 DecompressFiles(files);
             });

            foreach (var unsuccessfullExtractedFilePath in _unsuccesfullExtractedFilePaths)
            {
                var unsuccessfullFolderPath = jobContext["unsuccessfullFolderPath"] as string;
                var foldetPath = Path.GetDirectoryName(unsuccessfullExtractedFilePath);
                var folderName = Path.GetFileName(foldetPath);

                var fileName = Path.GetFileName(unsuccessfullExtractedFilePath);

                if(!Directory.Exists(Path.Combine(Path.Combine(unsuccessfullFolderPath, folderName))))
                    Directory.CreateDirectory(Path.Combine(Path.Combine(unsuccessfullFolderPath, folderName)));

                var destinationName = Path.Combine(unsuccessfullFolderPath, folderName, fileName);
                if(!File.Exists(destinationName))
                    File.Copy(unsuccessfullExtractedFilePath, destinationName);
            }
        }

        private void DecompressFiles(string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath) + ".etl";
                var path = Path.Combine(Path.GetDirectoryName(filePath), fileName);

                if (!File.Exists(path))
                {
                    DecompressFile(filePath, Path.GetDirectoryName(filePath));
                }
            }
        }

        private void DecompressFile(string filePath,string targetDirectory)
        {
            var buffer = new byte[4096];
            var fileName = Path.GetFileNameWithoutExtension(filePath) + ".etl";
            var path = Path.Combine(targetDirectory, fileName);

            try
            {
                using (Stream streamIn = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var bzip2Stream = new BZip2InputStream(streamIn))
                {
                    
                    using (var fileStreamOut = File.Create(path))
                    {
                        StreamUtils.Copy(bzip2Stream, fileStreamOut, buffer);
                    }
                }
            }
            catch (Exception)
            {

                _unsuccesfullExtractedFilePaths.Add(filePath);

                if(!File.Exists(path))
                    File.Delete(path);
            }
        }
    }
}
*/