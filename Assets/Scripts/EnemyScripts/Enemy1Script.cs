using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    private float healthbarSize = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;


    void Start()
    {
        FindBoundaries();
        hitpoint = maxHitpoint;
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
        DestroyOnOutOfBoundaries();
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
            
        }
        if (hitpoint <= 0)
        {
            Destroy(gameObject);
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
