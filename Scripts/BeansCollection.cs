using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BeansCollection : MonoBehaviour
{
    public int Bean = 0;
    public int sessionBeans = 0;
    public int HighScore = 0;
    public TextMeshProUGUI beanCounter;
    public TextMeshProUGUI highScoreCounter;

    private void Start()
    {
        // Load total beans and high score from PlayerPrefs
        Bean = PlayerPrefs.GetInt("Beans", 0); 
        HighScore = PlayerPrefs.GetInt("HighScore", 0); // Load the saved high score or default to 0

        // At the start of the game, reset the session beans to 0
        sessionBeans = 0;

        UpdateBeanCounter();
        UpdateHighScoreCounter();
    }

    private void UpdateBeanCounter()
    {
        beanCounter.text = "Beans: " + Bean.ToString(); //update de text UI
    }

    private void UpdateHighScoreCounter()
    {
        highScoreCounter.text = "High Score: " + HighScore.ToString(); // Update the high score UI
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bean")
        {
            Bean ++;
            sessionBeans++;
            UpdateBeanCounter();
            Debug.Log("Bean collected");
            Destroy(other.gameObject);

            // Save beans
            PlayerPrefs.SetInt("Beans", Bean);

            if (sessionBeans > HighScore)
            {
                HighScore = sessionBeans;
                PlayerPrefs.SetInt("HighScore", HighScore);
                UpdateHighScoreCounter();
            }

            PlayerPrefs.Save();
        }
    }
}