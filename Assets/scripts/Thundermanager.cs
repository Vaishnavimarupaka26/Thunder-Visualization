using DigitalRuby.LightningBolt;
using UnityEngine;
using System.Collections;

public class ThunderManager : MonoBehaviour
{
    public GameObject lightningPrefab; // Prefab for the lightning bolt
    private GameObject activeGeoreference; // Reference to the active georeference
    public int numberOfBolts = 10; // Number of lightning bolts to spawn
    public float lightningAreaRadius = 1000f; // Radius of the area to cover with lightning
    public float spawnInterval = 2f; // Time in seconds between each lightning bolt spawn

    void Start()
    {
        // Set the thunder system to the initial location
        SetThunderSystemLocation(gameObject);

        // Start the thunderstorm effect
        StartCoroutine(SpawnLightningSequence());
    }

    private IEnumerator SpawnLightningSequence()
    {
        for (int i = 0; i < numberOfBolts; i++)
        {
            SpawnLightningBolt();
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before the next bolt
        }
    }

    private void SpawnLightningBolt()
    {
        if (activeGeoreference == null)
        {
            Debug.LogWarning("Active georeference is not set.");
            return;
        }

        // Generate random positions within the specified area
        Vector3 startPosition = GenerateRandomPositionWithinArea();
        Vector3 endPosition = GenerateRandomPositionWithinArea();

        // Instantiate the lightning bolt prefab
        GameObject lightningInstance = Instantiate(lightningPrefab, startPosition, Quaternion.identity);

        // Configure the lightning bolt positions
        LightningBoltScript lightningBolt = lightningInstance.GetComponent<LightningBoltScript>();
        lightningBolt.StartPosition = startPosition;
        lightningBolt.EndPosition = endPosition;

        // Trigger the lightning bolt
        lightningBolt.Trigger();
    }

    private Vector3 GenerateRandomPositionWithinArea()
    {
        Vector2 randomCircle = Random.insideUnitCircle * lightningAreaRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        return activeGeoreference.transform.position + randomPosition;
    }

    public void SetThunderSystemLocation(GameObject locationGeoreference)
    {
        activeGeoreference = locationGeoreference;
        Debug.Log($"Thunder system location set to: {locationGeoreference.name}");
    }

    public void StopThunders()
    {
        StopAllCoroutines();
    }
}
