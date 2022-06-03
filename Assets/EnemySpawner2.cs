using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{

    public int slimeCounter;
    public int batCounter;
    public int chargerCounter;

    public bool bossSpawned;

    private float timeKeeper;
    private float inGameTimer;

    [SerializeField]
    private GameObject slimePrefab;
    [SerializeField]
    private GameObject batPrefab;
    [SerializeField]
    private GameObject chargerPrefab;
    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private float slimeInterval = 4.0f;
    [SerializeField]
    private float batInterval = 6f;
    [SerializeField]
    private float chargerInterval = 12f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnSlime(slimeInterval, slimePrefab));
        StartCoroutine(spawnBat(batInterval, batPrefab));
        StartCoroutine(spawnCharger(chargerInterval, chargerPrefab));
    }

    void Update()
    {
        // 1 is the value of the increment for the timer
        if (timeKeeper + 1 <= Time.time)
        {
            inGameTimer += 1f;
            timeKeeper = Time.time;
        }
        // boss summon!!!
        if (inGameTimer > 67.5f && !bossSpawned)
        {
            // trigger boss!
            bossSpawned = true;
            int value = (int)Random.Range(0f, 2f);
            Debug.Log("Spawn boss!!!!");
/*            GameObject boss1 = Instantiate(bossPrefab, new Vector2(-7.95f + (15.9f * value), -1.5f), Quaternion.identity);
*/        }
    }

    private IEnumerator spawnBat(float interval, GameObject bat)
    {

        float localInterval = interval;
        // 22.5 seconds
        if (batCounter >= 4)
        {
            localInterval -= 1f;
        }
        // 35.5 seconds
        if (batCounter >= 8)
        {
            localInterval -= 0.5f;
        }
        // 47.5 seconds
        if (batCounter >= 12)
        {
            localInterval -= 1f;
        }
        if (bossSpawned)
        {
            localInterval = interval - 1f;
        }
        yield return new WaitForSeconds(localInterval);
        GameObject newEnemy = Instantiate(bat, new Vector3(Random.Range(-5f, 5), 5.5f, 0), Quaternion.identity);
        batCounter++;
        StartCoroutine(spawnBat(interval, bat));
    }

    private IEnumerator spawnSlime(float interval, GameObject enemy)
    {
        float localInterval = interval;
        if (slimeCounter >= 7)
        {
            localInterval -= 0.5f;
        }
        if (slimeCounter >= 11)
        {
            localInterval -= 0.5f;
        }
        if (slimeCounter >= 15)
        {
            localInterval -= 0.5f;
        }
        if (bossSpawned)
        {
            localInterval = interval - 0.5f;
        }
        yield return new WaitForSeconds(localInterval);
        int value = (int)Random.Range(0f, 2f);
        // if value == 0, left side, value == 1, right side
        GameObject newEnemy = Instantiate(enemy, new Vector2(-9.5f + (19f * value), Random.Range(-6f, 6f)), Quaternion.identity);
        slimeCounter++;
        StartCoroutine(spawnSlime(interval, enemy));
    }

    private IEnumerator spawnCharger(float interval, GameObject enemy)
    {
        float localInterval = interval;
        // 10 seconds
        if (chargerCounter >= 1)
        {
            localInterval -= 2f;
        }
        // 8 seconds
        if (chargerCounter >= 2)
        {
            localInterval -= 2f;
        }
        // 7.5 seconds
        if (chargerCounter >= 4)
        {
            localInterval -= 0.5f;
        }
        // 7 seconds
        if (chargerCounter >= 6)
        {
            localInterval -= 0.5f;
        }
        // 10 seconds
        if (bossSpawned)
        {
            localInterval = interval - 2f;
        }
        yield return new WaitForSeconds(localInterval);
        int value = (int)Random.Range(0f, 2f);
        // if value == 0, left side, value == 1, right side
        GameObject newEnemy = Instantiate(enemy, new Vector2(-9.5f + (19f * value), -1.86f), Quaternion.identity);
        chargerCounter++;
        StartCoroutine(spawnCharger(interval, enemy));
    }

}
