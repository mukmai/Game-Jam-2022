using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public void EnterGravityField(Particle particle)
    {
        particle.UpdateMoveDirection(transform.forward);
    }

}
