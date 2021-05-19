using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class transitionpoint12 : MonoBehaviour
{

    private GameObject canvas;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        this.collider = GetComponent<BoxCollider2D>();
        canvas = GameObject.Find("Canvas");
        Sound.LoadBgm("title", "title");
        Sound.PlayBgm("title", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    
}
