using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobApp;

string ConnectionString = "";
string ContainerName = "Scripts";
string blobName = "scripts.sql";
string filePath = "";

// Create Container

BlobServiceClient blobServiceClient = new(ConnectionString);
await blobServiceClient.CreateBlobContainerAsync(ContainerName);
Console.WriteLine("Container Created");

// Upload a blob
BlobContainerClient blobContainerClient = new (ConnectionString, ContainerName);
BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);
blobClient.UploadAsync(filePath, true);
Console.WriteLine("Uploaded the blob");

// list a blob
await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
{
    Console.WriteLine("The Blob Name is {0}", blobItem.Name);
}

// downloading blob

BlobClient blobClient1 = new BlobClient(ConnectionString, ContainerName, blobName);
await blobClient1.DownloadToAsync(filePath);
Console.WriteLine("Blob Downloaded");

SetBlobMetaData setblob = new SetBlobMetaData();
setblob.setBlobsMetaData();
setblob.GetMetaData();
