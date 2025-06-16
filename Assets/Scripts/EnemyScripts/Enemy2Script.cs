using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy2Script : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    private float healthbarSize = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;


    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] float fireRate;
    [SerializeField] GameObject enemyBullet;


    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;

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
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x  ;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void Update()
    {
        
    }
    public void Fire()
    {
        Instantiate(enemyBullet, gun1.position, Quaternion.Euler(0, 0, 90));
        Instantiate(enemyBullet, gun2.position, Quaternion.Euler(0, 0, 90));
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
            GameObject explosion = Instantiate(shipExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, 0.4f);
        }
    }
}
