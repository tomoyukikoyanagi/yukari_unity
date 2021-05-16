using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGroundCtrl : MonoBehaviour
{
    [Header("movepath")] public GameObject[] movePoint;
    [Header("pathSpeed")] public float speed = 4.0f;
    [Header("startPoint")] public int startPoint = 0;

    private Rigidbody2D rb;
    private int nowPoint = 0;
    private bool returnPoint = false;

     // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[startPoint].transform.position;
        }

    }

    private void FixedUpdate()
    {
        if(movePoint != null && movePoint.Length >  1 && rb != null)
        {
            if (!returnPoint)
            {
                int nextPoint = nowPoint + 1;

                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;

                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            else
            {
                int nextPoint = nowPoint - 1;

                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                }

                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
