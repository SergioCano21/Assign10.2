using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OurHand : MonoBehaviour
{
    public GameObject ourHandPrefab;
    public InputDeviceCharacteristics ourControllerCharacteristics;
    private InputDevice ourDevice;
    private Animator ourHandAnimator;
    void Start()
    {
        InitializeOurHand();
    }

    void InitializeOurHand()
    {
        //check for controllers characteristics
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(ourControllerCharacteristics, devices);

        //if device identified, then instantiate hand
        if (devices.Count > 0)
        {
            ourDevice = devices[0];
            GameObject newHand = Instantiate(ourHandPrefab, transform);
            ourHandAnimator = newHand.GetComponent<Animator>();
        }
    }
    void Update()
    {
        if (ourDevice.isValid)
        {
            UpdateOurHand();
        }
        else
        {
            InitializeOurHand();
        }
    }
    void UpdateOurHand()
    {
        if(ourDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            ourHandAnimator.SetFloat("Trigger", triggerValue);
            Debug.Log("Trigger: " + triggerValue);
        }
        else
        {
            ourHandAnimator.SetFloat("Trigger", 0);
            Debug.Log("Trigger: " + triggerValue);
        }
        if (ourDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            ourHandAnimator.SetFloat("Grip", gripValue);
            Debug.Log("Grip: " + gripValue);
        }
        else
        {
            ourHandAnimator.SetFloat("Grip", 0);
            Debug.Log("Grip: " + gripValue);
        }
    }
}
