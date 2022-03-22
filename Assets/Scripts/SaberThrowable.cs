using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SaberThrowable : Throwable
{
    public SteamVR_Action_Boolean saberGrabAction;

    bool isGrabbing = false;

    int grabCount;

    Hand currentHand;

    protected override void OnAttachedToHand(Hand hand)
    {
        base.OnAttachedToHand(hand);

        currentHand = hand;
    }
    protected override void OnDetachedFromHand(Hand hand)
    {
        base.OnDetachedFromHand(hand);

        hand = null;
    }
    protected override void HandAttachedUpdate(Hand hand)
    {
        if (isGrabbing)
        {
            base.HandAttachedUpdate(hand);
            isGrabbing = false;
        }
    }
    public void Update()
    {
        if (interactable.attachedToHand)
        {
            if (saberGrabAction.GetStateUp(currentHand.handType))
            {
                if (grabCount >= 1)
                {
                    isGrabbing = true;
                    grabCount = 0;
                }
                else
                {
                    grabCount++;
                }
            }
        }
    }
}