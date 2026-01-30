using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour
{
    [Header("ParticleSystem")]
    public ParticleSystem bombParticles;
    public ParticleSystem explosionParticles;


    public CircleCollider2D circleCollider;

    [Header("Explosion Properties")]
    public float explosionRadius;




    void Start()
    {
        StartCoroutine(Explode());    
    }


    void Update()
    {
        bombParticles.Play();
    }

    IEnumerator Explode()
    {
        int explosionTime = Random.Range(2, 5);

        yield return new WaitForSeconds(explosionTime);


        explosionParticles.Play();
        circleCollider.isTrigger = true;
        circleCollider.offset = Vector3.zero;
        circleCollider.radius = explosionRadius;

        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);

    }
}
