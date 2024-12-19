using System.Collections.Generic;
using UnityEngine;

public class BuildingHighlighter : MonoBehaviour
{
    public Material highlightMaterial; // The material to apply for highlighting
    public float highlightRadius = 100.0f; // Radius within which to highlight buildings

    private List<Renderer> originalRenderers = new List<Renderer>();
    private List<Material[]> originalMaterials = new List<Material[]>();

    void Start()
    {
        // Find all game objects with the "CesiumOSMBuilding" tag within the specified radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, highlightRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("CesiumOSMBuilding"))
            {
                Renderer renderer = collider.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Store the original materials for restoration later
                    originalRenderers.Add(renderer);
                    originalMaterials.Add(renderer.materials);

                    // Apply the highlight material
                    Material[] newMaterials = new Material[renderer.materials.Length];
                    for (int i = 0; i < newMaterials.Length; i++)
                    {
                        newMaterials[i] = highlightMaterial;
                    }
                    renderer.materials = newMaterials;
                }
            }
        }
    }

    void OnDestroy()
    {
        // Restore the original materials when the script is destroyed
        for (int i = 0; i < originalRenderers.Count; i++)
        {
            originalRenderers[i].materials = originalMaterials[i];
        }
    }
}


