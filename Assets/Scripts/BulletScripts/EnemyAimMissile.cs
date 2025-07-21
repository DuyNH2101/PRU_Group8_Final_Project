using UnityEngine;

public class EnemyAimMissile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Vector2 moveDirection;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [SerializeField] GameObject missleExplosion;
    void Start()
    {
        FindBoundaries();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 bulletPos = transform.position;

            moveDirection = (playerPos - bulletPos).normalized;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
        else
        {
            moveDirection = Vector2.down;
            transform.rotation = Quaternion.AngleAxis(-180f, Vector3.forward);
        }
    }

    void FindBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    void Update()
    {
        if (transform.position.x < minX ||
            transform.position.x > maxX ||
            transform.position.y < minY ||
            transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(missleExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 0.4f);
    }
}
