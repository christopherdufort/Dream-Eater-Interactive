using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData { get; private set; }

    public enum PowerUp
    {
        InvincibilityPowerup,      //0 position    --  1 on keyboard
        RicochetBulletPowerup,     //1 position    --  2 on keyboard (OMITTED)
        InfiniteAmmoPowerup,       //2 position    --  2 on keyboard
        SpreadShotPowerup          //3 position    --  3 on keyboard
    }

    public Text Invincibility, InfiniteAmmo, SpreadShot;


    [Space]
    [Header("Base attributes:")]
    public const float PLAYER_BASE_SPEED = 2.5f; // constant base to always refer back against
    public float player_speed = PLAYER_BASE_SPEED;
    public float PLAYER_BASE_ACCELERATION = 0.5f;
    public float SLASH_DURATION = 0.125f;
    public const float BASE_POWER_UP_DURATION = 15.0f; // constant base to always refer back against
    public float power_up_duration = BASE_POWER_UP_DURATION;

    [Space]
    [Header("Character Statistics:")]
    public bool isShooting;
    public float movementSpeed;
    public Vector2 movementDirection;
    public static Vector2 aimDirection;
    public static Vector3 playerPosition;
    public int invincibilityCount = 0;
    public int ricochetBulletCount = 0;
    public int infiniteAmmoCount = 0;
    public int spreadShotCount = 0;
    public bool canPowerup = true; 
    public bool invincible = false;
    public bool richochet = false;
    public bool infiniteAmmo = false;
    public bool spreadShot = false;
    public bool resuming = false;

    [Space]
    [Header("Component References:")]
    public Rigidbody2D rigidBody;
    public Animator animator;
    public GameObject crosshair;

    [Space]
    [Header("Prefabs:")]
    public GameObject bulletPrefab;

	[Space]
	[Header("Time Creeper Handler:")]
	public TimeCreeperController timeCreeperController;

    public GameController gameController;
    public GameController[] gameControllers;

	[Space]
	[Header("Combat Attributes")]
    // Private attributes
    private bool created;
    private PlayerSlash sword;
    private float acceleratedSpeed;
    [SerializeField] private const int base_maxHealth = 10; // constant base to always refer back against
    [SerializeField] private int maxHealth = base_maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private const int base_maxAmmo = 6; // constant base to always refer back against
    [SerializeField] private int maxAmmo = base_maxAmmo;
    [SerializeField] public static int currentAmmo;
    private bool reloading;

    private void Awake()
    {
        int playerControllers = FindObjectsOfType<PlayerController>().Length;
        if (playerControllers != 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void OnDestroy()
    {
        Debug.Log("Player Destroyed, transfering data to GameController");
        gameController.invincibilityCount = this.invincibilityCount;
        gameController.infiniteAmmoCount = this.infiniteAmmoCount;
        gameController.spreadShotCount = this.spreadShotCount;

    }

    void Start()
    {
        // Destroy duplicate gameController
        gameControllers = FindObjectsOfType<GameController>();
        if (gameControllers.Length == 1)
        {
            gameController = FindObjectOfType<GameController>();
        }
        else if (gameControllers[0].initializationTime > gameControllers[1].initializationTime)
        {
            Destroy(gameControllers[0].gameObject);
            gameController = gameControllers[1];
        }
        else
        {
            Destroy(gameControllers[1].gameObject);
            gameController = gameControllers[0];
        }

        Debug.Log("The Player is starting");
        Time.timeScale = movementSpeed;
        Time.fixedDeltaTime = movementSpeed * 0.2f;

        // Apply loaded save game stats
        LevelUpPlayer();

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
        Reload();
        Slash();
    }

    // Called after loading save data and creating the player instance
    public void LevelUpPlayer()
    {
        // Always increment the value against the constant base to prevent double leveling
        this.maxHealth = base_maxHealth + gameController.playerData.Vitality;
        this.maxAmmo = base_maxAmmo + gameController.playerData.Attunement;
        this.power_up_duration = BASE_POWER_UP_DURATION + gameController.playerData.Intelligence;
        this.player_speed = PLAYER_BASE_SPEED + gameController.playerData.Agility;

        // Load existing powerups from gamecontroller (carried over from last level)
        this.invincibilityCount = gameController.invincibilityCount;
        this.infiniteAmmoCount = gameController.infiniteAmmoCount;
        this.spreadShotCount = gameController.spreadShotCount;

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
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
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
        if (!FindObjectOfType<GameController>().paused && !FindObjectOfType<GameController>().gameover)
        {
            // How long the player has been accelerating
            acceleratedSpeed += Mathf.Clamp(Time.deltaTime * PLAYER_BASE_ACCELERATION, 0.0125f, 1.0f);
            
        // If player is moving
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            // Get a movement direction vector from user input and normalize it. Get speed from this vector. 
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementDirection.Normalize();
            movementSpeed = Mathf.Clamp(acceleratedSpeed, 0.0125f, 1.0f);

            // send player direction to sword
            sword.setDir(movementDirection);
        }
        // If player is standing still
        else
        {
            // Reset acceleration timer and set movement speed to zero
            acceleratedSpeed = 0f;
            movementSpeed = !sword.isSlashing && !reloading ? 0.0125f : 0.0f; 
        }
        
        Time.timeScale = !sword.isSlashing ? movementSpeed : 1.0f;
        Time.fixedDeltaTime = !sword.isSlashing ? movementSpeed * 0.02f : 0.02f;


        // check that game is not paused or over

            // Get the aim direction vector from target position to player position
            aimDirection = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);
            aimDirection = aimDirection - (Vector2)transform.position; 

            if(!FindObjectOfType<GameController>().paused && !resuming)
            // isShooting is true when Fire1 button is pressed
            isShooting = Input.GetButtonUp("Fire1");
            
            // set time scale to player speed
            Time.timeScale = !sword.isSlashing && !reloading ? movementSpeed : 1.0f;
            Time.fixedDeltaTime = !sword.isSlashing && !reloading ? movementSpeed * 0.02f : 0.02f;
        }

        // Check if player is using a powerup
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivatePowerup(PowerUp.InvincibilityPowerup);
            StartCoroutine("InvincibilityDelay");
            StartCoroutine("PowerupDelay");

        }
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    ActivatePowerup(PowerUp.RicochetBulletPowerup);
        //}
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivatePowerup(PowerUp.InfiniteAmmoPowerup);
            StartCoroutine("InfiniteAmmoDelay");
            StartCoroutine("PowerupDelay");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivatePowerup(PowerUp.SpreadShotPowerup);
            StartCoroutine("SpreadShotDelay");
            StartCoroutine("PowerupDelay");

        }
    }

    void Move()
    {
        // Move the player's rigid body in the movement direction (based on user input)
        rigidBody.velocity = movementDirection * movementSpeed * player_speed;
		
		if (rigidBody.velocity.magnitude > 1f)
		{
			timeCreeperController.NotifyMeaningfulEvent();
		}
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
            if(!infiniteAmmo)
            currentAmmo--;

            FindObjectOfType<AudioManager>().Play("Shoot");
            // Get normalized shooting direction from crosshair position (which is tied to mouse)
            Vector2 shootingDirection = crosshair.transform.localPosition;
            shootingDirection.Normalize();

            if (!spreadShot)
                // Instantiate bullet object
                Instantiate(bulletPrefab, transform.position + (Vector3)aimDirection.normalized * 1.4f, Quaternion.identity);
            else
            {
                Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized) * 1.4f, Quaternion.identity);

                if (crosshair.transform.localPosition.x < 0f && crosshair.transform.localPosition.y > 0f)
                {
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(0.2f, 0.2f)) * 1.4f, Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(-0.2f, -0.2f)) * 1.4f, Quaternion.identity);
                }

                if (crosshair.transform.localPosition.x > 0f && crosshair.transform.localPosition.y > 0f)
                {
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(0.2f, -0.2f)) * 1.4f, Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(-0.2f, 0.2f)) * 1.4f, Quaternion.identity);
                }

                if (crosshair.transform.localPosition.x > 0f && crosshair.transform.localPosition.y < 0f)
                {
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(-0.2f, -0.2f)) * 1.4f, Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(0.2f, 0.2f)) * 1.4f, Quaternion.identity);
                }

                if (crosshair.transform.localPosition.x < 0f && crosshair.transform.localPosition.y < 0f)
                {
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(0.2f, -0.2f)) * 1.4f, Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + ((Vector3)aimDirection.normalized + new Vector3(-0.2f, 0.2f)) * 1.4f, Quaternion.identity);
                }
            }

            timeCreeperController.NotifyMeaningfulEvent();
        }else if(isShooting && currentAmmo == 0 && !FindObjectOfType<GameController>().paused && !resuming)
            FindObjectOfType<AudioManager>().Play("NoAmmo");     
    }

    void Reload()
    {
        if (Input.GetKey("r") && currentAmmo != maxAmmo && !reloading && !FindObjectOfType<GameController>().paused && !resuming)
        {
            FindObjectOfType<AudioManager>().Play("Reload");
            StartCoroutine("ReloadDelay");
		}
    }

    void Slash()
    {
        // A on Controller , Space on Keyboard
        if (Input.GetButton("Jump") && !sword.isSlashing)
        {
            // Prevents user from attacking again until the slash is complete
            sword.isSlashing = true;
			sword.slashDurationRemaining = SLASH_DURATION;
            GetComponentInChildren<PlayerSlash>().anim.SetTrigger("Slash");
            StartCoroutine("SlashDelay");

			timeCreeperController.NotifyMeaningfulEvent();
		}
    }

    IEnumerator SlashDelay()
    {
        yield return new WaitForSeconds(SLASH_DURATION);
        sword.isSlashing = false;
    }

    IEnumerator ReloadDelay()
    {
        reloading = true;
        yield return new WaitForSecondsRealtime(2f);
        currentAmmo = maxAmmo;
        reloading = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(!invincible)
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
        if (other.gameObject.CompareTag("Enemy") && !invincible)
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");

            // decrease health
            currentHealth--;
            
            // check if dead and tell game controller to end game
            if (currentHealth <= 0)
            {
                FindObjectOfType<GameController>().gameOver();
            }
        }
    }

    public void ActivatePowerup(PowerUp powerUp)
    {
        Debug.Log("Trying to use powerup "+ powerUp.ToString());
        Debug.Log("powerUp count:" + invincibilityCount + " " + infiniteAmmoCount + " " + ricochetBulletCount + " " + spreadShotCount);
        switch (powerUp)
        {
            case PowerUp.InvincibilityPowerup:
                if (invincibilityCount > 0 && canPowerup)
                {
                    Debug.Log("Player has activated powerup " + powerUp.ToString());
                    invincibilityCount--;
                    FindObjectOfType<AudioManager>().Play("PowerUp");
                    canPowerup = false;
                    invincible = true;
                    //Affect stats
                    //Start timer
                    FindObjectOfType<TimerController>().StartTime(power_up_duration);
                }
                break;
            case PowerUp.InfiniteAmmoPowerup:
                if (infiniteAmmoCount > 0 && canPowerup)
                {
                    Debug.Log("Player has activated powerup " + powerUp.ToString());
                    infiniteAmmoCount--;
                    FindObjectOfType<AudioManager>().Play("PowerUp");
                    canPowerup = false;
                    infiniteAmmo = true;
                    //Affect stats
                    //Start timer
                    FindObjectOfType<TimerController>().StartTime(power_up_duration);
                }
                break;
            case PowerUp.RicochetBulletPowerup:
                if (ricochetBulletCount > 0 && canPowerup)
                {
                    Debug.Log("Player has activated powerup " + powerUp.ToString());
                    ricochetBulletCount--;
                    //Affect stats
                    //Start timer
                    FindObjectOfType<TimerController>().StartTime(power_up_duration);
                }
                break;
            case PowerUp.SpreadShotPowerup:
                if (spreadShotCount > 0 && canPowerup)
                {
                    Debug.Log("Player has activated powerup " + powerUp.ToString());
                    spreadShotCount--;
                    FindObjectOfType<AudioManager>().Play("PowerUp");
                    spreadShot = true;
                    canPowerup = false;
                    //Affect stats
                    //Start timer
                    FindObjectOfType<TimerController>().StartTime(power_up_duration);
                }
                break;
            default:
                break;
        }
    }

    IEnumerator PowerupDelay()
    {
        yield return new WaitForSecondsRealtime(power_up_duration);
        canPowerup = true;
    }

    IEnumerator InvincibilityDelay()
    {     
        yield return new WaitForSecondsRealtime(power_up_duration);
        invincible = false;
        FindObjectOfType<AudioManager>().Play("PowerDown");
    }
    IEnumerator InfiniteAmmoDelay()
    {
        yield return new WaitForSecondsRealtime(power_up_duration);
        infiniteAmmo = false;
        FindObjectOfType<AudioManager>().Play("PowerDown");

    }

    IEnumerator SpreadShotDelay()
    {
        yield return new WaitForSecondsRealtime(power_up_duration);
        spreadShot = false;
        FindObjectOfType<AudioManager>().Play("PowerDown");

    }

    IEnumerator ResumeDelay()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        resuming = false;
    }

    public void CollectPowerup(String powerUpName)
    {
        Debug.Log("Player has collected powerup " + powerUpName);
        //TODO use PowerUp enums included above instead?
        if (powerUpName == "InvincibilityPowerup")
        {
            invincibilityCount++;
            Debug.Log("inv ++ " + invincibilityCount);
        }
        else if (powerUpName == "RicochetBulletPowerup")
        {
            ricochetBulletCount++;
            Debug.Log("ric ++ " + ricochetBulletCount);
        }
        else if (powerUpName == "InfiniteAmmoPowerup")
        {
            infiniteAmmoCount++;
            Debug.Log("inf ++ " + infiniteAmmoCount);
        }
        else if (powerUpName == "SpreadShotPowerup")
        {
            spreadShotCount++;
            Debug.Log("spr ++ " + spreadShotCount);
        }
    }
}
