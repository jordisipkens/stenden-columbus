using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using WebserviceColumbus.Models.Other;
using System.IO;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Other
{
    public class FileManager
    {
        public static Photo UploadFile(User user, HttpPostedFile file)
        {
            CloudBlobContainer container = GetContainer(string.Format("{0}-{1}", user.Username, user.ID));
            container.CreateIfNotExists();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.FileName);
            try {
                using(var fileStream = file.InputStream) {
                    blockBlob.UploadFromStream(fileStream);
                }
            }
            catch(Exception ex) {
                new ErrorHandler(ex, "Failed to upload filestream", true);
            }

            Photo photo = new Photo() {
                URL = blockBlob.Uri.ToString()
            };
            return new DbManager<Photo>().AddEntity(photo);
        }

        private static CloudBlobContainer GetContainer(string name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorage"));
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            return client.GetContainerReference(name);
        }

        private static void Show(string name)
        {
            foreach(IListBlobItem item in GetContainer(name).ListBlobs(null, false)) {
                if(item.GetType() == typeof(CloudBlockBlob)) {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

                }
                else if(item.GetType() == typeof(CloudPageBlob)) {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if(item.GetType() == typeof(CloudBlobDirectory)) {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
        }
    }
}