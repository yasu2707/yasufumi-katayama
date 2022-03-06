using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_horizontal : MonoBehaviour
{
    Rigidbody rb;

    public float block_timer = 0.0f;
    public float re_block;
    public float blocktime_limitation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        block_timer += Time.deltaTime;

        if (block_timer <= re_block)
        {
            rb.velocity = new Vector3(1, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(-1, 0, 0);
        }

        if(block_timer >= blocktime_limitation)
        {
            block_timer = 0.0f;
        }
    }
}
