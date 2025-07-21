using System.Collections;
using UnityEngine;

public class Boss3Script : MonoBehaviour
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

    [SerializeField] Transform outerGun1;
    [SerializeField] Transform outerGun2;
    [SerializeField] Transform outerGun3;
    [SerializeField] Transform outerGun4;
    [SerializeField] Transform outerGun5;



    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;

    [SerializeField] GameObject enemyChargedBullet;
    [SerializeField] GameObject enemySparkBullet;
    [SerializeField] GameObject enemySpreadBullet;
    [SerializeField] GameObject laserWithWarning;
    [SerializeField] GameObject enemyAimingMissile;




    private GameObject player;
    void Start()
    {
        hitpoint = maxHitpoint;


        FindBoundaries();

    }
    void FindBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        stopY = maxY-3f;
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
                StartCoroutine(ShootSpreadBulletsFromOuterGuns());
                StartCoroutine(ShootStraightBulletFromOuterGun());
                StartCoroutine(ShootLaserWithWarning());
                StartCoroutine(ShootAimingMissile());
            }
        }
        
        

    }

    


    IEnumerator ShootBulletsInConeShape()
    {

        for (int i = 0; i < 10; i++)
        {
            FireChargedBulletsFromGunInConeShape();
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(19f);
        StartCoroutine(ShootBulletsInConeShape());
    }
    IEnumerator ShootStraightBulletFromOuterGun()
    {
        yield return new WaitForSeconds(11f);
        for (int i = 0; i < 10; i++)
        {
            FireSraightBulletsFromOuterGun();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(9f);
        StartCoroutine(ShootStraightBulletFromOuterGun());
    }

    IEnumerator ShootSpreadBulletsFromOuterGuns()
    {
        yield return new WaitForSeconds(15f);
        for (int i = 0; i < 5; i++)
        {
            FireSpreadBulletsFromOuterGuns();
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShootSpreadBulletsFromOuterGuns());
    }

    IEnumerator ShootLaserWithWarning()
    {
        yield return new WaitForSeconds(4f);
        FireStraightLaserFromOuterGun();
        yield return new WaitForSeconds(2f);
        FireLeftToRightLaserFromOuterGun();
        yield return new WaitForSeconds(2f);
        FireRightToLeftLaserFromOuterGun();
        yield return new WaitForSeconds(14f);
        StartCoroutine(ShootLaserWithWarning());
    }
    
    IEnumerator ShootAimingMissile()
    {
        yield return new WaitForSeconds(20f);
        for (int i = 0; i < 3; i++)
        {
            FireAimingMissileFromGun();
            yield return new WaitForSeconds(0.2f);
        }
        StartCoroutine(ShootAimingMissile());
    }


    private void FireChargedBulletsFromGunInConeShape()
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

    private void FireSpreadBulletsFromOuterGuns()
    {
        Instantiate(enemySpreadBullet, outerGun1.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, outerGun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, outerGun3.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, outerGun4.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySpreadBullet, outerGun5.position, Quaternion.Euler(0, 0, -90));
    }

    private void FireSraightBulletsFromOuterGun()
    {
        Instantiate(enemyChargedBullet, outerGun1.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySparkBullet, outerGun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemySparkBullet, outerGun3.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemyChargedBullet, outerGun4.position, Quaternion.Euler(0, 0, -90));
        Instantiate(enemyChargedBullet, outerGun5.position, Quaternion.Euler(0, 0, -90));
    }
    private void FireStraightLaserFromOuterGun()
    {
        Instantiate(laserWithWarning, outerGun1.position, Quaternion.Euler(0, 0, 0));
        Instantiate(laserWithWarning, outerGun2.position, Quaternion.Euler(0, 0, 0));
        Instantiate(laserWithWarning, outerGun3.position, Quaternion.Euler(0, 0, 0));
        Instantiate(laserWithWarning, outerGun4.position, Quaternion.Euler(0, 0, 0));
        Instantiate(laserWithWarning, outerGun5.position, Quaternion.Euler(0, 0, 0));
    }
    private void FireRightToLeftLaserFromOuterGun()
    {
        Instantiate(laserWithWarning, outerGun1.position, Quaternion.Euler(0, 0, -45));
        Instantiate(laserWithWarning, outerGun2.position, Quaternion.Euler(0, 0, -45));
        Instantiate(laserWithWarning, outerGun3.position, Quaternion.Euler(0, 0, -45));
        Instantiate(laserWithWarning, outerGun4.position, Quaternion.Euler(0, 0, -45));
        Instantiate(laserWithWarning, outerGun5.position, Quaternion.Euler(0, 0, -45));
    }
    private void FireLeftToRightLaserFromOuterGun()
    {
        Instantiate(laserWithWarning, outerGun1.position, Quaternion.Euler(0, 0, 45));
        Instantiate(laserWithWarning, outerGun2.position, Quaternion.Euler(0, 0, 45));
        Instantiate(laserWithWarning, outerGun3.position, Quaternion.Euler(0, 0, 45));
        Instantiate(laserWithWarning, outerGun4.position, Quaternion.Euler(0, 0, 45));
        Instantiate(laserWithWarning, outerGun5.position, Quaternion.Euler(0, 0, 45));
    }

    private void FireAimingMissileFromGun()
    {
        Instantiate(enemyAimingMissile, gun1.position, Quaternion.Euler(0, 0, 0));
        Instantiate(enemyAimingMissile, gun2.position, Quaternion.Euler(0, 0, 0));
        Instantiate(enemyAimingMissile, gun3.position, Quaternion.Euler(0, 0, 0));
        Instantiate(enemyAimingMissile, gun4.position, Quaternion.Euler(0, 0, 0));
        Instantiate(enemyAimingMissile, gun5.position, Quaternion.Euler(0, 0, 0));
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
