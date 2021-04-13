using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using DG.Tweening;

public class CanvasCtrl : MonoBehaviour
{

    private RectTransform rectTransform;

    private GameObject imageObject;

    private Image imageComponent;

    public GameObject textObject;
    private Text scoreText;
    public int Score = 0;

    public GameObject gameOverObject;
    public Text gameOverText;

    public GameObject clearObject;
    public Text gameClearText;

    private GameObject fadeObject;
    private RectTransform fadeRectT;

    private int deathCount;

    

    // Start is called before the first frame update
    void Start()
    {
        //imageObject = GameObject.Find("Image");
        //imageComponent = imageObject.GetComponent<Image>();
        //？imageComponent.enabled = false;
        clearObject = GameObject.Find("GameOver");
        gameClearText = clearObject.GetComponent<Text>();
        gameClearText.enabled = false;


        gameOverObject = GameObject.Find("GameClear");
        gameOverText = gameOverObject.GetComponent<Text>();
        gameOverText.enabled = false;


        textObject = GameObject.Find("Text");
        scoreText = textObject.GetComponent<Text>();
        scoreText.text = "000000";

        fadeObject = GameObject.Find("Fade");
        fadeRectT = fadeObject.GetComponent<RectTransform>();

        fadeIn();

    }

    void AddScore(int score)
    {
        Score += score;
        scoreText.text = Score.ToString("000000");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fadeIn()
    {
        fadeRectT.DOScale(new Vector3(1, 0, 1), 1.5f)
        .SetEase(Ease.InOutQuint);
    }

    void fadeOut()
    {
        fadeRectT.DOScale(new Vector3(1, 1, 1), 1.5f)
            .SetEase(Ease.InOutQuint);
    }

    void DisplayTelop(string text)
    {
        gameOverText.text = text;
        gameOverText.enabled = true;

    }

    void GameOver()
    {
        StartCoroutine("GameOverMessage","game over");
    }

    IEnumerator GameOverMessage(string message)
    {
        fadeOut();
        gameOverText.text = message.ToString();
        gameOverText.enabled = true;
        //yield return new WaitForSeconds(2.0f);
        
        yield return new WaitForSeconds(3.0f);
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("SampleScene");
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        Scene scene = SceneManager.GetSceneByName("SampleScene");
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            GameManager gameManager = rootGameObject.GetComponent<GameManager>();
            if (gameManager != null)
            {
                deathCount += 1;
                gameManager.deathCount = deathCount;
                Debug.Log("deathCount:" + deathCount);
                break;
            }
            SceneManager.sceneLoaded -= GameSceneLoaded;
        }
    }
}
