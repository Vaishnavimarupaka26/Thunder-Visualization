using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WeatherDataFetcher : MonoBehaviour
{
    private string apiKey = "667758377b3d240c37f2328bc7e02743"; // Replace with your OpenWeatherMap API key

    [Serializable]
    public class WeatherData
    {
        public Weather[] weather;
        public Main main;
        public Wind wind;
        public Clouds clouds;

        [Serializable]
        public class Weather
        {
            public string description;
            public string main;
        }

        [Serializable]
        public class Main
        {
            public float temp;
            public float pressure;
            public float humidity;
        }

        [Serializable]
        public class Wind
        {
            public float speed;
            public float deg;
        }

        [Serializable]
        public class Clouds
        {
            public int all;
        }
    }

    public TMP_Text weatherText; // Reference to the TextMeshPro UI component
    private WeatherData currentWeatherData;

    // Define the event that will notify subscribers when the weather data is updated
    public event Action OnWeatherDataUpdated;

    public void UpdateWeather(double latitude, double longitude)
    {
        Debug.Log($"Attempting to update weather for lat: {latitude}, lon: {longitude}");
        StartCoroutine(GetWeatherData(latitude, longitude));
    }

    IEnumerator GetWeatherData(double latitude, double longitude)
    {
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

        Debug.Log("Fetching weather data from: " + apiUrl);

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching weather data: " + request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("Weather data received: " + jsonResponse);

            currentWeatherData = JsonUtility.FromJson<WeatherData>(jsonResponse);

            // Update the text with the fetched weather data
            weatherText.text = $"Weather: {currentWeatherData.weather[0].main}\n" +
                               $"Description: {currentWeatherData.weather[0].description}\n" +
                               $"Temperature: {currentWeatherData.main.temp} K\n" +
                               $"Wind Speed: {currentWeatherData.wind.speed} m/s\n" +
                               $"Cloudiness: {currentWeatherData.clouds.all}%";
            Debug.Log("Updated weather info: " + weatherText.text);

            // Trigger the event to notify that the weather data has been updated
            OnWeatherDataUpdated?.Invoke();
        }
    }

    public string GetCurrentWeatherCondition()
    {
        if (currentWeatherData != null && currentWeatherData.weather.Length > 0)
        {
            return currentWeatherData.weather[0].description;
        }
        return "Unknown";
    }
}
