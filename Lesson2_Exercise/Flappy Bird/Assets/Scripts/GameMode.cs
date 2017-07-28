using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode instance; //实例化单例
    public bool gameOver = false;

    public Text scoreInfo;
    public GameObject gameOverInfo;

    int score = 0;

	void Awake ()
    {
        instance = this;
	}
	
	void Update ()
    {
		if(gameOver && Input.GetMouseButtonDown(0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Flappy Bird");
        }
	}

    public void Score()
    {
        if(gameOver)
        {
            return;
        }

        score += 1;
        scoreInfo.text = "SCORE: " + score.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverInfo.SetActive(true);
    }
}
