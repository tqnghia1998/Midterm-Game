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
    public static int playerLevel = 1;

    // Điểm nhận được khi đánh của từng loại tăng
    [SerializeField]
    int fastTankPoints = 100, bigTankPoints = 100, armoredTankPoints = 100;
    public int fastTankPointsWorth { get { return fastTankPoints; } }
    public int bigTankPointsWorth { get { return bigTankPoints; } }
    public int armoredTankPointsWorth { get { return armoredTankPoints; } }

    public static bool stageCleared = false;

    // Dev mode
    public static bool isShowRoute = false;
    public static bool isCustomTanks = false;
    public static int customFastTanks = 0;
    public static int customBigTanks = 0;
    public static int customArmoredTanks = 0;
    public static bool itemOneUp = true, itemLevelUp = true, itemGrenade = true;
    public static bool itemShovel = true, itemHelmet = true, itemStopwatch = true;

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
