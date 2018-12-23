using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;

namespace FlagsAPI.Models
{
    public class WorkerServiceFlags : IWorkerServiceFlags
    {
       
        public async Task<List<string>> GetFlagsUrl()
        {
            CloudBlobContainer cbc = await GetContainer();
            BlobContinuationToken token = new BlobContinuationToken();
            List<string> myList = new List<string>();
            do
            {
                BlobResultSegment resultSegment = await cbc.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;
                foreach (IListBlobItem blob in resultSegment.Results)
                {
                    
                    myList.Add(blob.Uri.ToString());
                   
                }
                return myList;
            } while (token != null);
        }

        public async Task<string> ReturnFlagUrl(Flag flagRequest)
        {
            var listFlags = await GetFlagsUrl();
            CloudBlobContainer cbc = await GetContainer();
            string flagBuild = cbc.Uri.ToString() + "/" + flagRequest.Nome + ".png";
            foreach (string flag in listFlags)
            {
                if (flag == flagBuild)
                {
                    return flag;
                }
            }

            return "no flag found";
           

        }

        public async Task<CloudBlobContainer> GetContainer()
        {
            CloudStorageAccount account = CreateStorageAccountFromConnectionString();
            CloudBlobClient blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer cbc = blobClient.GetContainerReference("container");
            return cbc;
        }

        public static CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            const string Message = "Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.";

            try
            {
                string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                    .Build();
                string connectionString = configuration.GetConnectionString("StorageConnectionString");
                storageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine(Message);
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine(Message);
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}
