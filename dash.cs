using UnityEngine;

public class DashMovement : MonoBehaviour
{
    public float dashSpeed = 10f; // Speed of the dash movement
    public LayerMask obstacleMask; // Layer mask to detect obstacles

    private bool canDash = true; // Flag to indicate if dash is available
    private Transform vrCamera; // Reference to the VR camera

    void Start()
    {
        // Get the VR camera reference
        vrCamera = Camera.main.transform;
    }

    void Update()
    {
        // Check for trigger button input to dash
        if (Input.GetAxis("Trigger") > 0 && canDash)
        {
            // Determine dash direction using joystick input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 dashDirection = vrCamera.forward * vertical + vrCamera.right * horizontal;

            // Perform the dash movement
            StartCoroutine(PerformDash(dashDirection));
        }
    }

    IEnumerator PerformDash(Vector3 dashDirection)
    {
        // Disable dash during cooldown
        canDash = false;

        // Normalize dash direction and apply dash speed
        Vector3 dashVelocity = dashDirection.normalized * dashSpeed;

        // Move the player
        while (Vector3.Distance(transform.position, transform.position + dashVelocity) > 0.2f)
        {
            transform.position += dashVelocity * Time.deltaTime;
            yield return null;
        }

        // Ensure the player reaches exactly the target position
        transform.position += dashVelocity * Time.deltaTime;

        // Enable dash after cooldown
        yield return new WaitForSeconds(1f); // Cooldown time
        canDash = true;
    }
}
