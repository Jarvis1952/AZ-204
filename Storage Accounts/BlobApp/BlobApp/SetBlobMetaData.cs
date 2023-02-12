using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;

namespace BlobApp
{
    public  class SetBlobMetaData
    {
         string ConnectionString = "";
         string ContainerName = "";

        public async Task setBlobsMetaData()
        {
            string blobName = "";
            BlobClient blobClient = new BlobClient(ConnectionString, ContainerName, blobName);

            IDictionary<string, string> metadata = new Dictionary<string, string>();
            metadata.Add("Department", "IT");

            await blobClient.SetMetadataAsync(metadata);
            Console.Write("MetaData Added");
        }

        public async Task GetMetaData()
        {
            string blobName = "";
            BlobClient blobClient2 = new BlobClient(ConnectionString, ContainerName, blobName);
            BlobProperties blobProperties = await blobClient2.GetPropertiesAsync();

            foreach(var metaData in blobProperties.Metadata)
            {
                Console.WriteLine("The Key is {0} and Value is {1}", metaData.Key, metaData.Value);
            }
        }
        
        public async Task AcquireLease()
        {
            string blobName = "";
            BlobClient blobClient2 = new BlobClient(ConnectionString, ContainerName, blobName);

            BlobLeaseClient blobLeaseClient = blobClient2.GetBlobLeaseClient();
            TimeSpan leaseTime = new TimeSpan(0, 0, 1, 0);
            
            Response<BlobLease> response = await blobLeaseClient.AcquireAsync(leaseTime);
            Console.Write("The Lease ID is:",response.Value.LeaseId);

        }
    }
}
