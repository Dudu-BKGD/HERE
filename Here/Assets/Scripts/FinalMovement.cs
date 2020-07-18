using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HedgehogTeam.EasyTouch;
using Unity.Mathematics;


public class FinalMovement : MonoBehaviour
{
    public EasyTouch ETC;

    public static int cherryNum = 0;

    public Text endText;

    public Text cherryText;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public Sprite wallSpriteRenderer;

    public Camera cam;
   
    public GameObject dashDie;
    public GameObject back1;
    public GameObject back2;
    public GameObject star;
    public GameObject boom;

    public Button zant;
    public float speed, jumpForce;
    private float horizontalMove;
    private float verticalMove;
    private  int dashDirection = 1;

    public  Transform groundCheck;
    public LayerMask ground;

    [Header("Dash参数")]
    public float dashTime;//冲锋时间
    private float dashTimeLeft;//冲锋剩余时间
    private float lastDash = -10;//上次冲锋时间
    public float dashCoolDown;
    public float dashSpeed;//冲锋速度
    public float dashupspeed;

    public bool isGround, isJump, isDashing, isUpDashing,isWall,isCam,isdash1,isdash2,isTing;
    public bool IsEnd = false;

    bool jumpPressed;
    public int jumpCount;
    public int dashCount;
    public int upDashCount;



    public void OnMove(Vector2 vec)
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(vec.y * speed,0);
      
        horizontalMove = vec.x;
        verticalMove = vec.y;
        Debug.Log(verticalMove);
    }

    public void right()
    {
        Input.GetKey(KeyCode.D);
    }

    void Start()
    {
       
        isdash1 = true;
        isdash2 = true;
        isCam = false;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);//运行到这，暂停t秒

        //t秒后，继续运行下面代码
        star.SetActive(true);
       
    }
    IEnumerator WaitBgm(float t)
    {
        yield return new WaitForSeconds(t);//运行到这，暂停t秒

        //t秒后，继续运行下面代码
        Audio.Instance.PlaySound("Home");
    }

    IEnumerator WaitEnd(float t)
    {
        yield return new WaitForSeconds(t);//运行到这，暂停t秒

        //t秒后，继续运行下面代码
        SceneManager.LoadScene("Scenes/Open");
    }



    void Update()
    {
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }



        cherryText.text = ("CHERRY:" + cherryNum);

        if (isCam)
        {
          
            if (transform.position.x < 39)
            {
               
                cam.transform.position = new Vector3(transform.position.x, 25, -10);
            }
            else
            {
                cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
                if (transform.position.y <= 5)
                {

                    GameObject booom = Instantiate(boom);
                    booom.transform.position = transform.position;
                    Destroy(booom, 0.3f);
                    gameObject.SetActive(false);
                    transform.position = Flag.birth.position;
                    gameObject.SetActive(true);
                }
            }
        }
       
        

        if (horizontalMove > 0)
        {
            dashDirection = 1;
        }
        if (horizontalMove < 0)
        {
            dashDirection = -1;
        }
        if (Input.GetKeyDown(KeyCode.K) && jumpCount > 0)
        {
            Audio.Instance.PlaySound("Tiao");
            jumpPressed = true;
        }


        if (Input.GetKeyDown(KeyCode.L)&&dashCount>0)
        {
            cam.GetComponent<ShakeCamera>().shake();
            if (Time.time >= (lastDash + dashCoolDown) && Time.time >= dashTime)
            {
                isdash1 = false;
                ReadyToDash();
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.L)&&upDashCount > 0)
        {
            isdash2 = false;
            ReadyToUpDash();
        }

        
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        Dash();

        if (isDashing)
        {
            return;
        }

        GroundMovement();

        Jump();

        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }

    void Jump()//跳跃
    {
        if (isGround)
        {
            jumpCount = 1;//可跳跃数量
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnim()//动画切换
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }
        else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }
    }

    void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }
    void ReadyToUpDash()
    {
        isUpDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;
    }

    void Dash()
    {
        if (isGround)
        {
            dashCount = 1;//可冲刺数量
            upDashCount = 1;
           
        }
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                //​this.GetComponent<Rigidbody>().useGravity = false;
                if (rb.velocity.y != 0 && !isGround)
                {

                    rb.velocity = new Vector2(dashSpeed * dashDirection, rb.velocity.y);
                    if (isdash1==false)
                    {
                        dashCount = 0;
                    }
                  
                }
                if (isGround)
                {
                    rb.velocity = new Vector2(dashSpeed * dashDirection, rb.velocity.y);
                }

                dashTimeLeft -= Time.deltaTime;
                ShadowPool.instance.GetFormPool();


            }
            if (dashTimeLeft <= 0)
            {
                rb.velocity = new Vector2(0, 0);
                //  ​this.GetComponent<Rigidbody>().useGravity = true;
                isDashing = false;
                if (!isGround)
                {

                }
            }
        }
        if (isUpDashing)
        {
            if (dashTimeLeft > 0)
            {
                if (isdash2==false)
                {
                    upDashCount = 0;
                }
                
                //    ​this.GetComponent<Rigidbody>().useGravity = false;
                if (horizontalMove == 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, dashupspeed);
                }
                if (horizontalMove != 0)
                {
                    rb.velocity = new Vector2(dashSpeed * horizontalMove, dashupspeed);
                }
            }
            if (dashTimeLeft <= 0)
            {
                //   ​this.GetComponent<Rigidbody>().useGravity = true;
                rb.velocity = new Vector2(0, 0);
                isUpDashing = false;
                if (!isGround)
                {

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Dash")
        {
            Audio.Instance.PlaySound("Chi");
            star = collision.gameObject;
            isdash1 = true;
            isdash2 = true;
            dashCount = 1;
            upDashCount = 1;
            //star.SetActive(false);
            //StartCoroutine(Wait(0.04f));
            GameObject dashdiee = Instantiate(dashDie);
            dashDie.transform.position = collision.transform.position;
            Destroy(dashdiee, 0.3f);
        }
        if (collision.gameObject.tag == "Cherry")
        {
            Audio.Instance.PlaySound("Chi");
            collision.gameObject.SetActive(false);
           // Destroy(collision.gameObject);
            cherryNum+=1;
           
            GameObject CherryDie = Instantiate(dashDie);
            CherryDie.transform.position = collision.transform.position;
            Destroy(CherryDie, 0.3f);
        }

        if (collision.gameObject.tag=="End")
        {
            collision.gameObject.SetActive(false);
            IsEnd = true;
        }
        if (collision.gameObject.tag=="Home"&&IsEnd)
        {
            endText.gameObject.SetActive(true);
            Audio.Instance.StopSound();
            StartCoroutine(WaitBgm(1f));
            StartCoroutine(WaitEnd(4.6f));
            

        }
        if (collision.gameObject.tag=="QieGe")
        {
          //  Audio.Instance.QieGe("2");
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Wall")
        {
            Debug.Log(0);

            isWall = true;
           gameObject.GetComponent<SpriteRenderer>().sprite = wallSpriteRenderer;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            jumpCount = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            Debug.Log(1);
            isWall = false;
            rb.gravityScale = 3;
        }
        if (collision.gameObject.name == "UP")
        {
            back1.SetActive(false);
            back2.SetActive(true);
            transform.position = new Vector3(19.5f, 20, 0);
            cam.transform.position = new Vector3(transform.position.x, 25, -10);
            isCam = true;
         
        }
    }

   

}
