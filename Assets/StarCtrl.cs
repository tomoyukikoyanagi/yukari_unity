using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class StarCtrl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent <SpriteRenderer>();
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.name == "Player")
        {
            Vector3 pos = transform.localPosition + Vector3.up * 1.5f;
            transform.DOLocalMove(pos, 0.25f);
            transform.DORotate(new Vector3(0, 180, 0), 1.0f);
            spriteRenderer.DOFade(0, 1.5f);

            //canvas score will change here
            canvas.SendMessage( "AddScore" , 150);

            GetComponent<BoxCollider2D>().enabled = false;

            Invoke("DestoyObject", 2);
        }
    }

    void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
