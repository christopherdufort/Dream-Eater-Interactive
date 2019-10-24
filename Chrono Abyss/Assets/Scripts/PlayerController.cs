using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Space]
    [Header("Base attributes:")]
    public float PLAYER_BASE_SPEED = 2.5f;
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

    // Slash reference
    private GameObject slash;

    // Update stack
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
        // Get a movement direction vector from user input and normalize it. Get speed from this vector. 
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        
        // If player is moving
        if (movementDirection != Vector2.zero)
            // Get a look-at vector in the direction of where the player was last moving (already normalized). 
            lookAtDirection = movementDirection;

        // Get the aim direction vector from target position to player position
        aimDirection = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        aimDirection = aimDirection - (Vector2)transform.position; 
        // isShooting is true when Fire1 button is pressed
        isShooting = Input.GetButtonUp("Fire1");
    }

    void Move()
    {
        // Move the player's rigid body in the movement direction (based on user input)
        rb.velocity = movementDirection * movementSpeed * PLAYER_BASE_SPEED;
        // If Player is slashing (a slash prefab exists), then keep the slash in front of Player at all times
        if (slash != null)
            slash.GetComponent<Rigidbody2D>().position = lookAtDirection*0.5f + (Vector2)transform.position;
    }

    void Animate()
    {
        // Hand off movementDirection component values to animator (for blend tree)
        if (movementDirection != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movementDirection.x);
            animator.SetFloat("Vertical", movementDirection.y);
        }
        // Hand off moveSpeed value to animator (for blend tree)
        animator.SetFloat("Speed", movementSpeed);
    }

    void Aim()
    {
        // Change crosshair to position to where mouse is pointing (aimDirection)
        crosshair.transform.localPosition = aimDirection;
    }

    void Shoot()
    {
        // If shooting
        if (isShooting)
        {
            // Get normalized shooting direction from crosshair position (which is tied to mouse)
            Vector2 shootingDirection = crosshair.transform.localPosition;
            shootingDirection.Normalize();
            // Instantiate bullet object
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Give bullet a velocity in the shooting direction
            bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * BULLET_BASE_SPEED;
            // Destroy bullet object after some duration
            Destroy(bullet, BULLET_DURATION);
        }
    }

    void Slash()
    {
        if (Input.GetButton("Jump") == true) 
            // Prevents user from attacking again until the slash is complete
            if (slash == null)
                // If the player is moving
                if (movementDirection != Vector2.zero)
                {
                    // Instantiate slash game object
                    slash = Instantiate(slashPrefab, movementDirection * 0.5f + (Vector2)transform.position, Quaternion.identity);
                    // Destroy slash object after some duration
                    Destroy(slash, SLASH_DURATION);
                }
                // If the player is standing still
                else
                {
                    // Unlike above, this instantiation uses different vectors to determine where the prefab will spawn to account for the player standing still
                    slash = Instantiate(slashPrefab, lookAtDirection * 0.5f + (Vector2)transform.position, Quaternion.identity);
                    // In this case, slash prefab does not get a velocity from the player, since they are standing still.
                    Destroy(slash, SLASH_DURATION);
                }
    }

}
