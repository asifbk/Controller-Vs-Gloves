using UnityEngine;
using UnityEngine.Events;
using SG;
using SGCore.Haptics;

public class SG_TouchButton : MonoBehaviour
{
    [Header("Button Event")]
    public UnityEvent onButtonPressed = new UnityEvent();

    [Header("Touch Detection")]
    public float pressDepth = 0.02f;
    public float pressCooldown = 1f;

    [Header("Haptic Feedback")]
    public bool hapticFeedbackEnabled = true;
    [Range(0f, 1f)]
    public float vibrationIntensity = 0.7f;
    public SG_HandSection vibrationLocation = SG_HandSection.Wrist;

    private bool canPress = true;
    private float cooldownTimer = 0f;
    private int touchCount = 0;

    private void Update()
    {
        if (!canPress)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= pressCooldown)
            {
                canPress = true;
                cooldownTimer = 0f;
                Debug.Log("[SG_TouchButton] Button ready to press again");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SG_TouchButton] Trigger ENTER from: {other.gameObject.name} on layer: {LayerMask.LayerToName(other.gameObject.layer)}");

        if (other.gameObject.name.Contains("Index") || 
            other.gameObject.name.Contains("Palm") ||
            other.gameObject.name.Contains("Thumb") ||
            other.gameObject.name.Contains("Middle"))
        {
            touchCount++;
            Debug.Log($"[SG_TouchButton] Touch count: {touchCount}");

            if (canPress)
            {
                Debug.Log("[SG_TouchButton] BUTTON PRESSED!");
                
                if (hapticFeedbackEnabled)
                {
                    SG_TrackedHand hand = FindTrackedHandInHierarchy(other.transform);
                    if (hand != null)
                    {
                        hand.SendImpactVibration(vibrationLocation, vibrationIntensity);
                        Debug.Log($"[SG_TouchButton] Sent haptic feedback at {vibrationIntensity} intensity to {vibrationLocation}");
                    }
                    else
                    {
                        Debug.LogWarning("[SG_TouchButton] Could not find SG_TrackedHand for haptic feedback");
                    }
                }

                onButtonPressed.Invoke();
                canPress = false;
            }
            else
            {
                Debug.Log("[SG_TouchButton] Button on cooldown");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Index") || 
            other.gameObject.name.Contains("Palm") ||
            other.gameObject.name.Contains("Thumb") ||
            other.gameObject.name.Contains("Middle"))
        {
            touchCount--;
            Debug.Log($"[SG_TouchButton] Touch count: {touchCount}");
        }
    }

    private SG_TrackedHand FindTrackedHandInHierarchy(Transform startTransform)
    {
        Transform current = startTransform;
        int depth = 0;
        while (current != null)
        {
            Debug.Log($"[SG_TouchButton] Searching for SG_TrackedHand at depth {depth}: {current.name}");
            
            SG_TrackedHand trackedHand = current.GetComponent<SG_TrackedHand>();
            if (trackedHand != null)
            {
                Debug.Log($"[SG_TouchButton] ✓ Found SG_TrackedHand on: {current.name}");
                
                IHandFeedbackDevice hapticDevice = trackedHand.HapticHardware;
                if (hapticDevice != null)
                {
                    Debug.Log($"[SG_TouchButton] ✓ HapticHardware available: {hapticDevice.GetType().Name}");
                }
                else
                {
                    Debug.LogWarning($"[SG_TouchButton] ✗ HapticHardware is NULL on {current.name}");
                }
                
                return trackedHand;
            }
            current = current.parent;
            depth++;
        }
        
        Debug.LogWarning($"[SG_TouchButton] ✗ Could not find SG_TrackedHand in hierarchy starting from {startTransform.name}");
        return null;
    }
}

