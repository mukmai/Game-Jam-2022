using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public List<LightSource> lightSources;
    public List<LightReceiver> lightReceivers;
    public List<WaveParticleConverter> waveParticleConverters;

    public void Init()
    {
        lightSources = GetComponentsInChildren<LightSource>().ToList();
        lightReceivers = GetComponentsInChildren<LightReceiver>().ToList();
        foreach (var lightReceiver in lightReceivers)
        {
            lightReceiver.Init();
        }
        waveParticleConverters = GetComponentsInChildren<WaveParticleConverter>().ToList();
        foreach (var converter in waveParticleConverters)
        {
            converter.Init();
        }
        var rotatableObjects = GetComponentsInChildren<RotatableObject>().ToList();
        foreach (var rotatableObject in rotatableObjects)
        {
            rotatableObject.Init();
        }
        var dragAlongPathObjects = GetComponentsInChildren<DragAlongPathObject>().ToList();
        foreach (var dragAlongPathObject in dragAlongPathObjects)
        {
            dragAlongPathObject.Init();
        }
        var freeDragObjects = GetComponentsInChildren<FreeDragObject>().ToList();
        foreach (var freeDragObject in freeDragObjects)
        {
            freeDragObject.Init();
        }
        var gravityFields = GetComponentsInChildren<GravityField>().ToList();
        foreach (var gravityField in gravityFields)
        {
            gravityField.Init();
        }
    }
    
    public bool WinConditionFulfilled()
    {
        if (lightReceivers.Count == 0) return false;
        foreach (var receiver in lightReceivers)
        {
            if (!receiver.IsCompleted())
            {
                return false;
            }
        }

        return true;
    }
}
