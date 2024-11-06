using APIAggregator.Interfaces;
using APIAggregator.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APIAggregator.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IGitHubApi _gitHubApiService;
		private readonly ICatFacts _catFactsService;
		private readonly IOpenWeatherMap _openWeatherService;

		public List<Repository> GitHubRepositories { get; private set; }
		public CatFactsResponse CatFacts { get; private set; }
		public WeatherResponse Weather { get; private set; }

		public IndexModel(IGitHubApi gitHubApiService, ICatFacts catFactsService, IOpenWeatherMap openWeatherService)
		{
			_gitHubApiService = gitHubApiService;
			_catFactsService = catFactsService;
			_openWeatherService = openWeatherService;

		}

		public async Task OnGetAsync()
		{   // Added these as a placeholder :)
			GitHubRepositories = await _gitHubApiService.GetUserRepositoriesAsync("fragiskospri");
			CatFacts = await _catFactsService.GetRandomCatFactsAsync(6);
			Weather = await _openWeatherService.GetCurrentWeatherAsync("Athens");
		}
	}
}
  
  
