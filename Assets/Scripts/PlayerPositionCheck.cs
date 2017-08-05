using System;
using System.Collections;
using UnityEngine;

public class PlayerPositionCheck : MonoBehaviour
{
    public Vector2 offset;

    private GameObject Player;
    private SpriteRenderer render;

    private void Start()
    {
        StartCoroutine(FindPlayer());
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Player != null)
        {
            if (transform.position.y - Player.transform.position.y > offset.y)
            {
                render.sortingLayerName = "Wall_0";
            }
            else
                render.sortingLayerName = "Wall_1";
        }
    }

    private IEnumerator FindPlayer()
    {
        while(Player == null)
        {
            Player = GameObject.Find("Player");
            yield return null;
        }
    }
}
