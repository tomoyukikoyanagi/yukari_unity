using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class cherryCtrl : MonoBehaviour
{
    public bool startAction = true;
    public float speed = 1;
    public float jumpForce = 300;
    public float maxVelocity = 1;
    public float maxVelocityX = 1;
    public float maxVelocityY = 30;

    private SpriteRenderer spRenderer;
    private Rigidbody2D rb2d;
    private HitChecker checker;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.spRenderer = GetComponent<SpriteRenderer>();
        checker = transform.Find("hitChecker").gameObject.GetComponent<HitChecker>();
        this.rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(maxVelocityX, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (startAction == true)
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
            //rb2d.AddForce(Vector2.left * x * speed);
            CheckValue(x);
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxVelocity);
    }

    private void CheckValue(float x)
    {
        if (checker.isGroundHit || checker.isBoxHit)
        {
            //rb2d.AddForce(Vector2.up * jumpForce);
            rb2d.velocity = new Vector2(x * maxVelocityX, maxVelocityY);
            //StartCoroutine("ChangeRotate");
        }
    }

    IEnumerator ChangeRotate()
    {
        startAction = true;
        yield return new WaitForSeconds(0.1f);

        startAction = true;
        if (this.transform.eulerAngles.y == 180)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        startAction = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("DestroyObject", 0);
        }
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    void StartAction()
    {
        startAction = true;
    }

    void initAction()
    {
        Vector3 pos = transform.localPosition + Vector3.up * 0.5f;
        transform.DOLocalMove(pos, 0.25f);
        Invoke("StartAction",1);
    }
}
