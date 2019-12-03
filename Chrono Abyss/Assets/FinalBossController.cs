using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawnPosition = new Vector3(0f, -5f, 0f);
    public GameObject[] bossPrefabs; // Only add Borgrimon here
    private int randomBossPosition;
    private GameObject bossObj;

    private void Start()
    {
        StartCoroutine("SetupBossFight");
    }

    IEnumerator SetupBossFight()
    {
        // bring over player
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPosition;

        // bring over boss
        randomBossPosition = Random.Range(0, bossPrefabs.Length);
        bossObj = Instantiate(bossPrefabs[randomBossPosition], transform.position, Quaternion.identity);
    }

    public void BossDied()
    {
        // You win go outside...
    }
}