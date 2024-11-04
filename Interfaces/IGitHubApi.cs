using APIAggregator.Models;

namespace APIAggregator.Interfaces
{
    public interface IGitHubApi
    {
        Task<List<Repository>> GetUserRepositoriesAsync(string username);
    }


}
