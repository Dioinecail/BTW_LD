using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public GameObject[] Trash;

	void Start ()
    {
        int rnd = Random.Range(0, Trash.Length - 1);
        bool random = Random.Range(0, 1) == 0 ? true : false;

        Instantiate(Trash[rnd], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360))).GetComponent<SpriteRenderer>().flipX = random;

        
	}
}
