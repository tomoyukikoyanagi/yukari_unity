using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerCtrl : MonoBehaviour
{

    public float speed = 5;
    public float jumpForce = 400f;
    public float maxVelocity = 30;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    private Animator anim;
    private CircleCollider2D collider;
    private SpriteRenderer spRenderer;

    private bool isGround;
    private bool isSloped;
    private bool isHeadBump;
    public bool isSakuraMode;
    public bool isLightMode;

    private GameObject canvas;
    private GameObject sakuraSprite;
    private UnityEngine.Experimental.Rendering.Universal.Light2D urp;
    private GameObject lightURP;
    private float innerRadius1 = 0.15f;
    private float outerRadius1 = 1.5f;
    private float innerRadius2 = 2.5f;
    private float outerRadius2 = 8.0f;
    private float lightSpeed = 1f;



    private bool isDead;

    
    // Start is called before the first frame update
    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.collider = GetComponent<CircleCollider2D>();
        this.spRenderer = GetComponent<SpriteRenderer>();

        sakuraSprite = GameObject.Find("SakuraSprite");
        canvas = GameObject.Find("Canvas");
        lightURP = GameObject.Find("PlayerLight");
        urp = lightURP.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        urp.pointLightInnerRadius = innerRadius1;
        urp.pointLightOuterRadius = outerRadius1;


        sakuraSprite.SetActive(false);
        Sound.LoadSe("head_hit", "head_hit");
        Sound.LoadSe("jump", "jump");
        Sound.LoadSe("dead", "dead");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (isHeadBump)
        {
            x = 0;
        }

        anim.SetFloat("speed", Mathf.Abs( x * speed));

        if (x > 0)
        {
            spRenderer.flipX = true;
        }else if (x < 0){
            spRenderer.flipX = false;
        }
        if (!isDead)
        {
            rb2d.AddForce(Vector2.right * x * speed);
        }

        anim.SetFloat("speed", Mathf.Abs(x * speed));

        if (isSloped)
        {
            //this.gameObject.transform.Translate(0.05f * x, 0.0f, 0.0f);
        }

        if (Input.GetButtonDown("Jump") & isGround & !isHeadBump & !isSakuraMode){
            anim.SetBool("isJump", true);
            rb2d.AddForce( Vector2.up * jumpForce );

            Sound.PlaySe("jump", 0);
        }
        //sakuraMode
        if (Input.GetButtonDown("Jump") & isSakuraMode & !isHeadBump)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            Sound.PlaySe("jump", 0);
        }
        if (isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }
        if (isHeadBump)
        {
            anim.SetBool("isHeadBump", true);
        }

        float velX = rb2d.velocity.x;
        float velY = rb2d.velocity.y;

        //Debug.Log("velx = " + velX);
        //Debug.Log("vely = " + velY);

        if (velY > 0.5f)
        {
            anim.SetBool("isJump", true);
        }
        if (velY < -0.2f)
        {
            anim.SetBool("isFall", true);
        }
            if (Mathf.Abs(velX) > 5)
        {
            if (velX > 5.0f) {
                rb2d.velocity = new Vector2(5.0f, velY);
            }
            if ( velX < 5.0f)
            {
                rb2d.velocity = new Vector2(-5.0f, velY);
            }
        }
    }

    private void FixedUpdate()
    {
        isGround = false;

        float x = Input.GetAxisRaw("Horizontal");

        Vector2 groundPos =
            new Vector2 (
                transform.position.x,
                transform.position.y
            );

        Vector2 groundArea = new Vector2(0.5f, 0.5f);
        Vector2 wallArea1 = new Vector2(x * 0.8f, 1.5f);
        Vector2 wallArea2 = new Vector2(x * 0.3f, 1.0f);


        Vector2 wallArea3 = new Vector2(x * 1.5f, 0.2f);
        Vector2 wallArea4 = new Vector2(x * 1.0f, 0.05f);

        //Debug.DrawLine(groundPos + groundArea, groundPos - groundArea, Color.red);
        //Debug.DrawLine(groundPos + wallArea1, groundPos + wallArea2, Color.red);
        //Debug.DrawLine(groundPos + wallArea3, groundPos + wallArea4, Color.red);

        isGround = Physics2D.OverlapArea(
            groundPos + groundArea,
            groundPos - groundArea,
            groundLayer
            );

        bool area1 = false;
        bool area2 = false;

        area1 = Physics2D.OverlapArea(
            groundPos + wallArea1,
            groundArea + wallArea2,
            groundLayer
            );

        area2 = Physics2D.OverlapArea(
            groundPos + wallArea3,
            groundArea + wallArea4,
            groundLayer
            );

        if (!area1 & area2){
            isSloped = true;
        } else
        {
            isSloped = false;
        }

        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
        //Debug.Log("sloped" + isSloped);
    }

    IEnumerator Dead()
    {
        
        anim.SetBool("isDamage", true);

        yield return null;
        

        //float deadForce = 100f;
        //rb2d.AddForce(Vector2.up * deadForce);
        //rb2d.AddForce(Vector2.left * deadForce);

        rb2d.gravityScale = 0;
        this.collider.enabled = false;
        Vector3 pos = transform.localPosition + Vector3.up * 7.0f + Vector3.left * 7.0f;
        transform.DOLocalMove(pos, 0.7f);

        GetComponent<CircleCollider2D>().enabled = false;
        Sound.StopBgm();
        Sound.PlaySe("dead",0);
        canvas.SendMessage("GameOver");

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.tag == "Enemy")
        {
            isDead = true;
            StartCoroutine("Dead");
        }

        if(col.gameObject.tag == "Finish")
        {
            GameObject.Find("ClearPoint").GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
            canvas.SendMessage("DisplayTelop", "clear");
            canvas.SendMessage("fadeOut");
            Sound.StopBgm();
        }

        if (col.gameObject.tag == "TrapBox")
        {
            isHeadBump = true;
            Sound.PlaySe("head_hit");
        }

        if (col.gameObject.tag == "Sakura")
        {
            Debug.Log("start_sakuramode");
            sakuraSprite.SetActive(true);
            isSakuraMode = true;
            Invoke("EndSakuraMode", 10.0f);
        }

        if (col.gameObject.tag == "Flower")
        {
            //urp.pointLightInnerRadius = Mathf.MoveTowards(innerRadius1,innerRadius2, Time.deltaTime * lightSpeed);
            //urp.pointLightOuterRadius = Mathf.MoveTowards(outerRadius1,outerRadius2, Time.deltaTime * lightSpeed);
            urp.pointLightInnerRadius = innerRadius2;
            urp.pointLightOuterRadius = outerRadius2;
            isLightMode = true;
            Invoke("EndLightMode", 10.0f);
        }

        if (col.gameObject.tag == "transitionPoint12")
        {
            Debug.Log("entered tp12");
            SceneManager.LoadScene("stage1-2");
        }
    }

    void EndSakuraMode()
    {
        isSakuraMode = false;
        sakuraSprite.SetActive(false);
    }

    void EndLightMode()
    {
        urp.pointLightInnerRadius = innerRadius1;
        urp.pointLightOuterRadius = outerRadius1;
        //urp.pointLightInnerRadius = Mathf.MoveTowards(innerRadius2, innerRadius1, Time.deltaTime * lightSpeed);
        //urp.pointLightOuterRadius = Mathf.MoveTowards(outerRadius2, outerRadius1, Time.deltaTime * lightSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col )
    {
        if ( col.gameObject.tag == "Enemy")
        {
            anim.SetBool("isJump", true);
            rb2d.AddForce(Vector2.up * jumpForce);
        }
        if ( col.gameObject.tag == "Damage")
        {
            isDead = true;
            StartCoroutine("Dead");
        }
        if ( col.gameObject.tag == "TrapBox")
        {
            Debug.Log("onTrapBox");
            isGround = true;
        }
        if (col.gameObject.tag == "Box")
        {
            isGround = true;
        }
    }
}


