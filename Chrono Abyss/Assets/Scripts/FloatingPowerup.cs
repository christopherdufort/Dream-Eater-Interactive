using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPowerup : MonoBehaviour
{
    float step;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        step = 0.3f * Time.deltaTime; // calculate distance to move
        targetPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // float powerup upwards
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // decrease health
            Destroy(this.gameObject);
            other.gameObject.GetComponent<PlayerController>().CollectPowerup(gameObject.tag);
        }
    }
}
