using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxCtrl : MonoBehaviour
{
    private bool isOn = true;
    private SpriteRenderer spRenderer;
    public Sprite OnSprite;
    public Sprite OffSprite;

    private HitChecker sChecker;


    // Start is called before the first frame update
    void Start()
    {
        sChecker = transform.Find("sideChecker").gameObject.GetComponent<HitChecker>();
        this.spRenderer = GetComponent<SpriteRenderer>();
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
        isOn = false;
    }
}
