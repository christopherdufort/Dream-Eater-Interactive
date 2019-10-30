using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    // private attributes
    private Transform transform;
    private Vector3 playerPos;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        //playerPos = GameObject.FindGameObjectsWithTag("Player")GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }

    private void rotate()
    {
        // get position of mouse cursor
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // rotate sprite to face cursor
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}