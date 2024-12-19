using System.Collections.Generic;
using UnityEngine;

public class ShelterLoader : MonoBehaviour
{
    public TextAsset shelterData; // CSV file with shelter data
    public GameObject shelterMarkerPrefab; // Prefab for shelter markers
    public GameObject cesiumGeoreference; // The CesiumGeoreference game object

    void Start()
    {
        LoadShelters();
    }

    public void LoadShelters()
    {
        if (shelterData == null || shelterMarkerPrefab == null || cesiumGeoreference == null)
        {
            Debug.LogError("Shelter data, prefab, or CesiumGeoreference is not assigned.");
            return;
        }

        string[] dataLines = shelterData.text.Split('\n');
        foreach (var line in dataLines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] data = line.Split(',');

            double latitude = double.Parse(data[0]);
            double longitude = double.Parse(data[1]);
            double height = 0; // Assume height is 0 for simplicity

            // Convert the latitude and longitude to Unity world space
            Vector3 worldPosition = LatLonToUnityWorldPosition(latitude, longitude, height);

            // Create a GameObject for the marker
            GameObject marker = Instantiate(shelterMarkerPrefab);

            // Set the marker's position relative to the CesiumGeoreference object
            marker.transform.position = worldPosition;
        }
    }

    private Vector3 LatLonToUnityWorldPosition(double latitude, double longitude, double height)
    {
        // Reference position (UCF Orlando, or the specific CesiumGeoreference origin)
        double refLat = 28.6024; // Example latitude
        double refLon = -81.2001; // Example longitude

        // Approximate conversion to Unity world coordinates
        float scale = 1000f; // Scale factor for better visibility

        float xPos = (float)((longitude - refLon) * scale);
        float zPos = (float)((latitude - refLat) * scale);
        float yPos = (float)height;

        return new Vector3(xPos, yPos, zPos);
    }
}



