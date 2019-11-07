using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TallyScore : MonoBehaviour
{
    [SerializeField]
    Text hiScoreText, stageText, playerScoreText, fastTankScoreText, bigTankScoreText, armoredTankScoreText, fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed, totalTanksDestroyed;
    
    int fastTankScore, bigTankScore, armoredTankScore;
    GameManager masterTracker;
    int fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;

    void Start ()
    {
        masterTracker = GameObject.Find("GameManager").GetComponent<GameManager>();
        fastTankPointsWorth = masterTracker.fastTankPointsWorth;
        bigTankPointsWorth = masterTracker.bigTankPointsWorth;
        armoredTankPointsWorth = masterTracker.armoredTankPointsWorth;
        stageText.text = "STAGE " + GameManager.stageNumber;
        hiScoreText.text = "1000000000";
        StartCoroutine(UpdateTankPoints());
    }

    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i <= GameManager.fastTankDestroyed; i++)
        {
            fastTankScore = fastTankPointsWorth * i;
            fastTankScoreText.text = fastTankScore.ToString();
            fastTankDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.25f);
        }
        for (int i = 0; i <= GameManager.bigTankDestroyed; i++)
        {
            bigTankScore = bigTankPointsWorth * i;
            bigTankScoreText.text = bigTankScore.ToString();
            bigTankDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.25f);
        }
        for (int i = 0; i <= GameManager.armoredTankDestroyed; i++)
        {
            armoredTankScore = armoredTankPointsWorth * i;
            armoredTankScoreText.text = armoredTankScore.ToString();
            armoredTankDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.25f);
        }

        totalTanksDestroyed.text = (GameManager.fastTankDestroyed + GameManager.bigTankDestroyed + GameManager.armoredTankDestroyed).ToString();
        GameManager.playerScore += (fastTankScore + bigTankScore + armoredTankScore);
        playerScoreText.text = GameManager.playerScore.ToString();
        yield return new WaitForSeconds(3f);

        if (GameManager.stageCleared)
        {
            ClearStatistics();
            SceneManager.LoadScene("Stage " + (GameManager.stageNumber + 1));
        }
        else
        {
            ClearStatistics();
        }
    }

    void ClearStatistics()
    {
        GameManager.fastTankDestroyed = 0;
        GameManager.bigTankDestroyed = 0;
        GameManager.armoredTankDestroyed = 0;
        GameManager.stageCleared = false;
    }
}