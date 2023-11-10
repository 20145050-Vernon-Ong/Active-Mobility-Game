using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class spawncoins : MonoBehaviour
{
    public GC script;
    public Button activateButton;
    public GameObject gemText;
    public GameObject phoneinfo;
    public GameObject[] gameObjectsToActivate;
    public GameObject compass;
    public int requiredCoins;
    public Color fullOpacityColor = Color.white;
    public Color lowOpacityColor = new Color(1f, 1f, 1f, 0.5f);
    //public AudioSource activationSound; // Reference to the AudioSource for the activation sound

    private bool buttonClicked = false;
    private int userCoins;

    private void Start()
    {
        // Attach a listener to the button's onClick event
        activateButton.onClick.AddListener(ActivateGameObjects);
        
        // Initialize the button's color based on the user's coin count
        UpdateButtonColor();
    }

    private void Update()
    {
        // Update the userCoins value in every frame
        userCoins = script.totalpoints;
        
        // Update the button's color based on the user's coin count
        UpdateButtonColor();
    }

private void ActivateGameObjects()
{
    // Check if the button has already been clicked and if the user has enough coins
    if (!buttonClicked && userCoins >= requiredCoins)
    {
        Debug.Log("Button clicked!");

            // Play the activation sound
            //activationSound.Play();
        compass.SetActive(true);
        gemText.SetActive(true);
        // Loop through the array and set each game object to active (true)
        Debug.Log("Deducting coins. userCoins before: " + userCoins);

        // Deduct the required coins from the user's total
        userCoins -= requiredCoins;

        Debug.Log("Deducting coins. userCoins after: " + userCoins);

        // Update the UI to reflect the new number of coins
        // UpdateCoinCounter();

        script.SetTotalPoints(userCoins);
        script.AddPoints(-requiredCoins);
        // Disable the button to prevent further clicks until the user collects more coins
        activateButton.interactable = false;

        // Update the flag to indicate that the button has been clicked
        buttonClicked = true;

        // Update the button's color to the clicked color
        activateButton.image.color = lowOpacityColor;

        phoneinfo.SetActive(false);
    }
}


    // Call this method to update the button's color based on the user's coin count
    private void UpdateButtonColor()
    {
        if (!buttonClicked)
        {
            if (userCoins >= requiredCoins)
            {
                activateButton.interactable = true; // Make the button clickable
                activateButton.image.color = fullOpacityColor;
            }
            else
            {
                activateButton.interactable = false; // Make the button non-clickable
                activateButton.image.color = lowOpacityColor;
            }
        }
        else
        {
            // The button has been clicked, use the clicked color
            activateButton.interactable = false;
            activateButton.image.color = lowOpacityColor;
        }
    }
}




