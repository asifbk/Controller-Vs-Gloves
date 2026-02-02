using UnityEngine;

public class ButtonDebugger : MonoBehaviour
{
    public Transform buttonTop;
    private Vector3 startPos;
    private bool hasLogged = false;

    private void Start()
    {
        if (buttonTop != null)
        {
            startPos = buttonTop.localPosition;
            Debug.Log($"[ButtonDebugger] Button top start position: {startPos}");
            
            Rigidbody rb = buttonTop.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Debug.Log($"[ButtonDebugger] Button top Rigidbody - Mass: {rb.mass}, IsKinematic: {rb.isKinematic}, UseGravity: {rb.useGravity}, Constraints: {rb.constraints}");
            }
            else
            {
                Debug.LogError("[ButtonDebugger] Button top has NO Rigidbody!");
            }

            Collider col = buttonTop.GetComponent<Collider>();
            if (col != null)
            {
                Debug.Log($"[ButtonDebugger] Button top Collider type: {col.GetType().Name}, IsTrigger: {col.isTrigger}");
            }
            else
            {
                Debug.LogError("[ButtonDebugger] Button top has NO Collider!");
            }
        }
        else
        {
            Debug.LogError("[ButtonDebugger] Button top is NULL!");
        }

        SG_PhysicsButton physButton = GetComponent<SG_PhysicsButton>();
        if (physButton != null)
        {
            Debug.Log("[ButtonDebugger] SG_PhysicsButton component found");
        }
        else
        {
            Debug.LogError("[ButtonDebugger] NO SG_PhysicsButton component!");
        }
    }

    private void Update()
    {
        if (buttonTop != null)
        {
            float yDelta = buttonTop.localPosition.y - startPos.y;
            
            if (Mathf.Abs(yDelta) > 0.001f && !hasLogged)
            {
                Debug.Log($"[ButtonDebugger] Button top moved! Y delta: {yDelta}");
                hasLogged = true;
            }

            if (yDelta < -0.01f)
            {
                Debug.Log($"[ButtonDebugger] Button pressed! Y delta: {yDelta}");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"[ButtonDebugger] Collision ENTER with: {collision.gameObject.name}");
    }
}
