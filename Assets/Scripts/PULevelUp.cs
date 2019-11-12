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
        collision.gameObject.GetComponent<User>().UpgradeTank();
        Destroy(this.gameObject);
    }
}
