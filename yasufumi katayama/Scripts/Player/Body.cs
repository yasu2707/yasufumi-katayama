using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    private GameObject pauseUIPrefab;
    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip walk;

    GameObject player;
    GameObject Bottom;
    GameObject stageTimer;
    public GameObject[] S_smoke;
    public GameObject barrier;
    public GameObject spray_attack;
    public GameObject tr_spray;
    public GameObject tl_spray;


    Rigidbody rb;
    Animator animator;
    BoxCollider box;
    SpriteRenderer sr;

    public float moveSpeed = 5f;
    public float jumpPower = 10f;
    public float timeOut;
    public float timeElapsed = 0.0f;
    public float s_count;

    public bool p_crash = false;
    public bool crash = false;
    public bool move_right = false;
    public bool move_left = false;
    public bool direction = false;
    public bool tired = false;

    Vector2 vector2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        player = GameObject.Find("Player");
        player.GetComponent<Player>().onGround = true;
        player.GetComponent<Player>().onTrampoline = false;

        Bottom = GameObject.Find("bottom_trigger");
        Bottom.GetComponent<Bottom>().fall = false;

        stageTimer = GameObject.Find("UI");
        stageTimer.GetComponent<Timer>().gamestop = false;

        animator.SetInteger("Player", 0);
        animator.SetBool("Crash", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Landing", false);
        animator.SetBool("Goal", false);
        animator.SetBool("Tired", false);
        animator.SetBool("Spray", false);
        animator.SetBool("Sliding", false);

        Time.timeScale = 1f;
    }

    void LateUpdate()
    {
        if (stageTimer.GetComponent<Timer>().gamestop == true)
        {
            // 時間切れアニメーション
            animator.SetBool("Crash", true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUIInstance == null)
            {
                if (Time.timeScale == 1f)
                {
                    pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                    Time.timeScale = 0f;
                }
            }
            else
            {
                Destroy(pauseUIInstance);
                Time.timeScale = 1f;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Time.timeScale == 1f && tired == false && s_count >= 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Sliding"))
            {
                animator.SetBool("Spray", true);
                audioSource.clip = attack;
                audioSource.PlayOneShot(audioSource.clip);

                if (sr.flipX == false)
                {
                    Instantiate(spray_attack, tr_spray.transform.position, transform.rotation);
                    direction = false;
                }
                else if (sr.flipX == true)
                {
                    Instantiate(spray_attack, tl_spray.transform.position, transform.rotation);
                    direction = true;
                }

                s_count = s_count -= 1f;
                    
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) // スライディング
        {
            if ( animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && Time.timeScale == 1) //スライディング可能か確認
            {
                // スライディングアニメーションを適応
                animator.SetBool("Sliding", true);
                // スライディング中の移動速度を取得
                vector2 = rb.velocity;
                vector2 *= 1.2f;
                vector2.y = -2.0f;
            }
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Sliding")) // スライディング中の処理
        {
            // 当たり判定を半分にする
            box.center = new Vector3(0, -0.4f, 0);
            box.size = new Vector3(1.5f, 0.75f, 0.2f);

            if(sr.flipX == true) // 左向き
            {
                S_smoke[1].GetComponent<SpriteRenderer>().enabled = true;
            }
            else if(sr.flipX == false) // 右向き
            {
                S_smoke[0].GetComponent<SpriteRenderer>().enabled = true;
            }

            // 移動速度
            rb.velocity = vector2;
        }
        else // 通常状態の当たり判定
        {
            S_smoke[0].GetComponent<SpriteRenderer>().enabled = false;
            S_smoke[1].GetComponent<SpriteRenderer>().enabled = false;

            box.center = new Vector3(0, -0.05f, 0);
            box.size = new Vector3(0.4f, 1.4f, 0.2f);
        }

        //if (Input.GetMouseButtonDown(2))
        //{
        //    Instantiate(barrier, transform.position, Quaternion.identity);

        //}

        if (player.GetComponent<Player>().onGround == true) // 地面判定がtrue時の処理
        {
            // 着地時から時間を計測
            timeElapsed += Time.deltaTime;

            // ジャンプアニメーション解除
            animator.SetBool("Jump", false);

            if (timeElapsed >= 0.6f && rb.isKinematic == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Sliding")) // 疲労状態ではないかを確認
            {
                // Vec2で横移動の値を取得
                Vector2 v = rb.velocity;
                v.x = Input.GetAxis("Horizontal") * moveSpeed;
                
                if (Input.GetKey(KeyCode.Z) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) // スペースキーでジャンプ処理
                {
                    // ジャンプ処理
                    v.y = jumpPower;
                    
                    // ジャンプのSE再生
                    audioSource.PlayOneShot(jump);

                    // ジャンプアニメーションを適応
                    animator.SetBool("Jump", true);
                }

                // Vec2の値を返す
                rb.velocity = v;
            }

            if (timeElapsed <= 0.6f) // 時間経過で疲労状態の判定をとる
            {
                rb.isKinematic = true;
                animator.SetBool("Tired", true);
                tired = true;
            }
            else
            {
                rb.isKinematic = false;
                animator.SetBool("Tired", false);
                tired = false;
            }

            if (rb.isKinematic == false && Time.timeScale == 1f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Sliding" +
                "" +
                "")) // 入力可能か確認
            {
                if (move_right == false && Input.GetKey(KeyCode.LeftArrow)) // 左だけが入力されている状態か確認
                {
                    // 左に移動中の判定
                    move_left = true;

                    // 左を向いて走っているアニメーション
                    sr.flipX = true;
                    animator.SetInteger("Player", 1);
                    return;
                }

                if (move_left == false && Input.GetKey(KeyCode.RightArrow)) // 右だけが入力されている状態か確認
                {
                    // 右に移動中の判定
                    move_right = true;

                    // 右を向いて走っているアニメーション
                    sr.flipX = false;
                    animator.SetInteger("Player", 1);
                    return;
                }
            }

            // 移動中の判定を初期化
            move_right = false;
            move_left = false;
            
            animator.SetInteger("Player", 0);
            return;
        }

        if (player.GetComponent<Player>().onGround == false) // 地面判定がfalse時の処理
        {
            // 計測を初期化
            timeElapsed = 0.0f;

            if(rb.isKinematic == true)
            {
                rb.isKinematic = false;
            }
            // ジャンプアニメーションを適応
            //animator.SetBool("Jump", true);
        }

        if (player.GetComponent<Player>().onTrampoline == true) // トランポリン判定がtrue時の処理
        {
            // ジャンプアニメーション解除
            animator.SetBool("Jump", false);

            if (rb.isKinematic == false && Time.timeScale == 1f)
            {
                Vector2 v = rb.velocity;
                v.x = Input.GetAxis("Horizontal") * moveSpeed;

                if (Input.GetKey(KeyCode.Z))
                {
                    v.y = jumpPower * 1.5f;
                }

                rb.velocity = v;

                if (move_right == false && Input.GetKey(KeyCode.LeftArrow)) // 左だけが入力されている状態か確認
                {
                    // 左に移動中の判定
                    move_left = true;

                    // 左を向いて走っているアニメーション
                    sr.flipX = true;
                    animator.SetInteger("Player", 1);
                    return;
                }

                if (move_left == false && Input.GetKey(KeyCode.RightArrow)) // 右だけが入力されている状態か確認
                {
                    // 右に移動中の判定
                    move_right = true;

                    // 右を向いて走っているアニメーション
                    sr.flipX = false;
                    animator.SetInteger("Player", 1);
                    return;
                }
            }

            // 移動中の判定を初期化
            move_right = false;
            move_left = false;

            animator.SetInteger("Player", 0);
            return;
        }

        if (player.GetComponent<Player>().onTrampoline == false) // トランポリン判定がfalse時の処理
        {
            animator.SetBool("Jump", true);
        }

        if (Bottom.GetComponent<Bottom>().fall == true) 
        {
            // 落下アニメーション
            animator.SetBool("Landing", true);
        }

    }
    //--------------------------------------------------------------------
    //--------------------------------------------------------------------
    //--------------------------------------------------------------------
    //--------------------------------------------------------------------
    //--------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            animator.SetBool("Crash", true);

            rb.isKinematic = true;

            crash = true;
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            animator.SetBool("Goal", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("Crash", true);

            rb.isKinematic = true;

            crash = true;
        } 
        
        if(other.gameObject.CompareTag("Gameover"))
        {
            animator.SetBool("Crash", true);

            rb.isKinematic = true;

            crash = true;
        }

        if (other.gameObject.CompareTag("Barrier"))
        {
            Instantiate(barrier, transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("Spray"))
        {
            s_count = s_count + 1f;
        }
    }
}
