# API Aggregation Project

## Overview
This project is an API aggregation service developed in ASP.NET Core. It fetches data from multiple external APIs, consolidates the information, and provides a single, unified endpoint for easier access. The service includes filtering, sorting, and error handling, allowing for robust and scalable data aggregation.

## Features
- Integrates data from three APIs:
  - **GitHub API**: Fetches repository details based on a username.
  - **OpenWeatherMap API**: Provides current weather data by city.
  - **Cat Facts API**: Returns random fun facts about cats.
- Supports aggregated data retrieval via a single API interface.
- Implements error handling and fallback mechanisms to manage external API unavailability.
- Includes unit tests to validate functionality and ensure reliability.

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- API keys for OpenWeatherMap and GitHub 

## Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/YourUsername/APIAggregator.git
   cd APIAggregator
   ```  
2. **Configure API Keys**

- If any external APIs require authentication, add your keys in appsettings.json:
   ```json
    
    {
      "OpenWeatherMap": {
        "ApiKey": "your_openweathermap_api_key"
        "BaseUrl": "https://api.openweathermap.org/data/2.5/"
      }
    }
   ```

3. **Run the Application** In Visual Studio, press F5 to run, or from the command line:
    ```bash
    dotnet run
    ```

4. **Access the Application**

  - The default endpoint will open the Razor page.

  - Swagger documentation is available at /swagger.

## API Endpoints

***GET /github/repositories/{username}***

Fetches a list of public repositories for a specified GitHub user.

  - **Parameters**:

    - username (string): GitHub username.

  - **Example**: /github/repositories/octocat

  - **Response**:
  ```json
    [
      {
        "id": 1296269,
        "name": "Hello-World",
        "description": "My first repository on GitHub!",
        "url": "https://github.com/octocat/Hello-World"
      },
      ...
    ]
   ```

***GET /weather/{city}***

Retrieves current weather data for the specified city from OpenWeatherMap.

- **Parameters**:

  - city (string): The city name (e.g., "London").

 - **Example**: /weather/London

 - **Response**:


  ```json
    {
          "coord": {
            "lon": -0.13,
            "lat": 51.51
          },
          "weather": [
            {
              "main": "Clouds",
              "description": "scattered clouds"
            }
          ],
          "main": {
            "temp": 280.32,
            "feels_like": 278.12,
            "temp_min": 279.15,
            "temp_max": 281.15
          },
          "wind": {
            "speed": 4.1,
            "deg": 80
          },
          "clouds": {
            "all": 40
          },
          "sys": {
            "country": "GB",
            "sunrise": 1586588420,
            "sunset": 1586634840
          },
          "name": "London"
        } 
   ```

***GET /catfacts/random***

Fetches a random cat fact from the Cat Facts API.

- **Example**: /catfacts/random

- **Response**:
    ```json
    {
      "fact": "Cats have five toes on their front paws, but only four toes on their back paws.",
      "length": 64
    }
   ```

## Razor Page Usage

The application includes a Razor page that dynamically displays the aggregated API data.

1. Data Display:
    - Each API’s data is rendered in a structured table on the Razor page.

2. Interactivity:
    - Implement filtering and sorting directly from the Razor page to customize data views.

## Error Handling
The application includes robust error handling. If any API is unavailable, the service gracefully returns an error message or cached data (if caching is implemented).

## Unit Tests

Unit tests are written in xUnit to verify:

- API data retrieval

- Aggregation and processing logic

- Error handling mechanisms

Run tests from the command line:

```bash
dotnet test
```
