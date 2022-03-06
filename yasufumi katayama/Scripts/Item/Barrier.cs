using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    SpriteRenderer sr;

    public GameObject player;
    public float bl_timer;
    float b_timer;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Body");
        b_timer = bl_timer;
    }

    // Update is called once per frame
    void Update()
    {
        float color_alpha = bl_timer / b_timer;
        bl_timer -= Time.deltaTime;

        sr.color = new Vector4(255f, 255f, 255f, color_alpha);

        Transform transform = this.transform;
        transform.position = player.transform.position;

        if(0 >= bl_timer)
        {
            Destroy(this.gameObject);
        }
    }
}
