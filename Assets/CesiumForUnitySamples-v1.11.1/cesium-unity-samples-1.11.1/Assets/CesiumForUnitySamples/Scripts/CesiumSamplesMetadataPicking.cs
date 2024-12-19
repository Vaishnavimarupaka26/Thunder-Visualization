using CesiumForUnity;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class CesiumSamplesMetadataPicking : MonoBehaviour
{
    public GameObject metadataPanel; // Use the Scroll View's Content Panel
    public Text metadataText;
    private Dictionary<String, CesiumMetadataValue> _metadataValues;

    public float crosshairRadius = 50f; // Adjust the crosshair radius as needed

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (metadataPanel != null)
        {
            metadataPanel.SetActive(false);
        }

        this._metadataValues = new Dictionary<String, CesiumMetadataValue>();
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        bool receivedInput = false;

        if (Mouse.current != null)
        {
            receivedInput = Mouse.current.leftButton.isPressed;
        }
        else if (Gamepad.current != null)
        {
            receivedInput = Gamepad.current.rightShoulder.isPressed;
        }
#else
        bool receivedInput = Input.GetMouseButtonDown(0);
#endif

        if (receivedInput && metadataText != null)
        {
            metadataText.text = String.Empty;

            RaycastHit[] hits;
            hits = Physics.SphereCastAll(
                Camera.main.transform.position,
                crosshairRadius,
                Camera.main.transform.TransformDirection(Vector3.forward),
                Mathf.Infinity);

            foreach (RaycastHit hit in hits)
            {
                CesiumPrimitiveFeatures features = hit.transform.GetComponent<CesiumPrimitiveFeatures>();
                CesiumModelMetadata metadata = hit.transform.GetComponentInParent<CesiumModelMetadata>();

                if (features != null && features.featureIdSets.Length > 0)
                {
                    CesiumFeatureIdSet featureIdSet = features.featureIdSets[0];
                    Int64 propertyTableIndex = featureIdSet.propertyTableIndex;
                    if (metadata != null && propertyTableIndex >= 0 && propertyTableIndex < metadata.propertyTables.Length)
                    {
                        CesiumPropertyTable propertyTable = metadata.propertyTables[propertyTableIndex];
                        Int64 featureID = featureIdSet.GetFeatureIdFromRaycastHit(hit);
                        propertyTable.GetMetadataValuesForFeature(this._metadataValues, featureID);

                        foreach (var valuePair in this._metadataValues)
                        {
                            string valueAsString = valuePair.Value.GetString();
                            if (!String.IsNullOrEmpty(valueAsString) && valueAsString != "null")
                            {
                                metadataText.text += "<b>" + valuePair.Key + "</b>" + ": " + valueAsString + "\n";
                            }
                        }
                        metadataText.text += "\n"; // Add spacing between building information
                    }
                }
            }

            if (metadataPanel != null)
            {
                metadataPanel.SetActive(metadataText.text.Length > 0);
            }
        }
    }
}