using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;

    [SerializeField]
    bool isPlayer;

    [SerializeField]
    GameObject userTank, fastTank, bigTank, armoredTank;

    enum tankType
    {
        fastTank, bigTank, armoredTank
    };

    void Start()
    {
        if (isPlayer)
        {
            tanks = new GameObject[1] 
            { 
                userTank
            };
        }
        else
        {
            tanks = new GameObject[3]
            { 
                fastTank, 
                bigTank, 
                armoredTank 
            };
        }
    }

    public void StartSpawning()
    {
        // Random như random hướng
        if (!isPlayer)
        {
            List<int> tankToSpawn = new List<int>();

            if (LevelManager.fastTanks > 0)
            {
                tankToSpawn.Add((int)tankType.fastTank);
            }
            if (LevelManager.bigTanks > 0)
            {
                tankToSpawn.Add((int)tankType.bigTank);
            }
            if (LevelManager.armoredTanks > 0)
            {
                tankToSpawn.Add((int)tankType.armoredTank);
            }

            // Tiến hành random
            if (tankToSpawn.Count == 0)
            {
                return;
            }

            int tankID = tankToSpawn[Random.Range(0, tankToSpawn.Count - 1)];
            tank = Instantiate(tanks[tankID], transform.position, transform.rotation);

            // Random bonus tank
            if (Random.value <= 0.5)
            {
                tank.GetComponent<BonusTank>().MakeBonusTank();
            }
            
            // Giảm số lượng còn lại
            if (tankID == (int)tankType.fastTank)
            {
                LevelManager.fastTanks--;
            }
            else if (tankID == (int)tankType.bigTank)
            {
                LevelManager.bigTanks--;
            }
            else if (tankID == (int)tankType.armoredTank)
            {
                LevelManager.armoredTanks--;
            }
        }
        else
        {
            tank = Instantiate(tanks[0], transform.position, transform.rotation);
        }
    }

    public void SpawnNewTank()
    {
        if (tank != null)
        {
            tank.SetActive(true);
        }
    }

}
