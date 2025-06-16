using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float maxHitpoint = 100f;
    private float hitpoint = 100f;
    [SerializeField] float padding = 0.4f;

    private Vector2 moveInput;

    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    [SerializeField] GameObject playerBullet;
    [SerializeField] Transform gun1;
    [SerializeField] Transform gun2;
    [SerializeField] float fireRate;

    void Start()
    {
        hitpoint = maxHitpoint;
        FindBoundaries();
        StartCoroutine(LoopFire());
        
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
    }
    IEnumerator LoopFire()
    {
        yield return new WaitForSeconds(fireRate);
        Instantiate(playerBullet, gun1.position, Quaternion.Euler(0, 0, -90));
        Instantiate(playerBullet, gun2.position, Quaternion.Euler(0, 0, -90));
        StartCoroutine(LoopFire() );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy Bullets"))
        {
            hitpoint -= 20f;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Small"))
        {
            hitpoint -= 20f;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Medium"))
        {
            hitpoint -= 60f;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy Boss"))
        {
            hitpoint -= 100f;
        }

        if(hitpoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
