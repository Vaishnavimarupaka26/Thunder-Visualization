using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Material clearSkySkybox;
    public Material fewCloudsSkybox;
    public Material brokenCloudsSkybox;
    public Material overcastCloudsSkybox;

    private WeatherDataFetcher weatherDataFetcher;

    void Start()
    {
        weatherDataFetcher = GetComponent<WeatherDataFetcher>();
    }
    

    public void UpdateSkybox(string weatherDescription)
    {
        if(weatherDescription.ToLower().Contains("rain")|| weatherDescription.ToLower().Contains("thunderstorm"))
        {
            RenderSettings.skybox = overcastCloudsSkybox;
        }
        switch (weatherDescription.ToLower())
        {
            case "clear sky":
                RenderSettings.skybox = clearSkySkybox;
                break;
            case "few clouds":
                RenderSettings.skybox = fewCloudsSkybox;
                break;
            case "broken clouds":
                RenderSettings.skybox = brokenCloudsSkybox;
                break;
            case "overcast clouds":
                RenderSettings.skybox = overcastCloudsSkybox;
                break;
            default:
                Debug.Log("No specific skybox for this weather condition.");
                break;
        }

        DynamicGI.UpdateEnvironment();
    }
}

