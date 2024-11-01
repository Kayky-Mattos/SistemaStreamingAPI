using System.Buffers.Text;
using Azure.Storage.Blobs;
public interface IBlob
{
    Task<string> Upload(IFormFile file);
}

public class Blob : IBlob
{
    private readonly IConfiguration _configuration;

    public Blob(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<string> Upload(IFormFile file)
    {
        string extension = Path.GetExtension(file.FileName);
        string uniqFileName = Guid.NewGuid() + extension;

        using var stream = new MemoryStream();
        file.CopyTo(stream);
        stream.Position = 0;

        var container = new BlobContainerClient(_configuration["Blob:ConnectionString"], _configuration["Blob:ContainerName"]);
        await container.UploadBlobAsync(uniqFileName, stream);
        return container.Uri.AbsoluteUri + "/" + uniqFileName;
    }
}

