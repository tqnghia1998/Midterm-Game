using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int fastTanksInThisLevel, bigTanksInThisLevel, armoredTanksInThisLevel, stageNumber;
    public static int fastTanks, bigTanks, armoredTanks;

    [SerializeField]
    float spawnRateInThisLevel = 5;
    public static float spawnRate { get; private set; }

    private void Awake()
    {
        GameManager.stageNumber = stageNumber;

        if (GameManager.isCustomTanks == true)
        {
            fastTanks = GameManager.customFastTanks;
            bigTanks = GameManager.customBigTanks;
            armoredTanks = GameManager.customArmoredTanks;

            // if (GameManager.customFastTanks == 0)
            // {
            //     fastTanks = fastTanksInThisLevel;
            // }
            // if (GameManager.customBigTanks == 0)
            // {
            //     bigTanks = bigTanksInThisLevel;
            // }
            // if (GameManager.customArmoredTanks == 0)
            // {
            //     armoredTanks = armoredTanksInThisLevel;
            // }
        }
        else
        {
            fastTanks = fastTanksInThisLevel;
            bigTanks = bigTanksInThisLevel;
            armoredTanks = armoredTanksInThisLevel;
        }
        
        spawnRate = spawnRateInThisLevel;
    }
}
