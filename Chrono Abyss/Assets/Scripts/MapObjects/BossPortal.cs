using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			other.transform.GetComponentInChildren<TimeCreeperController>().NotifyPlayerInBossRoom(true);
			FindObjectOfType<AudioManager>().StopCurrent();
            SceneManager.LoadScene("BossFightScene", LoadSceneMode.Single);
        }
        
    }

    IEnumerator LoadAsyncScene(GameObject playerObject)
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();
        
        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BossFightScene", LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;


        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(playerObject, SceneManager.GetSceneByName("BossFightScene"));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
