using UnityEngine;
using TMPro; // Required for TextMeshPro components

public class ToggleTMPTextVisibility : MonoBehaviour
{
    public TextMeshProUGUI myText; // Reference to the TextMeshProUGUI component

    // Method to toggle the visibility of the text
    public void ToggleText()
    {
        myText.gameObject.SetActive(!myText.gameObject.activeSelf);
    }
}
