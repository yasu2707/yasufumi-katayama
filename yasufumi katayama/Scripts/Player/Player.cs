using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;

    private AudioSource audioSource;

    //[SerializeField] private AudioClip walk; //走ってる足音
    [SerializeField] private AudioClip landing; //着地音

    public bool onGround = true;     //地面に触れているかの判定
    public bool onTrampoline = true; //トランポリンに触れているかの判定
    public bool onCliff = false;      //崖に触れているかの判定

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player = GameObject.Find("Body");
        player.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Transform transform = this.transform;
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ground" && onCliff == false)
        {
            onGround = true; 
            audioSource.clip = landing;
            audioSource.PlayOneShot(audioSource.clip);
        }

        if (other.gameObject.CompareTag("Trampoline"))
        {
            onTrampoline = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ground" && onCliff == false)
        {
            onGround = true;
        }
        else if(other.gameObject.tag != "ground")
        {
            onGround = false;
        }

        if (other.gameObject.CompareTag("Trampoline"))
        {
            onTrampoline = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            onGround = false;
        }

        if (other.gameObject.CompareTag("Trampoline"))
        {
            onTrampoline = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            onCliff = true;
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onCliff = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onCliff = false;
        }
    }
}
