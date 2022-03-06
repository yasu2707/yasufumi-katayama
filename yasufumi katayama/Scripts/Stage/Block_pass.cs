using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_pass : MonoBehaviour
{
    public GameObject prefab;
    public float Block_interval;

    float timer;

    private void Start()
    {
        timer = Block_interval;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > Block_interval)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
