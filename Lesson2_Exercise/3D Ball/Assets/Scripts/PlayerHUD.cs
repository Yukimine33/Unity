using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUD : MonoBehaviour
{
    PlayerCharacter player;
    public Text countCoin;
    public Text playerDied;
    public Button reset;

	void Start ()
    {
        player = FindObjectOfType<PlayerCharacter>();
	}
	
	void Update ()
    {
        countCoin.text = "Coin: " + player.coinNum.ToString();
        if (player.isAlive == false)
        {
            playerDied.text = "You Have Died";
        }
	}

    public void GameOver()
    {
        reset.gameObject.SetActive(true);
        reset.onClick.AddListener(OnClick_ResetButton);
    }

    public void OnClick_ResetButton()
    {
        SceneManager.LoadScene("3DBall");
    }
}
