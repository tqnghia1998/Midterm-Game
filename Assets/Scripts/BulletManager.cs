using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    GameObject bullet, bullet2, fireEffect;
    Bullet logicBullet, logicBullet2;

    public AudioSource fireSound;

    [SerializeField]
    int speed = 1;
    
    public int level = 1;

    void Start () 
    {
        // Khởi tạo đạn từ prefab
        bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        fireEffect = transform.GetChild(0).gameObject;
        logicBullet = bullet.GetComponent<Bullet>();
        logicBullet.speed = speed;

        if (level > 1)
        {
            UpgradeProjectileSpeed();
        }
        if (level > 2)
        {
            GenerateSecondCanonBall();
        }
        if (level > 3)
        {
            CanonBallPowerUpgrade();
        }
    }

    public void UpgradeProjectileSpeed()
    {
        speed = 20;
        logicBullet.speed = speed;
    }

    public void GenerateSecondCanonBall()
    {
        bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        logicBullet2 = bullet2.GetComponent<Bullet>();
        logicBullet2.speed = speed;
    }

    public void Fire()
    {
        if (bullet != null)
        {
            if (bullet.activeSelf == false)
            {
                // Hiện đạn thứ nhất lên và đặt vào vị trí nòng súng
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                StartCoroutine(ShowFireEffect());
                bullet.SetActive(true);
                fireSound.Play();
            }
            else if (bullet2 != null)
            {
                if (bullet2.activeSelf == false)
                {
                    // Hiện nó lên và đặt vào vị trí nòng súng
                    bullet2.transform.position = transform.position;
                    bullet2.transform.rotation = transform.rotation;
                    StartCoroutine(ShowFireEffect());
                    bullet2.SetActive(true);
                fireSound.Play();
                }   
            }
        }
    }

    public void CanonBallPowerUpgrade()
    {
        if (bullet != null)
        {
            logicBullet.destroyIron = true;
        }

        if (bullet2 != null)
        {
            logicBullet2.destroyIron = true;
        }
    }

    IEnumerator ShowFireEffect()
    {
        fireEffect.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        fireEffect.SetActive(false);
    }

    void OnDisable()
    {
        if (bullet != null)
        {
            logicBullet.DestroyBullet();
        }

        if (bullet2 != null)
        {
            logicBullet2.DestroyBullet();
        }
    }
}
