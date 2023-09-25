using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace ExpertFinder.Weaviate;

public class VectorDatabaseClient : IVectorDatabaseClient
{
    private HttpClient _httpClient;
    private ILogger<VectorDatabaseClient> _logger;

    public VectorDatabaseClient(HttpClient httpClient, ILogger<VectorDatabaseClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> ObjectExistsAsync(string className, Guid objectId)
    {
        _logger.LogInformation("Checking if object {objectId} exists in class {className}", objectId, className);

        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Head,
            RequestUri = new($"/v1/objects/{className}/{objectId}", UriKind.Relative)
        };

        var response = await _httpClient.SendAsync(requestMessage);

        return response.IsSuccessStatusCode;
    }

    public async Task CreateObjectAsync(VectorObject data)
    {
        _logger.LogInformation("Creating object {objectId} of class {className}", data.Id, data.Class);

        try
        {
            await _httpClient.PostAsJsonAsync($"/v1/objects/{data.Class}", data);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to create object {objectId} of class {className}", data.Id, data.Class);
            throw;
        }
    }

    public async Task<VectorObject?> GetObjectAsync(string className, Guid objectId)
    {
        _logger.LogInformation("Getting object {objectId} from class {className}", objectId, className);

        try
        {
            return await _httpClient.GetFromJsonAsync<VectorObject>($"/v1/objects/{className}/{objectId}");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to retrieve object {objectId} of class {className}", objectId, className);
            throw;
        }
    }

    public async Task UpdateObjectAsync(VectorObject data)
    {
        _logger.LogInformation("Updating object {objectId} of class {className}", data.Id, data.Class);

        try
        {
            await _httpClient.PatchAsJsonAsync($"/v1/objects/{data.Class}/{data.Id}", data);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to update object {objectId} of class {className}", data.Id, data.Class);
            throw;
        }
    }

    public async Task DeleteObjectAsync(string className, Guid objectId)
    {
        try
        {
            await _httpClient.DeleteAsync($"/v1/objects/{className}/{objectId}");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Failed to delete object {objectId} of class {className}", objectId, className);
            throw;
        }
    }
}