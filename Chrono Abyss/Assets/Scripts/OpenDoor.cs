using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Door Sprites")]
    public Sprite closedDoor;
    public Sprite openDoor;

    private SpriteRenderer spriteRend;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        spriteRend.sprite = openDoor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        spriteRend.sprite = closedDoor;
    }
}

