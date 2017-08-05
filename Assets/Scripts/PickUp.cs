using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Rigidbody2D rBody;

    public int ScrapAmount;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rBody.velocity = Vector2.Lerp(rBody.velocity, Vector2.zero, 0.01f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PickedUp();
        }
        rBody.AddForce(transform.position - collision.transform.position, ForceMode2D.Impulse);
    }

    private void PickedUp()
    {
        GameManager.instance.ScrapPickUp(ScrapAmount);
        Destroy(gameObject);
    }
}
