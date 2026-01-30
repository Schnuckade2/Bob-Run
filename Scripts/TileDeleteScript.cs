using UnityEngine;

public class TileDeleteScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }
}
