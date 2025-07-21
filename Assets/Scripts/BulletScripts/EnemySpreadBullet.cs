using UnityEngine;
using UnityEngine.EventSystems;

public class EnemySpreadBullet : MonoBehaviour
{
    [SerializeField] GameObject enemyBullet;
    [SerializeField] float explodeTime = 1f;
    [SerializeField] float speed = 5f;

    private float timer = 0f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;



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
            transform.position.y > maxY)
        {
            Destroy(gameObject);
        }


        timer += Time.deltaTime;
        if (timer >= explodeTime) 
        {
            SpreadBullet();
        }
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }

    void SpreadBullet()
    {



        GameObject spreadBullet1 = Instantiate(enemyBullet, transform.position, Quaternion.Euler(0, 0, 0));
        spreadBullet1.GetComponent<EnemyBullet>().SetDirection(-120);

        GameObject spreadBullet2 = Instantiate(enemyBullet, transform.position, Quaternion.Euler(0, 0, 0));
        spreadBullet2.GetComponent<EnemyBullet>().SetDirection(-100);

        GameObject spreadBullet3 = Instantiate(enemyBullet, transform.position, Quaternion.Euler(0, 0, 0));
        spreadBullet3.GetComponent<EnemyBullet>().SetDirection(-80);

        GameObject spreadBullet4 = Instantiate(enemyBullet, transform.position, Quaternion.Euler(0, 0, 0));
        spreadBullet4.GetComponent<EnemyBullet>().SetDirection(-60);

        Destroy(gameObject);
    }
}
