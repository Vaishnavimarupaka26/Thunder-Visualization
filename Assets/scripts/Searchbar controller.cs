using UnityEngine;
using UnityEngine.UI;

public class SearchBarController : MonoBehaviour
{
    public GameObject searchBar; // Reference to the search bar GameObject
    public Button showSearchButton; // Reference to the button

    void Start()
    {
        // Ensure the search bar is initially hidden
        searchBar.SetActive(false);

        // Add listener to the button to show the search bar when clicked
        showSearchButton.onClick.AddListener(ToggleSearchBar);
    }

    void ToggleSearchBar()
    {
        // Toggle the active state of the search bar
        searchBar.SetActive(!searchBar.activeSelf);
    }
}
