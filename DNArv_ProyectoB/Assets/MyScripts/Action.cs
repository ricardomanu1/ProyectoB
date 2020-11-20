using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Action : MonoBehaviour
{
    public SteamVR_Action_Boolean press;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (press.stateDown)
            print("accion global");
    }
}
