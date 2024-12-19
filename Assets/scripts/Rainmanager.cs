using UnityEngine;

public class RainManager : MonoBehaviour
{
    public ParticleSystem rainSystem;
    private GameObject activeGeoreference;

    public void SetRainSystemLocation(GameObject locationGeoreference)
    {
        activeGeoreference = locationGeoreference;
        Vector3 rainPosition = new Vector3(locationGeoreference.transform.position.x, locationGeoreference.transform.position.y, locationGeoreference.transform.position.z);
        rainSystem.transform.position = rainPosition;

        rainSystem.transform.localScale = new Vector3(47, 40, 125);
        rainSystem.Play();
    }

    public void StopRainSystem()
    {
        rainSystem.Stop();
    }
}
