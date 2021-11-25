using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1f;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
