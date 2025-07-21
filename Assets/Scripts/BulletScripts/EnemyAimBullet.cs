using UnityEngine;

public class EnemyAimBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Vector2 moveDirection;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        FindBoundaries();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 bulletPos = transform.position;

            moveDirection = (playerPos - bulletPos).normalized;
        }
        else
        {
            moveDirection = Vector2.down;
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
    }
}

