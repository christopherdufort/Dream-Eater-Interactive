using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Space]
    [Header("Character attributes:")]
    public float BASE_SPEED = 2.5f;
    public float BULLET_BASE_SPEED = 5.0f;
    public float BULLET_DURATION = 3.0f;
    public float SLASH_DURATION = 0.125f;

    [Space]
    [Header("Character Statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;
    public Vector2 aimDirection;
    public bool isShooting;
    Vector2 lookAtDirection;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject crosshair;

    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;
    public GameObject slashPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        Move();
        Animate();
        Aim();
        Shoot();
        Slash();
    }

    void CheckInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
        if (movementDirection != Vector2.zero)
            lookAtDirection = movementDirection;
        aimDirection = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        aimDirection = aimDirection - (Vector2)transform.position; 
        isShooting = Input.GetButtonUp("Fire1");   
    }

    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * BASE_SPEED;
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }

        animator.SetFloat("Speed", movementSpeed);
    }

    void Aim()
    {
        crosshair.transform.localPosition = aimDirection;
    }

    void Shoot()
    {
        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();
        if (isShooting)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * BULLET_BASE_SPEED;
            Destroy(bullet, BULLET_DURATION);
        }
    }

    void Slash()
    {
        if (Input.GetButtonUp("Jump") == true) {
            if (movementDirection != Vector2.zero)
            {
                
                GameObject slash = Instantiate(slashPrefab, movementDirection + (Vector2)transform.position, Quaternion.identity);
                Destroy(slash, SLASH_DURATION);
            }
            else
            {
                GameObject slash = Instantiate(slashPrefab, lookAtDirection + (Vector2)transform.position, Quaternion.identity);
                Destroy(slash, SLASH_DURATION);
            }
        }

    }

}
