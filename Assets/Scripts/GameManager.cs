using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int startingHearts;
    public int pHearts;

    public HeartBrain[] hearts;


    // Start is called before the first frame update
    void Start()
    {
        pHearts = startingHearts;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        Debug.Log("It's over!");
    }

    public void DamageHeartUI()
    {
        pHearts--;

        // makes UI heart dissappear
        hearts[pHearts].Dissappear();

    }

}
