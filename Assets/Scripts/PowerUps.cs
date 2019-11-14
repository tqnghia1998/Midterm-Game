using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUps : MonoBehaviour
{
    private SpriteRenderer sprite;
    public AudioSource startSound;
    public AudioSource endSound;

    protected virtual void Start () 
    {
        sprite = GetComponent<SpriteRenderer>();
        InvokeRepeating("Blink", 0, 0.2f);
        startSound.Play();
    }

    void Blink()
    {
        sprite.enabled = !sprite.enabled;
    }

    protected IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
