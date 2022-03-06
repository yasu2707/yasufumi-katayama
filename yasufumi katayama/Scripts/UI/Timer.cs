using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseUIPrefab;
    private GameObject pauseUIInstance;
    [SerializeField]
    private float maxSecond;
    [SerializeField]
    private Text timeText;

    public bool gamestop = false;

    Transform Sound_BGM;
    AudioSource BGM;
    AudioClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        Sound_BGM = transform.Find("BGM");
        BGM = Sound_BGM.GetComponent<AudioSource>();
        //BGM.clip = clip[0];
        BGM.Play();

        timeText.text = maxSecond.ToString("000");
        timeText.fontSize = 100;
    }

    // Update is called once per frame
    void Update()
    {
        maxSecond -= Time.deltaTime;
        timeText.GetComponent<Animator>().SetFloat("Timer", maxSecond);

        if (maxSecond >= 0)
        {
            timeText.text = maxSecond.ToString("000");


        }
        else if(pauseUIInstance == null)
        {
            pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            gamestop = true;
        }

        if (maxSecond <= 20)
        {
            timeText.color = new Vector4(255, 0, 0, 255);
            BGM.pitch = 1.5f;
        }
        else
        {
            timeText.color = new Vector4(0, 0, 0, 255);
        }

    }
}
