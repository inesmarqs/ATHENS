using UnityEngine;

public class ArmThrowPotion : MonoBehaviour
{
    public GameObject potionPrefab; // Reference to the potion prefab
    public Transform player; // Reference to the player's transform
    public float throwForceMultiplier = 2f; // Multiplier for the throw force

    private GameObject currentPotion; // Reference to the currently held potion
    private Vector3 initialControllerPosition; // Initial position of the controller for calculating throw direction

    void Update()
    {
        // Check for input to initiate potion throw
        if (Input.GetButtonDown("Throw") && currentPotion != null)
        {
            StartThrow();
        }

        // Check for input to end potion throw
        if (Input.GetButtonUp("Throw") && currentPotion != null)
        {
            EndThrow();
        }
    }

    void StartThrow()
    {
        // Record the initial position of the controller
        initialControllerPosition = GetControllerPosition();

        // Attach the potion to the controller
        currentPotion.transform.parent = transform;
        currentPotion.transform.localPosition = Vector3.zero;

        // Enable physics on the potion
        Rigidbody potionRB = currentPotion.GetComponent<Rigidbody>();
        potionRB.isKinematic = false;
    }

    void EndThrow()
    {
        // Calculate throw direction based on the difference between initial and current controller positions
        Vector3 throwDirection = GetControllerPosition() - initialControllerPosition;
        throwDirection.Normalize();

        // Apply throw force to the potion
        Rigidbody potionRB = currentPotion.GetComponent<Rigidbody>();
        potionRB.AddForce(throwDirection * throwForceMultiplier, ForceMode.Impulse);

        // Detach the potion from the controller
        currentPotion.transform.parent = null;

        // Listen for the potion to land
        StartCoroutine(DetectPotionLanding());
    }

    IEnumerator DetectPotionLanding()
    {
        yield return new WaitUntil(() => currentPotion.GetComponent<Rigidbody>().velocity.magnitude == 0f);

        // Teleport the player to the potion's position
        player.position = currentPotion.transform.position;

        // Destroy the potion object
        Destroy(currentPotion);
    }

    Vector3 GetControllerPosition()
    {
        // Get the position of the motion controller
        // Replace this with the appropriate method to get controller position from your VR SDK
        return transform.position;
    }

    // Called when the player interacts with the potion object
    public void PickupPotion(GameObject potion)
    {
        currentPotion = potion;
    }
}
