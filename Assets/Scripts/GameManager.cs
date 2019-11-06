using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static int smallTankDestroyed, fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed;
    public static int stageNumber;
    public static int playerLives = 3;
    public static int playerScore = 0;

    // Điểm nhận được khi đánh của từng loại tăng
    [SerializeField]
    int smallTankPoints = 100;
    public int smallTankPointsWorth { get { return smallTankPoints; } }
    int fastTankPoints = 100;
    public int fastTankPointsWorth { get { return fastTankPoints; } }
    int bigTankPoints = 100;
    public int bigTankPointsWorth { get { return bigTankPoints; } }
    int armoredTankPoints = 100;
    public int armoredTankPointsWorth { get { return armoredTankPoints; } }

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
