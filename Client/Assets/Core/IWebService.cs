using System.Threading.Tasks;
using Core;

public interface IWebService
{
    public Task<WebRequestResult<T>> PostAsync<T>(string url, object data = null, string token = null);
    public Task<WebRequestResult<object>> PostAsync(string url, object data = null, string token = null);
    
    public Task<WebRequestResult<T>> GetAsync<T>(string url, object data = null, string token = null);
    public Task<WebRequestResult<object>> GetAsync(string url, object data = null, string token = null);
}
