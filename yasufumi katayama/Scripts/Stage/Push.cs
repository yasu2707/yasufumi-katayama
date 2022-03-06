using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public GameObject soundmaneger;

    [SerializeField] private AudioClip clip1;

    public float y_power;
    public float x_power;

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(new Vector3(1 * x_power, 1 * y_power, 0), ForceMode.Impulse);
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip1;
        }
    }
}
