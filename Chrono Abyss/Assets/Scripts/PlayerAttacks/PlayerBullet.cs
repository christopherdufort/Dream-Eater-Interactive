using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float attackValue;

    public const float base_BULLET_DURATION = 3.0f; // constant base to always refer back against
    public float BULLET_DURATION = base_BULLET_DURATION;
    public float BULLET_BASE_SPEED = 1f;
    public Rigidbody2D rigidBody;
    public Vector2 trajectory;
    public float bulletTimer;

    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        bulletTimer = 0;
        trajectory = PlayerController.aimDirection.normalized;

        float angle = Mathf.Atan2(PlayerController.aimDirection.normalized.y, PlayerController.aimDirection.normalized.x) * Mathf.Rad2Deg;

        // rotate sprite to face cursor
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // TO-DO: Reimplement time factor (movement-time)
        rigidBody.velocity = trajectory * BULLET_BASE_SPEED /** Mathf.Clamp(PlayerController.movementSpeed, 0.0125f, 1.0f)*/;

        gameController = GameObject.Find("GameController");
        attackValue += gameController.GetComponent<GameController>().playerData.Dexterity;
        BULLET_DURATION = base_BULLET_DURATION + gameController.GetComponent<GameController>().playerData.Skill;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = trajectory * BULLET_BASE_SPEED /** Mathf.Clamp(PlayerController.movementSpeed, 0.0125f, 1.0f)*/;

        bulletTimer += Time.deltaTime /** Mathf.Clamp(PlayerController.movementSpeed, 0.0125f, 1.0f)*/;

        if (bulletTimer > BULLET_DURATION)
            Destroy(gameObject);
    }
}