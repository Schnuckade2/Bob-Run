using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;

    void Awake()
    {
        GameObject logicObj = GameObject.FindWithTag("Logic");

        if (logicObj != null)
        {
            target = logicObj.transform;
        }
        else
        {
            Debug.LogWarning("Kein Objekt mit dem Tag 'Logic' gefunden!");
        }
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            smoothSpeed * Time.deltaTime
        );
    }
}
