using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Itemcount : MonoBehaviour
{
    public GameObject player;

    public float i_count = 0f;
    [SerializeField]
    private Text counttext;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Body");
        float i_count = player.GetComponent<Body>().s_count;

        counttext.text = i_count.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        float i_count = player.GetComponent<Body>().s_count;

        counttext.text = i_count.ToString("0");
    }
}
