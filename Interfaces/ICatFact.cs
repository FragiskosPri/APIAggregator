using APIAggregator.Models;
using System.Threading.Tasks;

namespace APIAggregator.Interfaces
{
    public interface ICatFacts
    {
        Task<CatFactsResponse> GetRandomCatFactsAsync(int count);
    }
}
