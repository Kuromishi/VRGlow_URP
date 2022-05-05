using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButtonTutorial : SongButton
{
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer < goodTimeSeconds)
        {
            timeState = State.good;
        }

        else if (timer >= goodTimeSeconds && timer < goodTimeSeconds + excellentTimeSeconds)
        {
            timeState = State.excellent;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds && timer < goodTimeSeconds + excellentTimeSeconds + perfectTimeSeconds)
        {
            timeState = State.perfect;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds + perfectTimeSeconds && timer < goodTimeSeconds + excellentTimeSeconds * 2 + perfectTimeSeconds)
        {
            timeState = State.excellent;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds * 2 + perfectTimeSeconds && timer < goodTimeSeconds * 2 + excellentTimeSeconds * 2 + perfectTimeSeconds)
        {
            timeState = State.good;
        }

        else if (timer >= goodTimeSeconds * 2 + excellentTimeSeconds * 2 + perfectTimeSeconds)
        {
            Debug.Log("Missed!");
            GetComponentInParent<Tutorialll>().perfectCount++;
            Destroy(gameObject);
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
                        GetComponentInParent<Tutorialll>().perfectCount++;
                        break;
                    case State.excellent:
                        Debug.Log("Excellent!");
                        GetComponentInParent<Tutorialll>().perfectCount++;
                        break;
                    case State.perfect:
                        Debug.Log("Perfect!");
                        GetComponentInParent<Tutorialll>().perfectCount++;
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