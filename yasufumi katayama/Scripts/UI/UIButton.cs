using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public string[] SceneName;
    public Button[] Buttons;
    public Image[] images;

    int SelectionNumber = 0;
    public int ButtonNumber;

    [SerializeField] private AudioClip clip1; // 決定SE
    [SerializeField] private AudioClip clip2; // カーソル移動SE
    [SerializeField] private AudioClip clip3; // メニュー開くSE
    [SerializeField] private AudioClip clip4; // ゲーム終了画面SE

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clip3);

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title" && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;

            audioSource.PlayOneShot(clip1);

            SceneManager.LoadScene("stage_selection");
        }


        // ステージ選択画面でのゲーム終了画面処理

        if (SceneManager.GetActiveScene().name == "stage_selection")
        {
            if (images[0].IsActive())
            {
                switch (SelectionNumber)
                {
                    case 0:
                        Buttons[6].transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
                        Buttons[5].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            audioSource.PlayOneShot(clip1);

                            Application.Quit();
                            return;
                        }

                        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            audioSource.PlayOneShot(clip2);

                            SelectionNumber = 1;
                            return;
                        }
                        break;

                    case 1:
                        Buttons[6].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        Buttons[5].transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);

                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            SelectionNumber = 0;
                            audioSource.PlayOneShot(clip1);

                            images[0].gameObject.SetActive(false);
                            return;
                        }

                        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            audioSource.PlayOneShot(clip2);

                            SelectionNumber = 0;
                            return;
                        }
                        break;
                }

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (images[0].IsActive())
                {
                    SelectionNumber = 0;
                    images[0].gameObject.SetActive(false);

                    return;
                }

                if (!images[0].IsActive())
                {
                    SelectionNumber = 0;
                    images[0].gameObject.SetActive(true);

                    audioSource.PlayOneShot(clip4);

                    return;
                }
            }
        }
        //------------------------------------------------------------------------
        if (ButtonNumber == 2)
        {
            GameObject gameObject = GameObject.Find("Q(Clone)");
            if (Input.GetKeyDown(KeyCode.Escape) && gameObject == null)
            {
                Application.Quit();
            }

            switch (SelectionNumber)
            {
                case 0:

                    Buttons[0].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Scene loadScene = SceneManager.GetActiveScene();
                        SceneManager.LoadScene(loadScene.name);
                        Time.timeScale = 1f;

                        audioSource.PlayOneShot(clip1);
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 1;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 1;
                    }
                    break;

                case 1:

                    Buttons[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[0]);
                        audioSource.PlayOneShot(clip1);
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 0;
                    }

                    break;
            }
            return;
        }

        if(ButtonNumber == 5 && !images[0].IsActive())
        {
            switch (SelectionNumber)
            {
                case 0:

                    Buttons[0].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[3].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[4].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[0]);

                        audioSource.PlayOneShot(clip1);
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 4;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 1;
                    }
                    break;

                case 1:

                    Buttons[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
                    Buttons[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[3].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[4].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[1]);
                        audioSource.PlayOneShot(clip1);
                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 0;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 2;
                    }
                    break;

                case 2:

                    Buttons[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[2].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
                    Buttons[3].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[4].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[2]);
                        audioSource.PlayOneShot(clip1);

                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 1;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 3;
                    }
                    break;

                case 3:

                    Buttons[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[3].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);
                    Buttons[4].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[3]);
                        audioSource.PlayOneShot(clip1);

                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 2;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 4;
                    }
                    break;

                case 4:

                    Buttons[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[2].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[3].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    Buttons[4].transform.localScale = new Vector3(1.4f, 1.4f, 1.0f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(SceneName[4]);
                        audioSource.PlayOneShot(clip1);

                    }

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 3;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        audioSource.PlayOneShot(clip2);

                        SelectionNumber = 0;
                    }
                    break;
            }
            return;
        }
    }


    //public void OnClick()
    //{
    //        SceneManager.LoadScene(SceneName);
    //    Time.timeScale = 1f;
    //    audioSource.Play();
    //}
}
