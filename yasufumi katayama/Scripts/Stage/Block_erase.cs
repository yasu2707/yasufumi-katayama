using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_erase : MonoBehaviour
{
    GameObject block;

    private void Start()
    {
        block = GameObject.Find("Block");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            Destroy(block);
        }
    }
}
