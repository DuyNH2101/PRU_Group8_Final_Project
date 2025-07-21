using System.Collections;
using UnityEngine;

public class Enemy7Script : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    private float healthbarSize = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private float stopY;
    private bool hasReachedTop = false;
    private bool hasReachedPlayer = false;
    private float timer = 0f;
    private float trackPlayerCooldown = 3f;

    private Transform player;
    private float targetPlayerX;


    [SerializeField] Transform gun1;
    [SerializeField] float fireRate;
    [SerializeField] GameObject enemyLaser;


    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;

    void Start()
    {
        hitpoint = maxHitpoint;
        FindBoundaries();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
        }

        gun1.position.Set(gun1.position.x, gun1.position.y - 10f, gun1.position.z);
    }
    void FindBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        stopY = maxY - 2f;
    }

    void Update()
    {
        targetPlayerX = player.position.x;
        float currentX = transform.position.x;

        timer += Time.deltaTime;

        if(currentX != targetPlayerX && timer >= trackPlayerCooldown)
        {
            hasReachedPlayer = false;
            timer = 0f;
        }
        if (!hasReachedTop)
        {
            if (transform.position.y > stopY)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                hasReachedTop = true;
                transform.position = new Vector3(transform.position.x, stopY, transform.position.z);
            }
        }
        else
        {
            if (!hasReachedPlayer)
            {
                
                float step = speed * Time.deltaTime;

                
                transform.position = new Vector3(
                    Mathf.MoveTowards(currentX, targetPlayerX, step),
                    transform.position.y,
                    transform.position.z
                );

                
                if (Mathf.Abs(transform.position.x - targetPlayerX) < 0.1f)
                {
                    hasReachedPlayer = true;
                    Fire();
                }
            }
        }
    }
    public void Fire()
    {
        Instantiate(enemyLaser, gun1.position, Quaternion.Euler(0, 0, 0));
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullets"))
        {
            TakeDamage(20f);
        }
    }
    private void TakeDamage(float damage)
    {
        if (hitpoint > 0)
        {
            hitpoint -= damage;
            healthbarSize -= damage / maxHitpoint;
            healthBar.SetSize(healthbarSize);
            Debug.Log("Current HP: " + hitpoint + ", Current bar size:" + healthbarSize);
        }
        if (hitpoint <= 0)
        {
            Destroy(gameObject);
            GameSessionScript.instance.AddScore(100);
            GameObject explosion = Instantiate(shipExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, 0.4f);
        }
    }
}
