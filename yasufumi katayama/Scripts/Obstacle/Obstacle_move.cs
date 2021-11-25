using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_move : MonoBehaviour
{
    GameObject body;
    public GameObject soundmaneger;

    Rigidbody rb;

    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private AudioClip clip3;

    public float time = 0.0f;

    int Decision_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(-4, -6, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 20f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Decision_count == 0)
        {
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip2;
            rb.isKinematic = true;
            Decision_count++;
        }
        else
        {
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip1;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrier") || other.gameObject.CompareTag("Spray"))
        {
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip3;
            Destroy(this.gameObject);
        }
    }
}
