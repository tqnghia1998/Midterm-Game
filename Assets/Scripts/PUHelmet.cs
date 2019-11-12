using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUHelmet : PowerUps
{
    protected override void Start()
    {
        base.Start();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetTrigger("Invincible");
        Destroy(this.gameObject);
    }
}
