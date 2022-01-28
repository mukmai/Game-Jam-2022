using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private List<LightSource> lightSources;
    [SerializeField] private List<LightReceiver> lightReceivers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
