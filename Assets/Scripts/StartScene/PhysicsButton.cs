using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField]
    private float threshold = 0.1f;
    [SerializeField]
    private float deadZone = 0.025f;

    public UnityEvent onPressed;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    void Start()
    {
        startPos = transform.position;
        joint = GetComponent<ConfigurableJoint>();
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.position) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }
}
