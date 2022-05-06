using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    bool leftAttached = false;
    bool rightAttached = false;
    bool triggered = false;
    private void Update()
    {
        if (leftAttached && rightAttached && !triggered)
        {
            triggered = true;
            GetComponentInParent<SongControlSystem>().playing = true;
        }
    }
    public void LeftAttached()
    {
        leftAttached = true;
    }
    public void RightAttached()
    {
        rightAttached = true;
    }
}