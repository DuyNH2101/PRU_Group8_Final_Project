using System.Collections;
using UnityEngine;

public class Enemy8Script : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    private float healthbarSize = 1f;

    [SerializeField] float fireRate = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private float stopY;
    private bool hasReachedTop = false;


    [SerializeField] Transform gun1;


    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;

    [SerializeField] GameObject enemySpreadBullet;
    void Start()
    {
        hitpoint = maxHitpoint;
        FindBoundaries();
        StartCoroutine(Shoot());

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

    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(fireRate);
        Fire();
        StartCoroutine(Shoot());
    }

    public void Fire()
    {
        Instantiate(enemySpreadBullet, gun1.position, Quaternion.Euler(0, 0, -90));

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
