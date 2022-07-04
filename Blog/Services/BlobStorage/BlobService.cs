using Azure.Storage.Blobs;
using Blog.Services.BlobStorage;

namespace Blog.Services.BlobStorage;

public class BlobService
{
    private IConfiguration _configuration;
    private BlobContainerClient _container;

    public BlobService(IConfiguration config)
    {
        _configuration = config;
        string connectionString = config.GetConnectionString("conn_blobFileUpload"),
               containerName = config["AzureBlobContainers:blobFileUpload"];

        _container = new(connectionString, containerName);
        if (_container.Exists())
            Console.WriteLine("Successfully established connection to Azure Blob");
    }

    /// <summary> </summary>
    /// <param name="fileName"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public async ValueTask<bool> Upload(string fileName, Stream stream)
    {
        bool success = true;

        try
        {
            await _container.UploadBlobAsync(fileName, stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            success = false;
        }

        return success;
    }

    /// <summary> </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ValueTask<StreamReader> Download(string blobName)
    {
        throw new NotImplementedException();
    }
}