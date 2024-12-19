using UnityEngine;

public class ToggleCesiumOSMBuildings : MonoBehaviour
{
    public GameObject cesiumOSMBuildings; // Reference to the Cesium OSM Buildings GameObject

    // Method to toggle the visibility of the Cesium OSM Buildings
    public void ToggleBuildings()
    {
        if (cesiumOSMBuildings != null)
        {
            cesiumOSMBuildings.SetActive(!cesiumOSMBuildings.activeSelf);
        }
        else
        {
            Debug.LogWarning("Cesium OSM Buildings GameObject is not assigned!");
        }
    }
}

