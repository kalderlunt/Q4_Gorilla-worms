using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionRadiusScript : MonoBehaviour
{
    [SerializeField] private int forceProjection = 5;
    private List<Collider2D> oldcolliders = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        foreach (Collider2D icollider in oldcolliders) //evite d'avoir 2 fois le meme object detecte
        {
            if (icollider == collision)
            {
                return;
            }
        }
        oldcolliders.Add(collision);

        if (collision.tag == "Map")
        {
            if (collision.GetComponent<Rigidbody2D>() == null)
            {
                collision.AddComponent<Rigidbody2D>();
                PhysicsMaterial2D mapmaterial = new PhysicsMaterial2D();
                mapmaterial.bounciness = .5f;
                collision.transform.localScale *= 0.9f;
                collision.GetComponent<Rigidbody2D>().sharedMaterial = mapmaterial;
            }
            Rigidbody2D maprb = collision.GetComponent<Rigidbody2D>();
            Vector2 dist = collision.GetComponent<Transform>().position - GetComponent<Transform>().position;
            maprb.velocity = -dist.normalized * forceProjection;
            maprb.angularVelocity = Random.Range(-200, 200);
        }
    }
}