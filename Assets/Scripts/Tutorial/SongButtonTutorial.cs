using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButtonTutorial : SongButton
{
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (timer >= goodTimeSeconds * 2 + excellentTimeSeconds * 2 + perfectTimeSeconds)
        {
            Tutorialll.vp.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            Rigidbody rb = other.gameObject.GetComponentInParent<Rigidbody>();

            if (rb.velocity.y < 0)
            {
                if (rb.velocity.magnitude < goodSpeed)
                {
                    speedState = State.good;
                }

                else if (rb.velocity.magnitude >= goodSpeed && rb.velocity.magnitude < excellentSpeed)
                {
                    speedState = State.excellent;
                }

                else if (rb.velocity.magnitude >= excellentSpeed)
                {
                    speedState = State.excellent;
                }

                State finalState = (State)Mathf.Min((int)timeState, (int)speedState);

                switch (finalState)
                {
                    case State.good:
                        Debug.Log("Good!");
                        Tutorialll.PlayVideo();
                        break;
                    case State.excellent:
                        Debug.Log("Excellent!");
                        Tutorialll.PlayVideo();
                        break;
                    case State.perfect:
                        Debug.Log("Perfect!");
                        Tutorialll.PlayVideo();
                        break;
                }

                if (boomType == 0)
                {
                    Instantiate(boomEffectBlue, transform.position, boomEffectBlue.transform.rotation);
                    if (finalState == State.excellent)
                    {
                        Instantiate(boomEffectBlue_Perfect, transform.position, boomEffectBlue_Perfect.transform.rotation);
                    }
                }
                else
                {
                    Instantiate(boomEffectPink, transform.position, boomEffectPink.transform.rotation);
                    if (finalState == State.excellent)
                    {
                        Instantiate(boomEffectPink_Perfect, transform.position, boomEffectPink_Perfect.transform.rotation);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}