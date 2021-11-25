using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_body : MonoBehaviour //エネミーの当たり判定のみを渡す
{
    public bool Player_On = false;
    public bool Barrier_On = false;
    public bool Spray_On = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_On = true;
        }
        else if (other.gameObject.CompareTag("Barrier"))
        {
            Barrier_On = true;
        }
        else if (other.gameObject.CompareTag("Spray"))
        {
            Spray_On = true;
        }
    }
}
