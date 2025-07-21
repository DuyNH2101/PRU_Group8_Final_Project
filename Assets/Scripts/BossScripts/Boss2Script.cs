using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boss2Script : MonoBehaviour
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

    private bool movingRight = true;
    [SerializeField] float horizontalSpeed = 1f;


    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] Transform gun3;
    [SerializeField] Transform gun4;
    [SerializeField] Transform gun5;

    [SerializeField] Transform laserGun1;
    [SerializeField] Transform laserGun2;


    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;

    [SerializeField] GameObject enemyChargedBullet;
    [SerializeField] GameObject enemySpreadBullet;
    [SerializeField] GameObject laserWithWarning;


    private GameObject player;
    void Start()
    {
        hitpoint = maxHitpoint;
        player = GameObject.FindGameObjectWithTag("Player");

        FindBoundaries();

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
                StartCoroutine(ShootBulletsInConeShape());
                StartCoroutine(ShootAimingLaser());
                StartCoroutine(ShootSpreadBulletsFromSideGuns());
            }
        }
        else
        {
            MoveSideToSide();
        }

    }

    void MoveSideToSide()
    {
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += horizontalSpeed * Time.deltaTime;

            if (pos.x >= maxX - 5f)
            {
                pos.x = maxX - 5f;
                movingRight = false;
            }
        }
        else
        {
            pos.x -= horizontalSpeed * Time.deltaTime;

            if (pos.x <= minX + 5f)
            {
                pos.x = minX + 5f;
                movingRight = true;
            }
        }

        transform.position = pos;
    }


    IEnumerator ShootBulletsInConeShape()
    {
       
        for (int i = 0; i < 10; i++)
        {
            FireChargedBulletsFromGun1();
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(7f);
        StartCoroutine(ShootBulletsInConeShape());
    }

    IEnumerator ShootSpreadBulletsFromSideGuns()
    {
        yield return new WaitForSeconds(8f);
        for (int i = 0;i < 3; i++)
        {
            FireSpreadBulletsFromSideGuns();
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShootSpreadBulletsFromSideGuns());
    }

    IEnumerator ShootAimingLaser()
    {
        yield return new WaitForSeconds(5f);
        FireAimingLaser();
        yield return new WaitForSeconds(5f);
        StartCoroutine(ShootAimingLaser());
    }

    private void FireChargedBulletsFromGun1()
    {
        //Bullets from gun1
        GameObject bullet1 = Instantiate(enemyChargedBullet, gun1.position, Quaternion.Euler(0, 0, 0));
        bullet1.GetComponent<EnemyBullet>().SetDirection(-130);

        GameObject bullet2 = Instantiate(enemyChargedBullet, gun1.position, Quaternion.Euler(0, 0, 0));
        bullet2.GetComponent<EnemyBullet>().SetDirection(-110);

        GameObject bullet3 = Instantiate(enemyChargedBullet, gun1.position, Quaternion.Euler(0, 0, 0));
        bullet3.GetComponent<EnemyBullet>().SetDirection(-90);

        GameObject bullet4 = Instantiate(enemyChargedBullet, gun1.position, Quaternion.Euler(0, 0, 0));
        bullet4.GetComponent<EnemyBullet>().SetDirection(-70);

        GameObject bullet5 = Instantiate(enemyChargedBullet, gun1.position, Quaternion.Euler(0, 0, 0));
        bullet5.GetComponent<EnemyBullet>().SetDirection(-50);

        //Bullets from side guns
        GameObject bullet6 = Instantiate(enemyChargedBullet, gun4.position, Quaternion.Euler(0, 0, 0));
        bullet6.GetComponent<EnemyBullet>().SetDirection(-130);

        GameObject bullet7 = Instantiate(enemyChargedBullet, gun5.position, Quaternion.Euler(0, 0, 0));
        bullet7.GetComponent<EnemyBullet>().SetDirection(-50);

        GameObject bullet8 = Instantiate(enemyChargedBullet, gun4.position, Quaternion.Euler(0, 0, 0));
        bullet8.GetComponent<EnemyBullet>().SetDirection(-150);

        GameObject bullet9 = Instantiate(enemyChargedBullet, gun5.position, Quaternion.Euler(0, 0, 0));
        bullet9.GetComponent<EnemyBullet>().SetDirection(-30);
    }

    private void FireSpreadBulletsFromSideGuns()
    {
        Instantiate(enemySpreadBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, laserGun1.position, laserGun1.rotation);
        Instantiate(enemySpreadBullet, laserGun2.position, laserGun2.rotation);
    }

    private void FireAimingLaser()
    {
        Vector2 moveDirectionForLaserGun1;
        Vector2 moveDirectionForLaserGun2;

        if (player != null)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 laserGun1Pos = laserGun1.position;
            Vector2 laserGun2Pos = laserGun2.position;


            moveDirectionForLaserGun1 = (playerPos - laserGun1Pos).normalized;
            moveDirectionForLaserGun2 = (playerPos - laserGun2Pos).normalized;

            float angleForLaserGun1 = Mathf.Atan2(moveDirectionForLaserGun1.y, moveDirectionForLaserGun1.x) * Mathf.Rad2Deg;
            laserGun1.rotation = Quaternion.AngleAxis(angleForLaserGun1 + 90f, Vector3.forward);

            float angleForLaserGun2 = Mathf.Atan2(moveDirectionForLaserGun2.y, moveDirectionForLaserGun2.x) * Mathf.Rad2Deg;
            laserGun2.rotation = Quaternion.AngleAxis(angleForLaserGun2 + 90f, Vector3.forward);

            Instantiate(laserWithWarning, laserGun1.position, laserGun1.rotation);
            Instantiate(laserWithWarning, laserGun2.position, laserGun2.rotation);


        }
        else
        {
            moveDirectionForLaserGun1 = Vector2.down;
            laserGun1.rotation = Quaternion.AngleAxis(0f, Vector3.forward);

            moveDirectionForLaserGun2 = Vector2.down;
            laserGun2.rotation = Quaternion.AngleAxis(0f, Vector3.forward);

            Instantiate(laserWithWarning, laserGun1.position, laserGun1.rotation);
            Instantiate(laserWithWarning, laserGun2.position, laserGun2.rotation);
        }
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
            GameSessionScript.instance.AddScore(500);
            GameObject explosion = Instantiate(shipExplosion, transform.position, Quaternion.identity);
            explosion.transform.localScale *= 2f;
            Destroy(explosion, 0.4f);
            SoundManager.instance.changeToNormalAudioClip();
        }
    }
}
