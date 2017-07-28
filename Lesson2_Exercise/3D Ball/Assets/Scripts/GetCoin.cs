using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
	void Update ()
    {
        transform.Rotate(Vector3.up, 30 * Time.deltaTime);
	}

    /// <summary>
    /// 将硬币设置为触发器
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCharacter>();
        if(player)
        {
            player.AddCoin();
            transform.gameObject.SetActive(false);
        }
    }
}
