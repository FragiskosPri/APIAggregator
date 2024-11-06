//----------------------- GitHub ----------------------- //
async function fetchGitHubRepositories() {
    const username = document.getElementById('gitUsernameInput').value; // Access value of input
    if (!username) {
        alert("Please enter a valid username.");
        return;
    }

    try {

        const response = await fetch(`/api/github/repositories/${username}`);
        if (response.ok) {
            const repositories = await response.json();
            populateTableWithRepositories(repositories);
        } else {
            console.error('Repository data not found:', data);
            alert('Weather data not found for the entered city.');
        }

    } catch (error) {
        console.error("Failed to fetch repositories:", error);
        alert("An error occurred while fetching repositories.");
    }
}

// Helper function to populate the table
function populateTableWithRepositories(repositories) {
    const tableBody = document.getElementById("gitTableData").getElementsByTagName("tbody")[0];
    tableBody.innerHTML = ""; // Clear existing data

    repositories.forEach(repo => {
        const row = tableBody.insertRow();
        Object.values(repo).forEach(value => {
            const cell = row.insertCell();
            cell.textContent = value || "N/A"; // Fill with "N/A" if data is null or undefined
        });
    });
}

//----------------------- Weather ----------------------- //
async function fetchWeatherData() {
    const city = document.getElementById('weatherRequestInput').value; // Access value of input
    if (!city) {
        alert("Please enter a valid city.");
        return;
    }

    try {
        const response = await fetch(`/api/weather/current/${city}`);
        if (response.ok) {
            const weatherData = await response.json();
            populateTableWithWeatherData(weatherData);
        } else {
            console.error('Weather data not found:', response);
            alert('Weather data not found for the entered city.');
        }
    } catch (error) {
        console.error("Failed to fetch weather data:", error);
        alert("An error occurred while fetching weather data.");
    }
}

function populateTableWithWeatherData(weatherData) {
    const tableBody = document.getElementById("weatherTableData").getElementsByTagName("tbody")[0];



    const weatherFields = [
        weatherData.coord.lon,
        weatherData.coord.lat,
        weatherData.weather[0].main,
        weatherData.weather[0].description,
        weatherData.main.temp,
        weatherData.main.feels_like !== undefined ? weatherData.main.feels_like : "N/A", // Check if feelsLike is present
        weatherData.main.temp_min !== undefined ? weatherData.main.temp_min : "N/A", // Check if tempMin is present
        weatherData.main.temp_max !== undefined ? weatherData.main.temp_max : "N/A", // Check if tempMax is present
        weatherData.wind.speed,
        weatherData.wind.deg,
        weatherData.clouds.all,
        weatherData.sys.country,
        new Date(weatherData.sys.sunrise * 1000).toLocaleTimeString(),
        new Date(weatherData.sys.sunset * 1000).toLocaleTimeString(),
        weatherData.name,
        weatherData.id,
        weatherData.base,
        weatherData.visibility,
        weatherData.dt,
        weatherData.cod
    ];

    // Insert the data into the table
    const row = tableBody.insertRow();
    weatherFields.forEach(value => {
        const cell = row.insertCell();
        cell.textContent = value || "N/A"; // Fill with "N/A" if value is null or undefined
    });
}


//----------------------- Cat Facts ----------------------- //
async function fetchCatFacts() {
    const countInput = document.getElementById('catFactsRequestSearch');
    if (!countInput || !countInput.value || countInput.value <= 0) {
        alert("Please enter a valid count.");
        return;
    }

    const count = countInput.value;

    try {
        const response = await fetch(`/api/catfacts/random/${count}`);
        if (response.ok) {
            const catFactsData = await response.json();
            console.log("Received Cat Facts Data:", catFactsData); // Inspect data structure
            populateTableWithCatFacts(catFactsData);
        } else {
            console.error('Cat facts data not found:', response);
            alert('Cat facts data not found.');
        }
    } catch (error) {
        console.error("Failed to fetch cat facts:", error);
        alert("An error occurred while fetching cat facts.");
    }
}

// Helper function to populate the table with cat facts
function populateTableWithCatFacts(catFactsData) {
    const tableBody = document.getElementById("catFactsTableData").getElementsByTagName("tbody")[0];
    tableBody.innerHTML = ""; // Clear existing data

    if (catFactsData.data && Array.isArray(catFactsData.data)) {
        catFactsData.data.forEach(factObj => {
            const row = tableBody.insertRow();

            // Corrected property names to match actual API response
            const cell1 = row.insertCell(0);
            cell1.textContent = factObj.fact || "N/A";

            const cell2 = row.insertCell(1);
            cell2.textContent = factObj.length || "N/A";
        });
    } else {
        console.error("Invalid data format:", catFactsData);
        alert("Received data format is invalid.");
    }
}
