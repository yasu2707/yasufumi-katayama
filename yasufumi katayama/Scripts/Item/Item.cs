using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject soundmaneger;
    [SerializeField] private AudioClip clip1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip1;

            Destroy(this.gameObject);
        }
    }
}
