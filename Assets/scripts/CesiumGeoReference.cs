using UnityEngine;
using CesiumForUnity;

public class TestCesium : MonoBehaviour
{
    internal static object Math;
    public CesiumGeoreference cesiumGeoreference;

    void Start()
    {
        // Just test if it can be accessed
        if (cesiumGeoreference != null)
        {
            Debug.Log("CesiumGeoreference is available.");
        }
    }
}

