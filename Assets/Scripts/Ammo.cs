using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float Speed;
    public Vector2 direction;
    public float LifeTime;
    public int Damage = 5555;

	void Start ()
    {
        Destroy(gameObject, LifeTime);
	}
	
	
	void Update ()
    {
        transform.Translate(direction * Speed * Time.deltaTime);    	
	}
}
