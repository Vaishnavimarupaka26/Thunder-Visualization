using UnityEngine;
using CesiumForUnity;

public class OSMMetadataProcessor : MonoBehaviour
{
    public Cesium3DTileset cesiumTileset;

    void Start()
    {
        if (cesiumTileset == null)
        {
            Debug.LogError("Cesium3DTileset is not assigned!");
            return;
        }
    }

    void Update()
    {
        ProcessMetadata();
    }

    void ProcessMetadata()
    {
        foreach (Transform child in cesiumTileset.transform)
        {
            SearchForMetadata(child);
        }
    }

    void SearchForMetadata(Transform parent)
    {
        Debug.Log(parent);
        // Check if the current transform has CesiumPrimitiveFeatures
        CesiumPrimitiveFeatures features = parent.GetComponent<CesiumPrimitiveFeatures>();
        if (features != null)
        {
            Debug.Log($"Found features on: {parent.name}");
            // You can access and process the metadata here
        }

        // Recursively search for features in child objects
        foreach (Transform child in parent)
        {
            SearchForMetadata(child);
        }
    }
}
