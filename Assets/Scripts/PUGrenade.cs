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
        endSound.Play();
        enemyHolder = GameObject.Find("EnemyHolder");
        Health[] enemiesHealthScript = enemyHolder.GetComponentsInChildren<Health>(true);

        foreach (Health enemyHealthScript in enemiesHealthScript)
        {
            enemyHealthScript.TakeDamage(200, true);
        }
        transform.position = new Vector3(-100, -100, 0);
        StartCoroutine(DestroyObject());
    }
}
