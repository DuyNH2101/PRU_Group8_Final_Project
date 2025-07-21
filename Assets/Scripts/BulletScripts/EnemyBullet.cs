using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private Vector2 moveDirection = Vector2.down;

    void Start()
    {
        FindBoundaries();
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
            transform.position.y > maxY) {
            Destroy(gameObject);
        }
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void SetDirection(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        SetDirection(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized);
    }
}
