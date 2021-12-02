using System.IO;
using System.IO.Compression;

namespace Contracts.Api.Domain.Util
{
    public static class SupportUtil
    {
        public static bool CreateDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath)) return true;

            Directory.CreateDirectory(directoryPath);

            return true;
        }

        public static FileInfo NewFileInfo(string directoryPath, string fileName)
        {
            SupportUtil.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fileName);

            return new FileInfo(filePath);
        }

        public static bool DeleteFile(string directoryFile)
        {
            if (!File.Exists(directoryFile)) return false;

            File.Delete(directoryFile);

            return true;
        }

        public static FileInfo File_To_Zip(string pathOrigin, string pathDestiny)
        {
            ZipFile.CreateFromDirectory(pathOrigin, pathDestiny);

            return new FileInfo(pathDestiny);
        }
    }
}