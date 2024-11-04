using APIAggregator.Models;

namespace APIAggregator.Interfaces
{
    public interface INewsApi
    {
        Task<NewsApiResponse> GetTopHeadlinesAsync(string category);
    }

}
