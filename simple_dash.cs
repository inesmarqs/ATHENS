using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Dash : LocomotionProvider
{
  public float dashSpeed = 10f;
  
  // Listen to controller
  void Update()
  {
    if (Input.GetButtonDown("Dash"))
    {
      startDash();
    }
  }

  void startDash()
  {
  // Perform the dash action
  // For example, move the player forward with a high speed
  transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
  }
}

