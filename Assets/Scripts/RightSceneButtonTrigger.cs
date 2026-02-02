using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RightSceneButtonTrigger : MonoBehaviour
{
    [Header("Scene Transition")]
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    [Header("Loop Management")]
    public GameObject rightLoop;
    public Transform rightLoopTargetParent;

    [Header("Hand Models")]
    public GameObject leftHandModel;
    public GameObject rightHandModel;

    public void OnButtonPressed()
    {
        Debug.Log("RightSceneButtonTrigger.OnButtonPressed() called!");

        if (objectToDeactivate != null)
        {
            Debug.Log("Deactivating: " + objectToDeactivate.name);
            objectToDeactivate.SetActive(false);
        }

        if (objectToActivate != null)
        {
            Debug.Log("Activating: " + objectToActivate.name);
            objectToActivate.SetActive(true);
        }

        if (rightLoop != null)
        {
            Debug.Log("Moving and activating right loop: " + rightLoop.name);
            rightLoop.SetActive(true);
            
            if (rightLoopTargetParent != null)
            {
                rightLoop.transform.SetParent(rightLoopTargetParent, true);
                Debug.Log($"Moved RightLoop to: {rightLoopTargetParent.name}");
            }
            
            Rigidbody loopRb = rightLoop.GetComponent<Rigidbody>();
            if (loopRb != null)
            {
                loopRb.useGravity = false;
                loopRb.isKinematic = false;
                loopRb.constraints = RigidbodyConstraints.None;
                loopRb.drag = 5f;
                loopRb.angularDrag = 5f;
                loopRb.velocity = Vector3.zero;
                loopRb.angularVelocity = Vector3.zero;
                Debug.Log("Reset RightLoop Rigidbody: gravity off, constraints removed, drag applied, velocity zeroed");
            }
        }

        if (leftHandModel != null)
        {
            Debug.Log("Hiding left hand model: " + leftHandModel.name);
            leftHandModel.SetActive(false);
        }

        if (rightHandModel != null)
        {
            Debug.Log("Hiding right hand model: " + rightHandModel.name);
            rightHandModel.SetActive(false);
        }
    }
}
