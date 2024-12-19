using UnityEngine;
using TMPro; 

public class WeatherDropdown : MonoBehaviour
{
    public TMP_Dropdown weatherDropdown; 
    public WeatherEffectManager weatherEffectManager;

    void Start()
    {
        // Ensure that the dropdown is set up to trigger the function when the value is changed
        weatherDropdown.onValueChanged.AddListener(DropdownValueChanged);
    }

    void DropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0:
                weatherEffectManager.UpdateWeatherEffectsManually("clear sky");
                break;
            case 1:
                weatherEffectManager.UpdateWeatherEffectsManually("thunderstorm");
                break;
            case 2:
                weatherEffectManager.UpdateWeatherEffectsManually("rain");
                break;
            case 3:
                weatherEffectManager.UpdateWeatherEffectsManually("overcast clouds");
                break;
            case 4: // New case for "Default Weather"
                ResetToFetchedWeather(); // Revert to fetched weather
                break;
            default:
                // Optionally, handle cases where no matching weather condition is found
                Debug.LogWarning("Unknown weather condition selected.");
                break;
        }
    }

    // Method to reset the weather to the condition fetched by WeatherDataFetcher
    void ResetToFetchedWeather()
    {
        string currentWeatherCondition = weatherEffectManager.weatherDataFetcher.GetCurrentWeatherCondition();
        weatherEffectManager.UpdateWeatherEffectsManually(currentWeatherCondition);
        Debug.Log("Reverted to fetched weather condition: " + currentWeatherCondition);
    }
}
