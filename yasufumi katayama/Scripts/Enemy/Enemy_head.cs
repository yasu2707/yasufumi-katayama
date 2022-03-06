using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_head : MonoBehaviour
{
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(enemy);
        }
    }
}
