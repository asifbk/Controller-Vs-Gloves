using UnityEngine;
using System.Reflection;

public class SG_ButtonHelper : MonoBehaviour
{
    private void Start()
    {
        SG_PhysicsButton button = GetComponent<SG_PhysicsButton>();
        if (button != null)
        {
            FieldInfo switchedField = typeof(SG_PhysicsButton).GetField("switched", BindingFlags.NonPublic | BindingFlags.Instance);
            if (switchedField != null)
            {
                switchedField.SetValue(button, true);
                Debug.Log("SG_ButtonHelper: Initialized button switched state to true");
            }
        }
    }
}
