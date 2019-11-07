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
        fastTanks = fastTanksInThisLevel;
        bigTanks = bigTanksInThisLevel;
        armoredTanks = armoredTanksInThisLevel;
        spawnRate = spawnRateInThisLevel;
    }
}
