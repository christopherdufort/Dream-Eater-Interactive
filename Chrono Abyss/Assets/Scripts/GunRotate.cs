using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    // private attributes
    private Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }

    private void rotate()
    {

        Vector3 normalizedAimDir = PlayerController.aimDirection;
        normalizedAimDir.Normalize();
        transform.position = normalizedAimDir + PlayerController.playerPosition;

        float angle = Mathf.Atan2(normalizedAimDir.y, normalizedAimDir.x) * Mathf.Rad2Deg;

        // rotate sprite to face cursor
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}