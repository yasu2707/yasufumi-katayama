using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray_attack : MonoBehaviour
{
    public GameObject player;

    Rigidbody rb;

    public float attack_timer;
    public float attack_limitation;

    public bool attack_d;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Body");
        bool attack_d = player.GetComponent<Body>().direction;

        rb = GetComponent<Rigidbody>();

        if (player.GetComponent<Body>().direction == false)
        {
            rb.velocity = new Vector3(1, 0, 0);
        }

        if (player.GetComponent<Body>().direction == true)
        {
            rb.velocity = new Vector3(-1, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        attack_timer += Time.deltaTime;

        if (attack_limitation <= attack_timer)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
