using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sakuraBoxCtrl : MonoBehaviour
{
    private bool isOn = true;
    private SpriteRenderer spRenderer;
    public Sprite OnSprite;
    public Sprite OffSprite;

    private HitChecker sChecker;
    private GameObject sakura;


    // Start is called before the first frame update
    void Start()
    {
        sChecker = transform.Find("pChecker").gameObject.GetComponent<HitChecker>();
        this.spRenderer = GetComponent<SpriteRenderer>();
        sakura = GameObject.Find("sakura");
        sakura.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (sChecker.isPlayerHit)
        {
            switchBox();
        }
    }

    void switchBox()
    {
        this.spRenderer.sprite = OffSprite;
        sakura.SetActive(true);
        sakura.SendMessage("initAction");
        isOn = false;
    }
}
