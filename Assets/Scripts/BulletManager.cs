using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    GameObject bullet, fireEffect;
    Bullet logicBullet;

    [SerializeField]
    int speed = 1;

    void Start () 
    {
        // Khởi tạo đạn từ prefab
        bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        fireEffect = transform.GetChild(0).gameObject;
        logicBullet = bullet.GetComponent<Bullet>();
        logicBullet.speed = speed;
    }

    public void Fire()
    {
        if (bullet != null)
        {
            // Nếu đạn đang ẩn
            if (bullet.activeSelf == false)
            {
                // Hiện nó lên và đặt vào vị trí nòng súng
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                StartCoroutine(ShowFireEffect());
                bullet.SetActive(true);
            }   
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
    }
}
