using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    [SerializeField] float padding = 0.4f;
    private float healthbarSize = 1f;

    private Vector2 moveInput;

    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] Transform gun3;
    [SerializeField] float fireRate;
    private int firingLevel = 0;

    private float autoUpgradeTimer = 0f;
    private float autoHealTimer = 0f;

    private List<Func<IEnumerator>> firingLevelPattern;

    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject shipExplosion;
    void Start()
    {
        firingLevelPattern = new List<Func<IEnumerator>>
        {
            LoopFireLevel1,
            LoopFireLevel2,
            LoopFireLevel3,
            LoopFireLevel4,
            LoopFireLevel5,
        };
        hitpoint = maxHitpoint;
        FindBoundaries();
        StartCoroutine(firingLevelPattern[firingLevel]());
    }

    void FindBoundaries()
    {
        Camera mainCamera = Camera.main;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void MovePlayer()
    {
        Vector2 deltaFromInput = moveInput * speed * Time.deltaTime;

        float trueDeltaX = Mathf.Clamp(transform.position.x + deltaFromInput.x, minX, maxX);
        float trueDeltaY = Mathf.Clamp(transform.position.y + deltaFromInput.y, minY, maxY);

        transform.position = new Vector3(trueDeltaX, trueDeltaY, 0);
    }
    void Update()
    {
        MovePlayer();
        autoHealTimer += Time.deltaTime;
        autoUpgradeTimer += Time.deltaTime;
        if (autoHealTimer > 10f && hitpoint < maxHitpoint) 
        {
            autoHealTimer = 0f;
            TakeDamage(-20f);
        }
        if (autoUpgradeTimer > 2f && firingLevel < 5)
        {
            autoUpgradeTimer = 0f;
            firingLevel++;
        }
        
    }
    IEnumerator LoopFireLevel1()
    {
        yield return new WaitForSeconds(fireRate);
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        SoundManager.instance.playShootSound();
        if(firingLevel == 1)
        {
            StartCoroutine(LoopFireLevel1());
        }
        else
        {
            StartCoroutine(firingLevelPattern[firingLevel]());
        }

    }
    IEnumerator LoopFireLevel2()
    {
        yield return new WaitForSeconds(fireRate-0.2f);
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        SoundManager.instance.playShootSound();
        if (firingLevel == 2)
        {
            StartCoroutine(LoopFireLevel2());
        }
        else
        {
            StartCoroutine(firingLevelPattern[firingLevel]());
        }

    }
    IEnumerator LoopFireLevel3()
    {
        yield return new WaitForSeconds(fireRate);
        Instantiate(playerBullet, gun1.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        SoundManager.instance.playShootSound();
        if (firingLevel == 3)
        {
            StartCoroutine(LoopFireLevel3());
        }
        else
        {
            StartCoroutine(firingLevelPattern[firingLevel]());
        }

    }
    IEnumerator LoopFireLevel4()
    {
        yield return new WaitForSeconds(fireRate - 0.2f);
        Instantiate(playerBullet, gun1.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        SoundManager.instance.playShootSound();
        if (firingLevel == 4)
        {
            StartCoroutine(LoopFireLevel4());
        }
        else
        {
            StartCoroutine(firingLevelPattern[firingLevel]());
        }

    }
    IEnumerator LoopFireLevel5()
    {
        yield return new WaitForSeconds(fireRate - 0.2f);
        Instantiate(playerBullet, gun1.position, Quaternion.Euler(0, 0, -80));
        Instantiate(playerBullet, gun1.position, Quaternion.Euler(0, 0, -100));
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun3.position, Quaternion.Euler(0, 0, -90));
        SoundManager.instance.playShootSound();

        StartCoroutine(LoopFireLevel5());
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy Bullets"))
        {
            TakeDamage(20f);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Small"))
        {
            TakeDamage(20f);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Medium"))
        {
            TakeDamage(60f);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Boss"))
        {
            TakeDamage(100f);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Missle"))
        {
            TakeDamage(40f);
        }
        if(hitpoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void TakeDamage(float damage)
    {
        if (damage >= 0)
        {
            SoundManager.instance.playBeingHitSound();
        }
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
