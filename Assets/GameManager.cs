using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int deathCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        Sound.LoadBgm("spelanker", "spelanker");
        Sound.PlayBgm("spelanker",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
