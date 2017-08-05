using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] ScrapPrefab;
    public GameObject AmmoPrefab;
    public AudioClip shoot;
    public AudioClip gethit;
    public AudioClip death;
    private AudioSource source;
    private EnemySpawner spawner;
    public float FireSpeed;
    private bool CanFire = true;
    public float Speed;
    private int _Health;
    public int Damage;
    public int Armor;

    public int Health
    {
        get { return _Health; }
        set { if (value > MaxHealth) _Health = MaxHealth;
            else if (value < 0) _Health = 0;
            else _Health = value;
        }
    }
    public int MaxHealth;
    public bool Moving;
    private bool Facing;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.pitch = Random.Range(0.7f, 1.23f);
        Health += MaxHealth;
        StartCoroutine(FindPlayer());
        FireSpeed = Random.Range(1f, 3f);
    }
    public void PassSpawner(EnemySpawner get)
    {
        spawner = get;  
    }

    private void Update()
    {
        if(Player != null)
        {
            ChasePlayer();
            Shoot();
        }

        if(Health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ammo"))
        {
            source.PlayOneShot(gethit);
            int damage = Mathf.Clamp(collision.GetComponent<Ammo>().Damage - Armor, 0, MaxHealth);

            Health -= damage;
            Destroy(collision.gameObject);
        }
    }

    private void ChasePlayer()
    {
            if (Player.transform.position.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
                GetComponent<SpriteRenderer>().flipX = false;
        if (!Moving)
            return;
        Vector2 direction = Vector2.ClampMagnitude(Player.transform.position - transform.position, 1);
        transform.Translate(direction * Speed * Time.deltaTime);
    }
    private void Shoot()
    {
        if(CanFire)
        {
            source.PlayOneShot(shoot);
            CanFire = false;
            Vector2 facing = Vector2.ClampMagnitude(Player.transform.position - transform.position, 1);

            Quaternion ammoRotation = Quaternion.FromToRotation(Vector2.up, facing);
            GameObject ammo = Instantiate(AmmoPrefab, transform.position, ammoRotation);
            ammo.GetComponent<Ammo>().direction = Vector2.up;
            ammo.GetComponent<Ammo>().Damage = Damage;

            StartCoroutine(ShootCooldown(FireSpeed));
        }
    }

    private void Die()
    {
        source.PlayOneShot(death);
        spawner.Remaining--;
        int rnd = Random.Range(1, 5);
        for (int i = 0; i < rnd; i++)
        {
            int rnd2 = Random.Range(0, ScrapPrefab.Length - 1);
            GameObject scrap = Instantiate(ScrapPrefab[rnd2], transform.position, transform.rotation);

            int amount = Random.Range(1, 3);
            Vector2 randomDirection = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector2.up;
            
            scrap.GetComponent<Rigidbody2D>().AddForce(randomDirection,ForceMode2D.Impulse);
        } 
        GameManager.instance.EnemyDied();
        Destroy(gameObject);
    }

    private IEnumerator FindPlayer()
    {
        while(Player == null)
        {
            Player = GameObject.Find("Player");
            yield return null;
        }
    }
    IEnumerator ShootCooldown(float speed)
    {
        yield return new WaitForSeconds(1 / speed);
        CanFire = true;
    }
}
