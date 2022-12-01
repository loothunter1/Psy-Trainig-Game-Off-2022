using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    public AudioClip collisionSound;
    public AudioSource collisionPoint;
    public float maxVolume = 0.5f;
    public float maxSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 colPos = collision.GetContact(0).point;
        collisionPoint.transform.position = colPos;
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb)
        {
            collisionPoint.volume = maxVolume * rb.velocity.magnitude / maxSpeed;
        }
        else
        {
            collisionPoint.volume = maxVolume;
        }
        collisionPoint.Play();
        Debug.Log("Collision at " + colPos);
    }
}
