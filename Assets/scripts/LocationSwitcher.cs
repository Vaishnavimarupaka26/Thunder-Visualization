using UnityEngine;

public class LocationSwitcher : MonoBehaviour
{
    public GameObject ucfGeoreference;
    public GameObject orlandoGeoreference;
    public GameObject tampaGeoreference;
    public GameObject miamiGeoreference;
    public GameObject jacksonvilleGeoreference;

    public GameObject dynamicCamera; // Reference to the single DynamicCamera

    private GameObject activeGeoreference;
    private WeatherDataFetcher weatherDataFetcher;

    void Start()
    {
        weatherDataFetcher = FindObjectOfType<WeatherDataFetcher>();
        SetActiveLocation(ucfGeoreference, 28.6024f, -81.2001f); // Default UCF location
    }

    void SetActiveLocation(GameObject location, float latitude, float longitude)
    {
        if (activeGeoreference != null)
        {
            activeGeoreference.SetActive(false);
        }

        location.SetActive(true);
        activeGeoreference = location;

        // Move the dynamic camera to the new active location
        dynamicCamera.transform.position = location.transform.position;
        dynamicCamera.transform.rotation = location.transform.rotation;

        // Update weather for the new location
        weatherDataFetcher.UpdateWeather(latitude, longitude);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveLocation(ucfGeoreference, 28.6024f, -81.2001f); // UCF
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveLocation(orlandoGeoreference, 28.5383f, -81.3792f); // Orlando Downtown
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveLocation(tampaGeoreference, 27.9506f, -82.4572f); // Tampa
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveLocation(miamiGeoreference, 25.7617f, -80.1918f); // Miami
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetActiveLocation(jacksonvilleGeoreference, 30.3322f, -81.6557f); // Jacksonville
        }
    }
}

