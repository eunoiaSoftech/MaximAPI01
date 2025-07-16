using System.Data.SqlClient;
using System.Data;

namespace Eunoia_UM_API.Helper
{
    public static class DocumentUploader
    {
        public static string UploadDocuments(string filename,string foldername, byte[] image,string extension)
        {
            var serverImagePath = "";
            try
            {
                var relativePath = "\\wwwroot" + "\\"+ foldername;

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory() + relativePath)))
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory()+relativePath));

                var imageUploadPath = Path.Combine(Directory.GetCurrentDirectory() + relativePath, filename + "."+extension);

                if (File.Exists(imageUploadPath))
                {
                    File.Delete(imageUploadPath);
                    File.WriteAllBytes(imageUploadPath, image);
                }
                else
                {
                    File.WriteAllBytes(imageUploadPath, image);
                }

                var path = relativePath.Split("\\");

                //var host = _httpContext.HttpContext.Request.Host;

                serverImagePath = "/" +
                   path[2] + "/" +filename+"."+ extension; // /images/<filename>.jpg
            }
            catch (Exception ex)
            {
                throw;
            }
            return serverImagePath;
        }
        public static void Main()
        {
            // Specify the directory where the project file is located
            string projectDirectory = @"C:\Path\To\Your\Project";

            // Specify the name of the project file (e.g., MyProject.csproj)
            string projectFileName = "MyProject.csproj";

            // Combine the directory and file name to get the full path
            string projectFilePath = Path.Combine(projectDirectory, projectFileName);

            // Check if the file exists before attempting to load it
            if (File.Exists(projectFilePath))
            {
                // Load or read the project file content as needed
                string projectFileContent = File.ReadAllText(projectFilePath);

                // Now you can work with the content of the project file
                Console.WriteLine("Project File Content:");
                Console.WriteLine(projectFileContent);
            }
            else
            {
                Console.WriteLine("Project file not found at: " + projectFilePath);
            }
        }
    }
}
