using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;
using SGCore.Haptics;

public class collisionChangeRing : MonoBehaviour
{
    [Header("Wrist Vibration")]
    public SG_TrackedHand leftHand;
    public SG_TrackedHand rightHand;
    [Range(0f, 1f)]
    public float vibrationIntensity = 1.0f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wire")
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "wire")
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
            TriggerWristVibration();
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "wire")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void TriggerWristVibration()
    {
        if (leftHand != null)
        {
            leftHand.SendImpactVibration(SG_HandSection.Wrist, vibrationIntensity);
        }

        if (rightHand != null)
        {
            rightHand.SendImpactVibration(SG_HandSection.Wrist, vibrationIntensity);
        }
    }

    // void OnCollisionEnter(Collision other) 
    // {
    //     GetComponent<MeshRenderer>().material.color = Color.black;
    // }
}
