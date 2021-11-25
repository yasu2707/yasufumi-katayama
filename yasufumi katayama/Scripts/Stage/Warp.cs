using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform destination;

    AudioSource se;

    void Start()
    {
        se = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = destination.position;

        collision.rigidbody.velocity = Vector3.zero;
        collision.rigidbody.angularVelocity = Vector3.zero;

        if (se)
        {
            se.Play();
        }
    }
}
