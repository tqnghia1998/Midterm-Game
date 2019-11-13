using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("UserBullet") || collision.gameObject.CompareTag("BotBullet"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            GameplayManager GPM = GameObject.Find("Canvas").GetComponent<GameplayManager>();
            StartCoroutine(GPM.GameOver());
        }
    }
}
