using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rb;

    Transform e_body; //当たり判定をとる子オブジェクトを格納

    // 子オブジェクトの当たり判定を格納
    public bool P_On = false;
    public bool B_On = false;
    public bool S_On = false;

    //0か1で判定
    int P_Permit = 0; 
    int B_Permit = 0; 
    int S_Permit = 0; 


    [SerializeField]
    //　ゲームオーバーした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ゲームオーバーUIのインスタンス
    private GameObject pauseUIInstance;


    // エネミー自体が消えてもSEを残すために別オブジェクトでSE再生.
    public GameObject soundmaneger;

    // プレイヤーを倒した時のSE
    [SerializeField] private AudioClip clip1;
    // エネミー消滅時のSE
    [SerializeField] private AudioClip clip2; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 当たり判定をとる子オブジェクトを取得
        e_body = transform.Find("Enemy_body");

        // エネミーの移動速度を固定
        rb.velocity = new Vector3(-1, 0, 0);
    }

    void Update()
    {
        // 常に判定を取得
        P_On = e_body.GetComponent<Enemy_body>().Player_On;
        B_On = e_body.GetComponent<Enemy_body>().Barrier_On;
        S_On = e_body.GetComponent<Enemy_body>().Spray_On;

        if (P_On == true && P_Permit == 0) // プレイヤーと衝突時の処理
        {
            rb.isKinematic = true;

            // clip1を再生
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip1;

            // ゲームオーバーUIを表示
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
            }

            P_Permit++;
        }

        if(B_On == true && B_Permit == 0) // バリアと衝突時の処理
        {
            // clip2を再生
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip2;

            B_Permit++;

            // エネミー消滅
            Destroy(this.gameObject);
        }

        if (S_On == true && S_Permit == 0) // スプレーと衝突時の処理
        {
            // clip2を再生
            GameObject temp = Instantiate(soundmaneger, transform.position, Quaternion.identity);
            temp.GetComponent<AudioSource>().clip = clip2;

            S_Permit++;

            // エネミー消滅
            Destroy(this.gameObject);
        }
    }
}
