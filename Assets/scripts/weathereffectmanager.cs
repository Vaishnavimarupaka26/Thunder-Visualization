using UnityEngine;

public class WeatherEffectManager : MonoBehaviour
{
    public WeatherDataFetcher weatherDataFetcher;
    public SkyboxManager skyboxManager;
    public RainManager rainManager;

    public GameObject lightningPrefab;  // Reference to your lightning bolt prefab
    public int numberOfBolts = 30;      // Number of lightning bolts to spawn
    public float spawnRadius = 1000f;

    private bool manualOverride = false; // Flag to check if the weather has been manually overridden

    void Start()
    {
        // Subscribe to the weather data updated event
        weatherDataFetcher.OnWeatherDataUpdated += UpdateWeatherEffects;
    }

    void UpdateWeatherEffects()
    {
        if (manualOverride)
        {
            // Skip updating based on fetched weather if manual override is active
            return;
        }

        string currentWeatherCondition = weatherDataFetcher.GetCurrentWeatherCondition();
        Debug.Log("Weather condition updated: " + currentWeatherCondition);

        skyboxManager.UpdateSkybox(currentWeatherCondition);

        if (currentWeatherCondition.Contains("thunderstorm"))
        {
            SpawnLightningBolts();
            rainManager.SetRainSystemLocation(weatherDataFetcher.gameObject);
        }
        else if (currentWeatherCondition.Contains("rain"))
        {
            rainManager.SetRainSystemLocation(weatherDataFetcher.gameObject);
        }
        else
        {
            rainManager.StopRainSystem();
        }
    }

    public void SpawnLightningBolts()
    {
        for (int i = 0; i < numberOfBolts; i++)
        {
            Vector3 randomPosition = GenerateRandomPositionWithinRadius();
            Instantiate(lightningPrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GenerateRandomPositionWithinRadius()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        return transform.position + randomPosition;  // Adjust the y-axis (height) if needed
    }
    public void DespawnLightningBolts()
    {
        GameObject[] lightningBolts = GameObject.FindGameObjectsWithTag("LightningBolt");
        foreach (GameObject bolt in lightningBolts)
        {
            Destroy(bolt);
        }
    }

    public void UpdateWeatherEffectsManually(string weatherCondition)
    {
        Debug.Log("Manually updating weather effects to: " + weatherCondition);

        // Set the manual override flag
        manualOverride = true;

        // Update the skybox based on the weather condition
        skyboxManager.UpdateSkybox(weatherCondition);

        // Update rain and thunder effects based on the weather
        if (weatherCondition.Contains("thunderstorm"))
        {
            rainManager.SetRainSystemLocation(weatherDataFetcher.gameObject);
            SpawnLightningBolts();
        }
        else if (weatherCondition.Contains("rain"))
        {
            rainManager.SetRainSystemLocation(weatherDataFetcher.gameObject);
            DespawnLightningBolts();
        }
        else
        {
            rainManager.StopRainSystem();
            DespawnLightningBolts();
        }
    }

    public void ResetToFetchedWeather()
    {
        // Disable the manual override
        manualOverride = false;

        // Update weather effects based on the fetched weather data
        UpdateWeatherEffects();
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        weatherDataFetcher.OnWeatherDataUpdated -= UpdateWeatherEffects;
    }
}

