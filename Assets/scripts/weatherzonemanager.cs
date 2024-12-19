using UnityEngine;

public class WeatherZoneManager : MonoBehaviour
{
    public GameObject zone; // The Quad or Plane representing the area
    public Material redZoneMaterial;
    public Material yellowZoneMaterial;
    public Material greenZoneMaterial;
    public Material colorlessMaterial; // New material for default colorless state

    void Start()
    {
        // Set the zone to colorless by default
        zone.GetComponent<Renderer>().material = colorlessMaterial;

        // Uncomment one of these lines to test:
        UpdateZoneMaterial("Thunderstorm"); // Should turn the zone red
        //UpdateZoneMaterial("Rain");         // Should turn the zone yellow
        //UpdateZoneMaterial("Clear");        // Should turn the zone green
    }

    public void UpdateZoneMaterial(string condition)
    {
        if (condition.Contains("Thunderstorm"))
        {
            zone.GetComponent<Renderer>().material = redZoneMaterial;
        }
        else if (condition.Contains("Rain"))
        {
            zone.GetComponent<Renderer>().material = yellowZoneMaterial;
        }
        else
        {
            zone.GetComponent<Renderer>().material = greenZoneMaterial;
        }
    }
}


