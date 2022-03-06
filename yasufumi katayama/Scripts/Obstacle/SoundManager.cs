using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.PlayOneShot(sound.clip);

        Invoke("Destroy", 10);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
