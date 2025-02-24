using UnityEngine;
using UnityEngine.InputSystem; // Make sure you've installed / set up the new Input System

public class HandSwitch : MonoBehaviour
{
    [Header("References to Hands")]
    public GameObject physicalHand;           // Root object for the physical hand (with Rigidbody, etc.)
    public GameObject animatedHand;           // Root object for the animated hand (child of the controller)
    public Transform physicalHandTransform;   // Transform of the physical hand root
    public Transform animatedHandTransform;   // Transform of the animated hand root

    [Header("Input Action")]
    // This reference lets you pick an action from the new Input System in the Inspector
    public InputActionReference actionReference;

    private bool currentlyUsingAnimatedHand = false;

    private void OnEnable()
    {
        // Enable the action and subscribe to events
        if (actionReference != null && actionReference.action != null)
        {
            actionReference.action.Enable();
            actionReference.action.performed += OnActionPerformed;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe and disable the action
        if (actionReference != null && actionReference.action != null)
        {
            actionReference.action.performed -= OnActionPerformed;
            actionReference.action.Disable();
        }
    }

    private void Start()
    {
        // Start with physical hand active, animated hand inactive
        physicalHand.SetActive(true);
        animatedHand.SetActive(false);
        currentlyUsingAnimatedHand = false;
    }

    /// <summary>
    /// Called whenever the assigned InputAction is triggered (e.g. button pressed).
    /// </summary>
    /// <param name="ctx"></param>
    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        // Toggle the hand each time the action is performed
        if (currentlyUsingAnimatedHand)
        {
            SwitchToPhysicalHand();
        }
        else
        {
            SwitchToAnimatedHand();
        }
    }

    private void SwitchToAnimatedHand()
    {
        // Snap the animated hand to the physical hand's position/rotation
        animatedHandTransform.position = physicalHandTransform.position;
        animatedHandTransform.rotation = physicalHandTransform.rotation;

        // Enable animated, disable physical
        animatedHand.SetActive(true);
        physicalHand.SetActive(false);

        currentlyUsingAnimatedHand = true;
    }

    private void SwitchToPhysicalHand()
    {
        // Snap the physical hand to the animated hand's position/rotation
        physicalHandTransform.position = animatedHandTransform.position;
        physicalHandTransform.rotation = animatedHandTransform.rotation;

        // Enable physical, disable animated
        physicalHand.SetActive(true);
        animatedHand.SetActive(false);

        currentlyUsingAnimatedHand = false;
    }
}
