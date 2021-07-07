using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingelton();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpSingelton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length; // обратить внимание на FindObjectsOfType там есть s
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    
    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
