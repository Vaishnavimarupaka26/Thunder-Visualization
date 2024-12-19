using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using CesiumForUnity;
using UnityEngine.UI;

public class LocationSearch : MonoBehaviour
{
    public TMP_InputField inputField;
    public CesiumGeoreference cesiumGeoreference;
    public CesiumCameraController cesiumCameraController;
    public WeatherDataFetcher weatherDataFetcher; // Reference to the WeatherDataFetcher script
    public string apiKey = "AIzaSyB4eaq-slc5Y_Uc20tWK31XricEaxOpISw";

    private string urlLocation = "";
    private string urlElevation = "";
    private double lat;
    private double lon;
    private double elevation;
    private string latlon;
    private string address;
    private double height = 0; // Set this to an appropriate height value for your use case

    // Default location coordinates
    private double defaultLatitude = 28.60274;
    private double defaultLongitude = -81.19554;
    private double defaultHeight = 360.9266;

    void Start()
    {
        // Set the CesiumGeoreference to the default location
        cesiumGeoreference.originAuthority = CesiumGeoreferenceOriginAuthority.LongitudeLatitudeHeight;
        cesiumGeoreference.SetOriginLongitudeLatitudeHeight(defaultLongitude, defaultLatitude, defaultHeight);
        cesiumCameraController.transform.position = Vector3.zero;
        cesiumCameraController.transform.rotation = Quaternion.Euler(90.0f, 0, 0);
        weatherDataFetcher = FindObjectOfType<WeatherDataFetcher>();
        // Update weather data for the default location
        weatherDataFetcher.UpdateWeather(defaultLatitude, defaultLongitude);

        // Attach listener for text input field
        inputField.onEndEdit.AddListener(delegate { OnTextChange(); });
    }

    IEnumerator GetGoogleMapLocation()
    {
        string urlLocation = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=" + apiKey;
        UnityWebRequest webRequest = UnityWebRequest.Get(urlLocation);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + webRequest.error);
        }
        else
        {
            string requestText = webRequest.downloadHandler.text;
            int locationIndex = requestText.IndexOf("\"location\"");
            if (locationIndex != -1 && requestText.IndexOf("\"lat\"") > -1 && requestText.IndexOf("\"lng\"") > -1)
            {
                // Extract latitude
                int indexOfLat = requestText.IndexOf("\"lat\"", locationIndex);
                int startIndexLat = requestText.IndexOf(':', indexOfLat) + 1;
                int endIndexLat = requestText.IndexOf(',', startIndexLat);
                string latString = requestText.Substring(startIndexLat, endIndexLat - startIndexLat);
                if (double.TryParse(latString, out lat))
                {
                    Debug.Log("Latitude: " + lat);
                }

                // Extract longitude
                int indexOfLng = requestText.IndexOf("\"lng\"", locationIndex);
                int startIndexLng = requestText.IndexOf(':', indexOfLng) + 1;
                int endIndexLng = requestText.IndexOf('}', startIndexLng);
                string lngString = requestText.Substring(startIndexLng, endIndexLng - startIndexLng);
                if (double.TryParse(lngString, out lon))
                {
                    Debug.Log("Longitude: " + lon);
                }

                latlon = latString + "," + lngString;
                StartCoroutine(GetGoogleMapElevation());
            }
            else
            {
                Debug.Log("Location data not found in the response.");
            }
        }
    }

    IEnumerator GetGoogleMapElevation()
    {
        urlElevation = "https://maps.googleapis.com/maps/api/elevation/json?locations=" + latlon + "&key=" + apiKey;
        UnityWebRequest webRequest = UnityWebRequest.Get(urlElevation);
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + webRequest.error);
        }
        else
        {
            string requestText = webRequest.downloadHandler.text;
            if (requestText.IndexOf("\"elevation\"") > -1)
            {
                int elevationIndex = requestText.IndexOf("\"elevation\"");
                int colonIndex = requestText.IndexOf(":", elevationIndex);
                int commaIndex = requestText.IndexOf(",", colonIndex);
                string elevationSubstring = requestText.Substring(colonIndex + 1, commaIndex - colonIndex - 1);
                elevationSubstring = elevationSubstring.Trim();
                if (double.TryParse(elevationSubstring, out elevation)) { }
                Debug.Log("lat:" + lat + "lon:" + lon + "he:" + elevation);
                cesiumGeoreference.SetOriginLongitudeLatitudeHeight(lon, lat, elevation + 400);
                cesiumCameraController.transform.position = Vector3.zero;
                cesiumCameraController.transform.rotation = Quaternion.Euler(90.0f, 0, 0);

                // Update weather data for the new location
                Debug.Log("Updating weather for new location...");
                weatherDataFetcher.UpdateWeather(lat, lon);
            }
        }
    }

    public void OnTextChange()
    {
        address = inputField.text;
        address = address.Replace(" ", "+");
        Debug.Log("Starting location search for: " + address);
        StartCoroutine(GetGoogleMapLocation());
    }
}
