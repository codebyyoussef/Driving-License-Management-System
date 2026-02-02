using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool CreateFolderIfNotExist(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return false;

            if (Directory.Exists(folderPath))
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(folderPath);
                return true;
            }
        }

        public static string ReplaceFileNameWithGUID(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            return GenerateGUID() + fi.Extension;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string destinationFolder = @"C:\DVLD-People-Images\";

            if (!CreateFolderIfNotExist(destinationFolder))
            {
                return false;
            }

            string destinationFile = destinationFolder + ReplaceFileNameWithGUID(sourceFile);

            try
            {
                File.Copy(sourceFile, destinationFile);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = destinationFile;
            return true;
        }
    }
}
