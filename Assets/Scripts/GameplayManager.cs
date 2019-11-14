using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;

    [SerializeField]
    Text stageNumberText, gameOverText, stageNumberTextIngame, livesLeft, fastTankLeft, bigTankLeft, armoredTankLeft;

    [SerializeField]
    RectTransform canvas;

    [SerializeField]
    GameObject[] bonusCrates;

    GameObject[] spawnPoints, botSpawnPoints;
    Tilemap waterTilemap, steelTilemap;

    public AudioSource theme;

    Transform enemyHolder;
    
    bool stageStart = false;
    bool tankReserveEmpty = false;

    void Start ()
    {
        stageNumberText.text = "STAGE " + GameManager.stageNumber.ToString();
        stageNumberTextIngame.text = "ST. " + GameManager.stageNumber.ToString();
        UpdateUserLives();
        UpdateBotLives();

        // Lấy reference
        botSpawnPoints = GameObject.FindGameObjectsWithTag("BotSpawnPoint");
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        stageStart = true;
        StartCoroutine(StartStage());

        steelTilemap = GameObject.Find("Iron").GetComponent<Tilemap>();
	    waterTilemap = GameObject.Find("Water").GetComponent<Tilemap>();
    }

    public void UpdateBotLives()
    {
        fastTankLeft.text = "x" + LevelManager.fastTanks.ToString();
        bigTankLeft.text = "x" + LevelManager.bigTanks.ToString();
        armoredTankLeft.text = "x" + LevelManager.armoredTanks.ToString();
    }

    public void UpdateUserLives()
    {
        livesLeft.text = "x" + GameManager.playerLives.ToString();
    }

    void Update()
    {
        if (tankReserveEmpty && GameObject.FindWithTag("FastTank") == null && GameObject.FindWithTag("BigTank") == null && GameObject.FindWithTag("ArmoredTank") == null)
        {
            GameManager.stageCleared = true;
            LevelCompleted();
        }
    }

    bool IsInvalidBonusCratePosition(Vector3 cratePosition)
    {
        return waterTilemap.GetTile(waterTilemap.WorldToCell(cratePosition)) != null || steelTilemap.GetTile(steelTilemap.WorldToCell(cratePosition)) != null;
    }

    public void GenerateBonusCrate()
    {
        GameObject bonusCrate = bonusCrates[Random.Range(0, bonusCrates.Length - 1)];
        Vector3 cratePosition = new Vector3(Random.Range(-12, 12), Random.Range(-12, 13), 0);
        if (IsInvalidBonusCratePosition(cratePosition))
        {
            do
            {
                cratePosition = new Vector3(Random.Range(-12, 12), Random.Range(-12, 13), 0);
                if (!IsInvalidBonusCratePosition(cratePosition)) Instantiate(bonusCrate, cratePosition, Quaternion.identity);
            } while (IsInvalidBonusCratePosition(cratePosition));
        }
        else
        {
            Instantiate(bonusCrate, cratePosition, Quaternion.identity);
        }
    }

    public void SpawnUser()
    {
        if (GameManager.playerLives > 0)
        {
            if (!stageStart)
            {
                GameManager.playerLives--;
                livesLeft.text = "x" + GameManager.playerLives.ToString();
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
        if (LevelManager.fastTanks + LevelManager.bigTanks + LevelManager.armoredTanks > 0)
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
        yield return new WaitForSeconds(2);
        theme.Play();
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
        User player = GameObject.FindGameObjectWithTag("UserTank").GetComponent<User>();
        GameManager.playerLevel = player.level;
        SceneManager.LoadScene("Score Screen");
    }

    public void ActivateFreeze()
    {
        StartCoroutine(FreezeActivated());
    }

    IEnumerator FreezeActivated()
    {
        Bot.freezing = true;
        enemyHolder = GameObject.Find("EnemyHolder").transform;

        for (int i = 0; i < enemyHolder.childCount; i++)
        {
            // enemyHolder.GetChild(i).gameObject.SetActive(false);
            enemyHolder.GetChild(i).gameObject.GetComponent<Bot>().ToFreezeTank();
            // enemyHolder.GetChild(i).gameObject.GetComponent<Bot>().enabled = false;
            // enemyHolder.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(10);

        for (int i = 0; i < enemyHolder.childCount; i++)
        {
            // enemyHolder.GetChild(i).gameObject.SetActive(false);
            // enemyHolder.GetChild(i).gameObject.GetComponent<Bot>().enabled = true;
            enemyHolder.GetChild(i).gameObject.GetComponent<Bot>().ToUnfreezeTank();
            // enemyHolder.GetChild(i).gameObject.SetActive(true);
        }
        
        Bot.freezing = false;
    }
}
