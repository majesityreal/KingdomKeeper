using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{

    public int slimeCounter;
    public int batCounter;

    public bool bossSpawned;

    private float timeKeeper;
    private float inGameTimer;

    [SerializeField]
    private GameObject slimePrefab;
    [SerializeField]
    private GameObject batPrefab;
    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private float slimeInterval = 3.5f;
    [SerializeField]
    private float batInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnSlime(slimeInterval, slimePrefab));
        StartCoroutine(spawnBat(batInterval, batPrefab));
        AudioManager.Instance.Stop("TitleTheme");
        AudioManager.Instance.Play("Level1");
    }

    private void OnDestroy()
    {
        AudioManager.Instance.Stop("Level1");
    }

    void Update()
    {
        // 1 is the value of the increment for the timer
        if (timeKeeper + 1 <= Time.time)
        {
            inGameTimer += 1f;
            timeKeeper = Time.time;
        }
    }

    private IEnumerator spawnBat(float interval, GameObject bat)
    {

        float localInterval = interval;
        // 22.5 seconds
        if (inGameTimer > 22.5f)
        {
            localInterval -= 1f;
        }
        // 35.5 seconds
        if (inGameTimer > 35.5f)
        {
            localInterval -= 0.5f;
        }
        // 47.5 seconds
        if (inGameTimer > 47.5f)
        {
            localInterval -= 1f;
        }
        // 67.5 seconds
        if (inGameTimer > 67.5f && !bossSpawned)
        {
            // trigger boss!
            bossSpawned = true;
            int value = (int)Random.Range(0f, 2f);
            GameObject boss1 = Instantiate(bossPrefab, new Vector2(-7.95f + (15.9f * value), -1.5f), Quaternion.identity);
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
        if (inGameTimer > 20f)
        {
            localInterval -= 0.5f;
        }
        if (inGameTimer > 30f)
        {
            localInterval -= 0.5f;
        }
        if (inGameTimer > 45f)
        {
            localInterval -= 0.5f;
        }
        if (bossSpawned)
        {
            localInterval = interval - 0.5f;
        }
        yield return new WaitForSeconds(localInterval);
        int value = (int) Random.Range(0f, 2f);
        // if value == 0, left side, value == 1, right side
        GameObject newEnemy = Instantiate(enemy, new Vector2(-9.5f + (19f * value), Random.Range(-6f, 6f)), Quaternion.identity);
        StartCoroutine(spawnSlime(interval, enemy));
    }
}
