﻿using System;
using System.Collections;
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

    // Private attributes
    private PlayerSlash sword;
    private bool slash;
    private float acceleratedSpeed;
    private int maxHealth = 10;
    private int currentHealth;
    private int maxAmmo = 6;
    private int currentAmmo;

    void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;

	rigidBody.freezeRotation = true;
        updatePosition();

        sword = GetComponentInChildren<PlayerSlash>();
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
        maxHealth = newHealth > 0 ? newHealth : maxHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setCurrentHealth(int newHealth)
    {
        currentHealth = newHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setMaxAmmo(int newAmmo)
    {
        maxAmmo = newAmmo > 0 ? newAmmo : maxAmmo;
    }

    public int getMaxAmmo()
    {
        return maxAmmo;
    }
    
    public void setCurrentAmmo(int newAmmo)
    {
        currentAmmo = newAmmo;
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;
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
            
            // send player direction to sword
            sword.setDir(movementDirection);
        }
        // If player is standing still
        else
        {
            // Reset accleration timer and set movement speed to zero
            acceleratedSpeed = 0f;
            movementSpeed = 0;
        }

        // check that game is not paused or over
        if (Time.timeScale >= 0.005f)
        {
            // Get the aim direction vector from target position to player position
            aimDirection = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
            aimDirection = aimDirection - (Vector2)transform.position; 

            // isShooting is true when Fire1 button is pressed
            isShooting = Input.GetButtonUp("Fire1");
        }
    }

    void Move()
    {
        // Move the player's rigid body in the movement direction (based on user input)
        rigidBody.velocity = movementDirection * movementSpeed * PLAYER_BASE_SPEED;
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
        if (isShooting && currentAmmo > 0)
        {
            currentAmmo--;
            
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
        if (Input.GetButton("Jump") && !slash)
        {
            // Prevents user from attacking again until the slash is complete
            slash = true;
            GetComponentInChildren<PlayerSlash>().anim.SetTrigger("Slash");
            StartCoroutine("SlashDelay");
        }
    }

    IEnumerator SlashDelay()
    {
        yield return new WaitForSeconds(SLASH_DURATION);
        slash = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // decrease health
            currentHealth--;
            
            // check if dead and tell game controller to end game
            if (currentHealth <= 0)
            {
                FindObjectOfType<GameController>().gameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // decrease health
            currentHealth--;
            
            // check if dead and tell game controller to end game
            if (currentHealth <= 0)
            {
                FindObjectOfType<GameController>().gameOver();
            }
        }
    }
}
