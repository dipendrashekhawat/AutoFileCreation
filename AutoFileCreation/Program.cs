using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;

namespace FileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfFiles = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfFiles"]); // Number of files to be created
            string folder = ConfigurationManager.AppSettings["Path"]; // Location where the files would be created

            ClearExistingData(folder);
            CreateFilesDynamically(folder, numberOfFiles);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Delete existing files and folder
        /// </summary>
        /// <param name="folder">location of files</param>
        private static void ClearExistingData(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            Directory.CreateDirectory(folder);
        }

        /// <summary>
        /// Creating files within the given range dynamically
        /// </summary>
        /// <param name="folder">Location where the files will be created</param>
        /// <param name="n">Number of files to be created</param>
        private static void CreateFilesDynamically(string folder, int n)
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();

                Parallel.For(0, n, (i) =>
                {
                    string path = Path.Combine(folder, string.Format("{0}.dat", i));
                    File.Create(path);
                });

                Console.WriteLine("Time taken for creating the files: {0}ms", sw.ElapsedMilliseconds);
            }
            catch (IOException ioe)
            {
                Console.WriteLine("IO Exception occurred" + ioe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while creating files" + ex.Message);
            }

        }
    }
}