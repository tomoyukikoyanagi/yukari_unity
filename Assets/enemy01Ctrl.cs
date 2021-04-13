using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class enemy01Ctrl : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spRenderer;
    private Rigidbody2D rb2d;
    public GameObject player;

    public float speed = 15;

    public bool standby = true;
    public float standbyDistance = 5;
    public bool stay = false;


    private HitChecker sChecker;
    private HitChecker gChecker;

    private bool isAttack = false;

    private bool isIdle = false;

    private bool isDead = false;

    private bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        this.anim = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        sChecker = transform.Find("sideChecker").gameObject.GetComponent<HitChecker>();
        gChecker = transform.Find("groundChecker").gameObject.GetComponent<HitChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (standby == true)
        {
            Vector2 pPos = player.transform.position;
            Vector2 myPos = this.transform.position;
            float distance = Vector2.Distance(pPos, myPos);

            if (distance < standbyDistance)
            {
                isMove = true;
            }
        } else if(stay == true){
            isMove = false;

        }
        else
        {
            isMove = true;
        }

        if (isMove)
        {
            anim.SetBool("startMove", true);
            startAction();
        }
        
    }

    private void startAction()
    {
        float x = 1;
        if (this.transform.eulerAngles.y == 180)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }

        CheckValue();

        if (sChecker.isPlayerHit)
        {
            // attack action
            if (!isAttack)
            {
                //StartCoroutine("Attack");
            }
            //rb2d.velocity = new Vector2(0, 0);
        }

        if (!isIdle & !isAttack & !isDead)
        {
            anim.SetBool("isWalk", true);
            rb2d.AddForce(Vector2.right * x * speed);
        }
        else
        {
            anim.SetBool("isWalk", false);
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    private void CheckValue()
    {

            if (!gChecker.isGroundHit & !isIdle)
            {
                gChecker.isGroundHit = true;
                StartCoroutine("ChangeRotate");
            }
            if (sChecker.isEnemyHit & !isIdle)
            {
                sChecker.isEnemyHit = false;
                StartCoroutine("ChangeRotate");
            }
            if (sChecker.isGroundHit & !isIdle)
            {
                StartCoroutine("ChangeRotate");
            }
        
    }

    IEnumerator Attack()
    {
        isAttack = true;
        //anim.SetTrigger("trgAttack");
        yield return new WaitForSeconds(2.0f);

        isAttack = false;
    }

    IEnumerator ChangeRotate()
    {
        isIdle = true;

        yield return new WaitForSeconds(0.1f);

        isIdle = true;
        if (this.transform.eulerAngles.y == 180)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetBool("isOpposite", false);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("isOpposite", true);
        }

        isIdle = false;
    }

    void dokanAction()
    {
        Vector3 pos = transform.localPosition + Vector3.up * 0.5f;
        transform.DOLocalMove(pos, 0.25f);
    }
}
