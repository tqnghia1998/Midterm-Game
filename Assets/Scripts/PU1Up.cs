using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU1Up : PowerUps
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.playerLives++;
        GameplayManager GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
        GPM.UpdateUserLives();
        Destroy(this.gameObject);
    }
}