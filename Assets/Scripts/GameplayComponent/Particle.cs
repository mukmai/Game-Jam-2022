using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private float _speed = 7;
    public Rigidbody rigidbody;
    public SphereCollider collider;
    public void Init(Vector3 forward)
    {
        UpdateMoveDirection(forward);
    }

    public void UpdateMoveDirection(Vector3 direction)
    {
        transform.forward = direction;
        // rigidbody.velocity = transform.forward * _speed;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        // Collider[] hitColliders = Physics.OverlapSphere(transform.position, collider.radius);
        // List<EnemyRuntime> enemiesInRange = hitColliders.Where(x => x.gameObject.CompareTag("Enemy")).
        //     Select(x => x.gameObject.GetComponent<EnemyRuntime>()).ToList();
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        int receiverMask = 1 << 10;
        int particleMask = 1 << 2;
        if (Physics.Raycast(ray, out hitData, collider.radius, ~(receiverMask | particleMask)))
        {
            //call hitObject function
            hitData.transform.GetComponent<LightRayHitTarget>().HandleParticleInteraction(this);
        }

        Ray rayCheckReceiver = new Ray(transform.position, transform.forward);
        RaycastHit[] hitReceivers;
        hitReceivers = Physics.RaycastAll(rayCheckReceiver, collider.radius, receiverMask);
        for (int i = 0; i < hitReceivers.Length; i++)
        {
            RaycastHit hit = hitReceivers[i];
            LightReceiver hitReceiver = hit.transform.GetComponent<LightReceiver>();
            if (hitReceiver && !hitReceiver.IsWaveSensor)
            {
                hitReceiver.ReceivingLight(transform);
            }
        }
    }

    public void Remove()
    {
        ObjectPool.Instance.DestroyObject(gameObject);
    }
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     other.body.GetComponent<LightRayHitTarget>().HandleParticleInteraction(this);
    // }
}
