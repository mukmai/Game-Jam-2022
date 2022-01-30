using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    [SerializeField] private float force = 10;

    public void Init()
    {
        gameObject.layer = 11;
    }

    public void EnterGravityField(Particle particle)
    {
        Vector3 newDirection = (particle.transform.forward + transform.forward * force * Time.deltaTime).normalized;
        particle.UpdateMoveDirection(newDirection);
    }

}
