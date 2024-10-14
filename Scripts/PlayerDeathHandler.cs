using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeathHandler : MonoBehaviour
{
    public float fallThreshold = -10f;
    public GameObject deathUI;

    private void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f; // Stop the game
        deathUI.SetActive(true); // Show death UI
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
        //Debug.Log("You died!");
    }

   public void RestartGame()
    {
        Debug.Log("Restarting Game");
        // Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
    }

   public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit(); // This will only work in the built game, not in unity editor
    }
}
