using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;

    [SerializeField]
    Text stageNumberText, gameOverText;

    [SerializeField]
    RectTransform canvas;

    GameObject[] spawnPoints, botSpawnPoints;

    bool stageStart = false;
    bool tankReserveEmpty = false;

    void Start ()
    {
        stageNumberText.text = "STAGE " + GameManager.stageNumber.ToString();

        // Lấy reference
        botSpawnPoints = GameObject.FindGameObjectsWithTag("BotSpawnPoint");
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        stageStart = true;
        StartCoroutine(StartStage());
    }

    void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("FastTank") == null && GameObject.FindWithTag("BigTank") == null && GameObject.FindWithTag("ArmoredTank") == null)
        {
            GameManager.stageCleared = true;
            LevelCompleted();
        }
    }

    public void SpawnUser()
    {
        if (GameManager.playerLives > 0)
        {
            if (!stageStart)
            {
                GameManager.playerLives--;
            }

            stageStart = false;
            Animator anime = spawnPoints[0].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    public void SpawnBot()
    {
        if (LevelManager.smallTanks + LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
        {
            if (botSpawnPoints.Length == 0)
            {
                return;
            }

            int spawnPointIndex = Random.Range(0, botSpawnPoints.Length - 1);
            Animator anime = botSpawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawning");
        }
        else
        {
            tankReserveEmpty = true;
            CancelInvoke();
        }
    }

    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        GameManager.stageCleared = false;
        LevelCompleted();
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(2);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        yield return null;
        InvokeRepeating("SpawnBot", LevelManager.spawnRate, LevelManager.spawnRate);
        SpawnUser();
    }

    IEnumerator RevealTopStage()
    {
        float moveTopUpMin = topCurtain.rectTransform.position.y + (canvas.rect.height / 2)  + 10;
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < moveTopUpMin)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        float moveBottomDownMin = bottomCurtain.rectTransform.position.y - (canvas.rect.height / 2) - 10 ;
        while (bottomCurtain.rectTransform.position.y > moveBottomDownMin)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(blackCurtain.rectTransform.localScale.x, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime,0,1), blackCurtain.rectTransform.localScale.z);
            yield return null;
        }
    }

    private void LevelCompleted()
    {
        tankReserveEmpty = false;
        SceneManager.LoadScene("ScoreScene");
    }
}
