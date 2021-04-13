using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sound.LoadBgm("title", "title");
        Sound.PlayBgm("title", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
