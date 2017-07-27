using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public GameObject resetButton;

	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        resetButton.SetActive(true);
    }

    public void OnClickButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson1_Exercise");
    }
}
