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
    [EnumFlag] public ColorCode colorCode;
    [SerializeField] private VolumetricLines.VolumetricLineBehavior line;

    public void Init(Vector3 forward, ColorCode colorCode)
    {
        line.StartPos = Vector3.forward * 0.025f;
        line.EndPos = Vector3.forward * -0.025f;
        UpdateMoveDirection(forward);
    }

    public void UpdateMoveDirection(Vector3 direction)
    {
        transform.forward = direction;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        int receiverMask = 1 << 10;
        int particleMask = 1 << 2;
        int gravityFieldMask = 1 <<11;
        if (Physics.Raycast(ray, out hitData, collider.radius, ~(receiverMask | particleMask | gravityFieldMask)))
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

        Ray rayCheckGravityF = new Ray(transform.position, transform.forward);
        RaycastHit[] hitGravityF;
        hitGravityF = Physics.RaycastAll(rayCheckGravityF, collider.radius, gravityFieldMask);
        for (int i = 0; i < hitGravityF.Length; i++)
        {
            RaycastHit hit = hitGravityF[i];
            GravityField gravityF = hit.transform.GetComponent<GravityField>();
            if (gravityF)
            {
                gravityF.EnterGravityField(this);
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
