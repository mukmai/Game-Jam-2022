using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private float _speed = 6;
    private float _lifeDuration = 10;
    private float _lifeEndTime;
    public Rigidbody rigidbody;
    public SphereCollider collider;
    [EnumFlag] public ColorCode colorCode;
    [SerializeField] private VolumetricLines.VolumetricLineBehavior line;
    public int power;
    public bool withinRefractionMedium;
    public ParticleLightRay sourceLightRay;

    public void Init(Vector3 forward, ParticleLightRay lightRay)
    {
        line.StartPos = Vector3.forward * 0.025f;
        line.EndPos = Vector3.forward * -0.025f;
        _lifeEndTime = Time.time + _lifeDuration;
        UpdateMoveDirection(forward);
        sourceLightRay = lightRay;
        power = lightRay.power;
        SetColor(lightRay.colorCode);
        withinRefractionMedium = false;
    }

    public void SetColor(ColorCode colorCode)
    {
        this.colorCode = colorCode;
        line.LineColor = colorCode.ToColor();
    }

    public void UpdateMoveDirection(Vector3 direction)
    {
        transform.forward = direction;
    }

    private void FixedUpdate()
    {
        if (_lifeEndTime < Time.time)
        {
            Remove();
            return;
        }
        
        transform.position += transform.forward * _speed * Time.deltaTime;
        
        int receiverMask = 1 << 10;
        int particleMask = 1 << 2;
        int gravityFieldMask = 1 <<11;

        // Ray rayCheckGravityF = new Ray(transform.position, transform.forward);
        // RaycastHit hitGravity;
        // if (Physics.Raycast(rayCheckGravityF, out hitGravity, collider.radius, gravityFieldMask))
        // {
        //     //call hitObject function
        //     hitGravity.transform.GetComponent<GravityField>().EnterGravityField(this);
        // }

        Collider[] gravityColliders = Physics.OverlapSphere(transform.position, collider.radius, gravityFieldMask);
        foreach (Collider gravityCollider in gravityColliders)
        {
            gravityCollider.GetComponent<GravityField>().EnterGravityField(this);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, collider.radius, ~(receiverMask | particleMask | gravityFieldMask)))
        {
            //call hitObject function
            hitData.transform.GetComponent<LightRayHitTarget>().HandleParticleInteraction(this, hitData.normal);
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

        if (withinRefractionMedium)
        {
            ray = new Ray(transform.position + transform.forward * collider.radius, -transform.forward);
            RaycastHit[] hitObjects;
            hitObjects = Physics.RaycastAll(ray, collider.radius, ~(receiverMask | particleMask | gravityFieldMask));
            for (int i = 0; i < hitObjects.Length; i++)
            {
                RaycastHit hit = hitObjects[i];
                RefractionMedium refractionMedium = hit.transform.GetComponent<RefractionMedium>();
                if (refractionMedium)
                {
                    refractionMedium.HandleParticleInteraction(this, hit.normal);
                    break;
                }

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
