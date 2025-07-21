using System.Collections;
using UnityEngine;

public class Enemy5Script : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] public float rotationSpeed = 50f;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float maxHitpoint = 20f;
    private float hitpoint = 20f;
    private float healthbarSize = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [SerializeField] public float rotationDuration = 2f;
    [SerializeField] public float rotationStart = 1f;
    private float rotationTimer = 0f;
    private bool rotating = false;

    [SerializeField] Transform gun1;
    [SerializeField] GameObject enemyBullet;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;
    void Start()
    {
        FindBoundaries();
        hitpoint = maxHitpoint;
        StartCoroutine(Shoot());
    }
    void FindBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 3;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + 3;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 3;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 3;
        Debug.Log("minX: " + minX + ", maxX: " + maxX + ", minY: " + minY + ", maxY: " + maxY);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        rotationTimer += Time.deltaTime;
        Debug.Log("Rotating: " + rotating);
        if (rotationTimer >= rotationStart && rotationTimer - rotationStart < rotationDuration)
        {
            rotating = true; 
        }
        if (rotationTimer - rotationStart >= rotationDuration)
        {
            rotating = false;
        }
        if (rotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        }
        DestroyOnOutOfBoundaries();
    }

    public void Fire()
    {
        Instantiate(enemyBullet, gun1.position, Quaternion.Euler(0, 0, 90));
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(fireRate);
        Fire();
        StartCoroutine(Shoot());
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
            GameSessionScript.instance.AddScore(20);
            GameObject explosion = Instantiate(shipExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, 0.4f);
        }
    }

    void DestroyOnOutOfBoundaries()
    {
        if (transform.position.x < minX ||
            transform.position.x > maxX ||
            transform.position.y < minY ||
            transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
