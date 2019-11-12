using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUGrenade : PowerUps
{
    GameObject[] enemies;
    GameObject enemyHolder;
    
    protected override void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyHolder = GameObject.Find("EnemyHolder");
        Health[] enemiesHealthScript = enemyHolder.GetComponentsInChildren<Health>(true);

        foreach (Health enemyHealthScript in enemiesHealthScript)
        {
            enemyHealthScript.TakeDamage(1000, true);
        }
        Destroy(this.gameObject);
    }
}
