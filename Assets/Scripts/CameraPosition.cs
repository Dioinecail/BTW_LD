using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    Vector3 position;
    public EnemySpawner spawn;

    private void Start()
    {
        position = transform.position;
        position.z = -10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveCamera());
            if(spawn != null)
            spawn.SpawnEnemy();
        }
    }

    IEnumerator MoveCamera()
    {
        while(Camera.main.transform.position != position)
        {
            Time.timeScale = 0.01f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, position, 0.1f);
            yield return null;
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
