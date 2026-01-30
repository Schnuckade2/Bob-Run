using Unity.VisualScripting;
using UnityEngine;

public class Spawner2D : MonoBehaviour
{
     // Dein 2D-Prefab
    public Vector2 groundSpawnPosition;
    public Vector2 spikeSpawnPosition;
    public Transform basicGround; // wird automatisch gesetzt
    public Transform spike;




        void Start()
    {

        // Alle 6 Sekunden "SpawnWay" aufrufen, ab 0 Sekunden Start
        InvokeRepeating(nameof(SpawnWay), 0f, 6f);
        InvokeRepeating(nameof(SpawnWay), 0f, 1f);
    }

    void SpawnWay()
    {
        Instantiate(basicGround, groundSpawnPosition, Quaternion.identity);
        
        int randint = Random.Range(1, 9);
        if (randint == 8)
        {
            Instantiate(spike, spikeSpawnPosition, Quaternion.identity);
        }
    }
}
