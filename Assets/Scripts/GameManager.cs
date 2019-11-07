using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed;
    public static int stageNumber;
    public static int playerLives = 3;
    public static int playerScore = 0;

    // Điểm nhận được khi đánh của từng loại tăng
    [SerializeField]
    int fastTankPoints = 100, bigTankPoints = 100, armoredTankPoints = 100;
    public int fastTankPointsWorth { get { return fastTankPoints; } }
    public int bigTankPointsWorth { get { return bigTankPoints; } }
    public int armoredTankPointsWorth { get { return armoredTankPoints; } }

    public static bool stageCleared = false;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
