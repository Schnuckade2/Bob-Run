using UnityEngine;
using System.Collections;

public class PowerUpSpawnerScript : MonoBehaviour
{
    public Vector2 spawnPosition;
    public GameObject regenerationPowerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnPowerUp()
    {
        InvokeRepeating(nameof(SpawnPower), 0f, 3f);
    }




    void SpawnPower()
    {
        int num = Random.Range(1, 7);
        if (num == 6)
        {
            Instantiate(regenerationPowerUp, spawnPosition, Quaternion.identity);
        }
    }


}
