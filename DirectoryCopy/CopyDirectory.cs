using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryCopy
{
    public class CopyDirectory
    {
        //Method for copying the directories(+ files)
        public void MakeCopy(string source, string dest)
        {
            // Get the directory's subdirectories.
            DirectoryInfo directory = new DirectoryInfo(source);

            if (!directory.Exists) //In case it doesnt exists
            {
                throw new DirectoryNotFoundException(
                    "There is no directory with this name: "
                    + source);
            }

            DirectoryInfo[] directories = directory.GetDirectories();
            
            // If the destination directory doesn't exist
            if (!Directory.Exists(dest))
            {
                //Create it
                Directory.CreateDirectory(dest);
            }

            // Get the files from source directory and copy them to the destination
            FileInfo[] files = directory.GetFiles();
            int files_number = files.Length;
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(dest, file.Name);
                file.CopyTo(tempPath, false);
            }

            // For each subdirectory, do the same
            foreach (DirectoryInfo subdirectory in directories)
            {
                string tempPath = Path.Combine(dest, subdirectory.Name);
                MakeCopy(subdirectory.FullName, tempPath);
            }
        }
    }
}
