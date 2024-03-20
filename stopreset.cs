using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StopAndResetProvider : LocomotionProvider
{

    public InputActionReference Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.action?.triggered == true)
        {
            Debug.Log("ït works");
            var xrOrigin = system.xrOrigin;
            xrOrigin.RotateAroundCameraUsingOriginUp(180);
        }
    }
}
