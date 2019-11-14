using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUShovel : PowerUps
{
    GameObject grid;

    protected override void Start()
    {
        base.Start();
        grid = GameObject.Find("Grid");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        endSound.Play();
        GridMap gridmap = grid.GetComponent<GridMap>();
        gridmap.ActivateSpadePower();
        transform.position = new Vector3(-100, -100, 0);
        StartCoroutine(DestroyObject());
    }
}
