using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    public GameObject lightningPrefab;  // Reference to your lightning bolt prefab
    public int numberOfBolts = 30;      // Number of lightning bolts to spawn
    public float spawnRadius = 1000f;   // Radius within which to spawn the lightning bolts

    void Start()
    {
        SpawnLightningBolts();
    }

    public void SpawnLightningBolts()
    {
        for (int i = 0; i < numberOfBolts; i++)
        {
            Vector3 randomPosition = GenerateRandomPositionWithinRadius();
            Instantiate(lightningPrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GenerateRandomPositionWithinRadius()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        return transform.position + randomPosition;  // Adjust the y-axis (height) if needed
    }
}
