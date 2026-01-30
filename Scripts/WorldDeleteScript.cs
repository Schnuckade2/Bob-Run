using UnityEngine;

public class WorldDeleteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -12.2)
        {
            Destroy(gameObject);
        }
    }
}
