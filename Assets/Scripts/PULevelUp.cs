using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PULevelUp : PowerUps
{
    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        endSound.Play();
        collision.gameObject.GetComponent<User>().UpgradeTank();
        transform.position = new Vector3(-100, -100, 0);
        StartCoroutine(DestroyObject());
    }
}
