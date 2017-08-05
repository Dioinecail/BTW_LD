using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float FireSpeed;
    public int FireCost;
    
    public int Health
    {
        get { return GameManager.instance.Health; }
        set { if (value > MaxHealth) GameManager.instance.Health = MaxHealth;
            else if (value < 0) GameManager.instance.Health = 0;
            else GameManager.instance.Health = value;

            if (GameManager.instance.Health <= 0)
                Die(false);
            HealthBar.value = GameManager.instance.Health;
        }
    }
    public int MaxHealth;
    
    public int Energy
    {
        get { return GameManager.instance.Energy; }
        set {
            if (value >= MaxEnergy) GameManager.instance.Energy = MaxEnergy;
            else if (value <= 0) GameManager.instance.Energy = 0;
            else GameManager.instance.Energy = value;
            if (GameManager.instance.Energy <= 0)
                Die(true);

            EnergyBar.value = GameManager.instance.Energy;
        }
    }
    public int MaxEnergy;

    public Slider HealthBar;
    public Slider EnergyBar;
    public GameObject AmmoPrefab;
    private Animator Anima;
    private AudioSource source;
    public AudioClip shot;
    public AudioClip gethit;


    private Vector2 facing;

    private bool CanFire = true;
    private bool NorthWalled;
    private bool SouthWalled;
    private bool WestWalled;
    private bool EastWalled;

    private bool Interacting;
    
    private void Start()
    {
        GameManager.instance.PassStats(this);
        source = GetComponent<AudioSource>();
        Anima = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        if (Input.GetButton("Fire1"))
            Shoot();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10);
        facing = ray.GetPoint(10) - transform.position;
        facing = Vector2.ClampMagnitude(facing * 9999, 1);        
    }

    private void Move()
    {
        Vector2 direction = Vector2.zero;
        Energy -= 1;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        if (NorthWalled)
            direction.y = Mathf.Clamp(direction.y, -1, 0);
        if (SouthWalled)
            direction.y = Mathf.Clamp01(direction.y);
        if (WestWalled)
            direction.x = Mathf.Clamp01(direction.x);
        if (EastWalled)
            direction.x = Mathf.Clamp(direction.x, -1, 0);
                
        transform.Translate(direction * Speed * Time.deltaTime);
        if (direction != Vector2.zero)
        {
            Anima.SetBool("Walking", true);
        }
        else
            Anima.SetBool("Walking", false);

        if (direction.x > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if(direction.x < 0)
            GetComponent<SpriteRenderer>().flipX = false;


    }
    private void Shoot()
    {
        if (CanFire && !Interacting)
        {
            source.PlayOneShot(shot);
            CanFire = false;
            Quaternion ammoRotation = Quaternion.FromToRotation(Vector2.up, facing);
            GameObject ammo = Instantiate(AmmoPrefab, transform.position, ammoRotation);
            ammo.GetComponent<Ammo>().direction = Vector2.up;
            ammo.GetComponent<Ammo>().Damage = GameManager.instance.Damage;

            StartCoroutine(ShootCooldown(FireSpeed));
        }
    }
    private void Die(bool energy)
    {
        Health += MaxHealth;
        Energy += MaxEnergy;
        if(energy)
            UnityEngine.SceneManagement.SceneManager.LoadScene(3, UnityEngine.SceneManagement.LoadSceneMode.Single);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(4, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    private IEnumerator ShootCooldown(float fireSpeed)
    {
        yield return new WaitForSeconds(1 / fireSpeed);

        CanFire = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAmmo"))
        {
            source.PlayOneShot(gethit);
            int damage = Mathf.Clamp(collision.GetComponent<Ammo>().Damage - GameManager.instance.Armor, 0, MaxHealth);
            Health -= damage;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Interaction"))
            Interacting = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interaction"))
            Interacting = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D point in collision.contacts)
        {
            if (point.normal.y < 0)
            {
                SouthWalled = false;
                NorthWalled = true;
            }
            else if(point.normal.y > 0)
            {
                SouthWalled = true;
                NorthWalled = false;
            }
            else { }

            if(point.normal.x < 0)
            {
                EastWalled = true;
                WestWalled = false;
            }
            else if(point.normal.x > 0)
            {
                EastWalled = false;
                WestWalled = true;
            }
            else { }

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        NorthWalled = false;
        SouthWalled = false;
        EastWalled = false;
        WestWalled = false;

    }
    
}
