using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DokanCtrl : MonoBehaviour
{
    private HitChecker sChecker;
    private GameObject enemy;

    public float waittime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        sChecker = transform.Find("pChecker").gameObject.GetComponent<HitChecker>();
        enemy = GameObject.Find("enemy_dokan");
        enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (sChecker.isPlayerHit)
        {
            Debug.Log("activate Enemy: DOKAN");
            Invoke("activateEnemy", waittime);
        }
    }

    void activateEnemy()
    {
        enemy.SetActive(true);
        enemy.SendMessage("dokanAction");
    }
}
