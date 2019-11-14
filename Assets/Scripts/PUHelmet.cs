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
        endSound.Play();
        collision.gameObject.GetComponent<Animator>().SetTrigger("Invincible");
        collision.gameObject.GetComponent<Health>().IntervalRestoreHealth();
        transform.position = new Vector3(-100, -100, 0);
        StartCoroutine(DestroyObject());
    }
}
