using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner1 : MonoBehaviour
{

    public int slimeCounter;
    public int batCounter;



    [SerializeField]
    private GameObject slimePrefab;
    [SerializeField]
    private GameObject batPrefab;

    [SerializeField]
    private float slimeInterval = 3.5f;
    [SerializeField]
    private float batInterval = 7.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnSlime(slimeInterval, slimePrefab));
        StartCoroutine(spawnBat(batInterval, batPrefab));
    }

    private IEnumerator spawnBat(float interval, GameObject bat)
    {
        float localInterval = interval;
        if (batCounter > 5)
        {
            localInterval -= 1f;
        }
        yield return new WaitForSeconds(localInterval);
        GameObject newEnemy = Instantiate(bat, new Vector3(Random.Range(-5f, 5), 5.5f, 0), Quaternion.identity);
        batCounter++;
        Debug.Log("Bat Counter: " + batCounter);
        StartCoroutine(spawnBat(interval, bat));
    }

    private IEnumerator spawnSlime(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        int value = (int) Random.Range(0f, 2f);
        // if value == 0, left side, value == 1, right side
        GameObject newEnemy = Instantiate(enemy, new Vector2(-9.5f + (19f * value), Random.Range(-6f, 6f)), Quaternion.identity);
        StartCoroutine(spawnSlime(interval, enemy));
    }
}
