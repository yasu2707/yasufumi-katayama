using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_down : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.Find("Body");
        body.GetComponent<Body>().crash = false;
    }

    private void Update()
    {
       
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            if (body.GetComponent<Body>().crash == true)
            {
                if (pauseUIInstance == null)
                {
                    pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                }
            }
        }
    }
}
