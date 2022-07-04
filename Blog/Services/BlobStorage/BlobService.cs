using Azure;
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
    public async ValueTask<string?> Upload(Stream stream, string? fileName = null)
    {
        Guid guid = Guid.NewGuid();
        string? blobUri = null;
        try
        {
            var blob = _container.GetBlobClient(guid.ToString());
		    blobUri = blob.Uri.AbsoluteUri;
            await blob.UploadAsync(stream);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return blobUri;
    }

    /// <summary> </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async ValueTask<StreamReader> Download(string blobName)
    {
        throw new NotImplementedException();
    }

    /// <summary>  </summary>
    /// <param name="blobName"></param>
    /// <returns></returns>
    public async Task Delete(string blobName)
    {
        var blob = _container.GetBlobClient(blobName);
        if (blob.Exists())
            await blob.DeleteAsync();
    }

    public async ValueTask<bool> Exists(string blobName)
    {
        return await _container.GetBlobClient(blobName).ExistsAsync();
    }
}