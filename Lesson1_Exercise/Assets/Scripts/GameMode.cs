using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

	void Start ()
    {
	}
	
	void Update ()
    {
	}

    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lesson1_Exercise");
    }

}
