using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float dethDelay = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoud());      
    }

    IEnumerator WaitAndLoud()
    {
        yield return new WaitForSeconds(dethDelay);
        SceneManager.LoadScene("Game Over");        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
