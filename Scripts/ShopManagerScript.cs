using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public BeansCollection beansCollection; // Reference to BeansCollection to access Bean
    public Canvas shopCanvas; // Reference to the shop canvas
    public int[,] shopItems = new int[5,5];
    public TextMeshProUGUI CoinsTXT;

    public Renderer playerRenderer;
    public Material defaultSkin;
    public Material greenSkin;
    public Material goldSkin;

    public PlayerMovement playerMovement; // Reference to the player's movement script

    private int beans;
    private bool isShopOpen = false;

    void Start()
    {
        // The shop is closed by default
        shopCanvas.enabled = false;

        // Load beans from PlayerPrefs (in case they were updated after a purchase)
        beans = PlayerPrefs.GetInt("Beans", beansCollection.Bean); // Load saved beans or default to BeansCollection value
        beansCollection.Bean = beans; // Sync beans value with BeansCollection

        CoinsTXT.text = "Beans: " + beans.ToString();

        // ID's
        shopItems[1,1] = 1; // Skin: Green
        shopItems[1,2] = 2; // Skin: Gold
        shopItems[1,3] = 3; // Jump Boost
        shopItems[1,4] = 4; // Coming soon

        // Price
        shopItems[2,1] = 10; // Price for green skin
        shopItems[2,2] = 20; // Price for gold skin
        shopItems[2,3] = 30; // Price for jump boost
        shopItems[2,4] = 0; // Nothing yet

        // Set player's initial skin (default)
        playerRenderer.material = defaultSkin;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }

    public void ToggleShop()
    {
        isShopOpen = !isShopOpen; // Toggle shop state
        shopCanvas.enabled = isShopOpen; // Show or hide the canvas

        if (isShopOpen == true)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;
            Time.timeScale = 0;
        } else {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor when shop is closed
            Time.timeScale = 1;
        }
    }

    
    public void Buy()
    {
        // Ensure this method only runs when the shop is open
        if (!isShopOpen) return; //to fix jump -50 beans issue


        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject; //reference to button & eventsystem

        beans = beansCollection.Bean;

        if (beans >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]) //check if can buy
        {
            beans -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID]; //subtract the beans
            beansCollection.Bean = beans; // Update the beans in BeansCollection
            PlayerPrefs.SetInt("Beans", beans); // Save the updated beans count
            PlayerPrefs.Save(); // Ensure it is saved to disk

            CoinsTXT.text = "Beans: " + beans.ToString();
            Debug.Log("Bought! " + ButtonRef.GetComponent<ButtonInfo>().ItemID);


            // If the player buys the "skin" item (ID 1)
            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 1)
            {
                ChangePlayerSkin(greenSkin);
            }

            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 2)
            {
                ChangePlayerSkin(goldSkin);
            }

            if (ButtonRef.GetComponent<ButtonInfo>().ItemID == 3)
            {
                UpgradeJump();
            }
        }
    }

    private void ChangePlayerSkin(Material newSkin)
    {
        playerRenderer.material = newSkin;
    }

    private void UpgradeJump()
    {
        playerMovement.jumpForce = 15; // Set jumpForce to 15 when jump boost is bought
        Debug.Log("Jump boost purchased! New jumpForce: " + playerMovement.jumpForce);
    }
}