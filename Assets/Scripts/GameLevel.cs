using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public List<LightSource> lightSources;
    public List<LightReceiver> lightReceivers;

    public void Init()
    {
        lightSources = GetComponentsInChildren<LightSource>().ToList();
        lightReceivers = GetComponentsInChildren<LightReceiver>().ToList();
    }
    
    public bool WinConditionFulfilled()
    {
        foreach (var receiver in lightReceivers)
        {
            if (!receiver.ReceivingLight())
            {
                return false;
            }
        }

        return true;
    }
}
