﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Space]
    [Header("Base attributes:")]
    public float PLAYER_BASE_SPEED = 2.5f;
    public float PLAYER_BASE_ACCELERATION = 0.5f;
    public float BULLET_BASE_SPEED = 5.0f;
    public float BULLET_DURATION = 3.0f;
    public float SLASH_DURATION = 0.125f;

    [Space]
    [Header("Character Statistics:")]
    public bool isShooting;
    public float movementSpeed;
    public Vector2 movementDirection;
    public static Vector2 aimDirection;
    public static Vector3 playerPosition;

    [Space]
    [Header("Component References:")]
    public Rigidbody2D rigidBody;
    public Animator animator;
    public GameObject crosshair;

    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;
    public GameObject slashPrefab;

    // Private attributes
    private GameObject slash;
    private float acceleratedSpeed;
    private int totalHealth = 10;
    private int currentHealth;

	void Start()
    {
        currentHealth = totalHealth;
        
		rigidBody.freezeRotation = true;
        updatePosition();
	}

	// Update stack
	void Update()
    {
        updatePosition();
        CheckInputs();
        Move();
        Animate();
        Aim();
        Shoot();
        Slash();
    }

    public void setTotalHealth(int newHealth)
    {
        totalHealth = newHealth;
    }

    public int getTotalHealth()
    {
        return totalHealth;
    }

    public void setCurrentHealth(int newHealth)
    {
        currentHealth = newHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }
    
    void updatePosition()
    {
        playerPosition = transform.position; 
    }

    void CheckInputs()
    {
        // How long the player has been accelerating
        acceleratedSpeed += Mathf.Clamp(Time.deltaTime * PLAYER_BASE_ACCELERATION, 0.0f, 1.0f);
            
        // If player is moving
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // Get a movement direction vector from user input and normalize it. Get speed from this vector. 
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementDirection.Normalize();
            movementSpeed = Mathf.Clamp(acceleratedSpeed, 0.0f, 1.0f);
        }
        // If player is standing still
        else
        {
            // Reset accleration timer and set movement speed to zero
            acceleratedSpeed = 0f;
            movementSpeed = 0;
        }          

        // Get the aim direction vector from target position to player position
        aimDirection = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
        aimDirection = aimDirection - (Vector2)transform.position; 

        // isShooting is true when Fire1 button is pressed
        isShooting = Input.GetButtonUp("Fire1");
    }

    void Move()
    {
        // Move the player's rigid body in the movement direction (based on user input)
        rigidBody.velocity = movementDirection * movementSpeed * PLAYER_BASE_SPEED;
        // If Player is slashing (a slash prefab exists), then keep the slash in front of Player at all times
        if (slash != null)
            slash.GetComponent<Rigidbody2D>().position = movementDirection*0.5f + (Vector2)transform.position;
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position + (Vector3)aimDirection.normalized * 1.4f, Quaternion.identity);

            //// Give bullet a velocity in the shooting direction
            //bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * BULLET_BASE_SPEED;
            //// Destroy bullet object after some duration
            //Destroy(bullet, BULLET_DURATION);
        }
    }

    void Slash()
    {
        if (Input.GetButton("Jump"))
            // Prevents user from attacking again until the slash is complete
            if (slash == null)
            {
                // If the player is moving
                if (movementDirection != Vector2.zero)
                {
                    // Instantiate slash game object
                    slash = Instantiate(slashPrefab, movementDirection * 0.5f + (Vector2)transform.position, Quaternion.identity);
                }
                // If the player is standing still
                else
                {
                    //Unlike above, this instantiation uses different vectors to determine where the prefab will spawn to account for the player standing still.
                    //In this case, slash prefab does not get a velocity from the player, since they are standing still.
                    slash = Instantiate(slashPrefab, movementDirection * 0.5f + (Vector2)transform.position, Quaternion.identity);
                }
                // Destroy slash object after some duration
                Destroy(slash, SLASH_DURATION);
            }
    }

}
