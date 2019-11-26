using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    // reference to player
    private PlayerController playerController;
    private float maxTime;
    private float currentTime;
    private bool timerGo;
    private RectTransform rectTransform;
    private Vector3 originalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerGo && currentTime > 0.005)
        {
            // decrease timer
            currentTime -= Time.deltaTime;
        
            // calculate health bar's new scale and pos
            float scale = currentTime / maxTime;
            float pos = calculateBarPos(scale);
        
            // apply transformations
            rectTransform.localScale = new Vector3(scale, 1, 1);
            rectTransform.position = new Vector3(pos + 100, rectTransform.position.y, rectTransform.position.z);
        }
        else
        {
            ResetTimer();
        }
    }

    public void StartTime(float time)
    {
        // set timer duration
        maxTime = time;
        currentTime = maxTime;
        
        // run timer
        timerGo = true;
    }

    private void ResetTimer()
    {
        timerGo = false;
        currentTime = maxTime;
        rectTransform.localScale = new Vector3(1, 1, 1);
        rectTransform.position = originalPos;
    }

    private float calculateBarPos(float barScale)
    {
        return (barScale * 60) - 20;
    }
}