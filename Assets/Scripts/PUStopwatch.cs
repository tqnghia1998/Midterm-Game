using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUStopwatch : PowerUps
{
    GameObject[] enemies;
    GameObject enemyHolder;
    
    protected override void Start()
    {
        base.Start();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameplayManager GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        GPM.ActivateFreeze();
        Destroy(this.gameObject);
    }
}
