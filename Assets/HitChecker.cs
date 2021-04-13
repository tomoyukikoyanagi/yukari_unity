using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public bool isGroundHit;
    public bool isSideHit;
    public bool isPlayerHit;
    public bool isEnemyHit;
    public bool isBoxHit;
    public bool isTrapBoxHit;
    public bool isDokan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if( col.gameObject.name == "StageMap" )
        {
            isGroundHit = true;
        }
        if( col.gameObject.name == "Player")
        {
            isPlayerHit = true;
            Debug.Log("player");
        }
        if( col.gameObject.tag == "Enemy")
        {
            isEnemyHit = true;
        }
        if( col.gameObject.tag == "Box")
        {
            isBoxHit = true;
        }
        if (col.gameObject.tag == "TrapBox")
        {
            isTrapBoxHit = true;
        }
        if (col.gameObject.tag == "Dokan")
        {
            isDokan = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "StageMap")
        {
            isGroundHit = false;
        }
        if (col.gameObject.name == "Player")
        {
            isPlayerHit = false;
        }
        if (col.gameObject.tag == "Enemy")
        {
            isEnemyHit = false;
        }
        if(col.gameObject.tag == "Box")
        {
            isBoxHit = false;
        }
        if (col.gameObject.tag == "TrapBox")
        {
            isTrapBoxHit = false;
        }
        if (col.gameObject.tag == "Dokan")
        {
            isDokan = false;
        }
    }
}
