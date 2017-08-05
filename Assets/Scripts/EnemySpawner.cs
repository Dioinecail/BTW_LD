using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public Transform[] Spawns;
    public GameObject[] Blockade;
    public AudioClip unlock;
    private AudioSource source;
    public int Remaining;

    private bool Spawned;
    private bool Over;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Spawned)
        {
            if (Remaining <= 0)
            {
                if (!Over)
                {
                    source.PlayOneShot(unlock);
                    Over = true;
                }
                foreach (GameObject block in Blockade)
                {
                    Destroy(block);
                }
            }
        }
    }

    public void SpawnEnemy()
    {
        if (!Spawned)
        {
            Spawned = true;
            foreach (Transform spawn in Spawns)
            {
                Remaining++;
                int rnd = Random.Range(0, Enemy.Length - 1);
                GameObject obj = Instantiate(Enemy[rnd], spawn.position, Quaternion.identity);
                obj.GetComponent<Enemy>().PassSpawner(this);
            }
            Debug.Log(Remaining);
        }
    }
}
