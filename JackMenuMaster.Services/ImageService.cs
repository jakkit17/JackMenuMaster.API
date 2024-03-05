using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace JackMenuMaster.Services 
{
    public class ImageService
    {
        public ImageService()
        {

        }
        public bool Insert(ImageBlob imageBlob)
        {
            try
            {
                if (imageBlob == null || imageBlob.Content == null || string.IsNullOrEmpty(imageBlob.Label) || string.IsNullOrEmpty(imageBlob.Name))
                {
                    // Handle invalid input
                    return false;
                }

                string directoryPath = Path.Combine(".", "ImageService", imageBlob.Label);
                string filePath = Path.Combine(directoryPath, imageBlob.Name);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(directoryPath);

                // Write the blob.Content to the specified file
                File.WriteAllBytes(filePath, imageBlob.Content);

                // File successfully saved
                return true;
            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"ImageService: Error saving file: {ex.Message}");
                //-------------------------------------------------------------------------------

                return false;
            }
        }

        public string[] SearchByFileName(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    // Handle invalid input
                    return Array.Empty<string>();
                }

                // Search for files with names similar to the provided file name
                string[] files = Directory.GetFiles(".", "*", SearchOption.AllDirectories)
                    .Where(file => Path.GetFileName(file)?.Contains(fileName, StringComparison.OrdinalIgnoreCase) == true)
                    .ToArray();

                return files;
            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"ImageService: SearchByFileName, Error saving file: {ex.Message}");
                //-------------------------------------------------------------------------------

                return Array.Empty<string>();
            }
        }

        public string[] SearchByLabel(string label)
        {
            try
            {
                if (string.IsNullOrEmpty(label))
                {
                    // Handle invalid input
                    return Array.Empty<string>();
                }

                // Search for files in the directory corresponding to the provided label
                string directoryPath = Path.Combine(".", "ImageService", label);

                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly);
                    return files;
                }

                // Return an empty array if the directory doesn't exist
                return Array.Empty<string>();
            }
            catch (Exception ex)
            {
                // Log --------------------------------------------------------------------------
                GlobalData.Log.Write($"ImageService: SearchByLabel, Error saving file: {ex.Message}");
                //-------------------------------------------------------------------------------

                return Array.Empty<string>();
            }
        }

    }


    //----------------------------------------------------------
    public class ImageBlob
    {
        public ImageBlob()
        {

        }

        public string? Name { get; set; }
        public string? Extension { get; set; }
        public string? Label { get; set; }
        public byte[]? Content { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
