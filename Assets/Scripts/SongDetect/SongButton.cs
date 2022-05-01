using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour
{
    public int boomType;
    //0 = blue; 1 = pink
    public GameObject boomEffectBlue;
    public GameObject boomEffectBlue_Perfect;
    public GameObject boomEffectPink;
    public GameObject boomEffectPink_Perfect;

    float timer = 0;

    public float goodTimeSeconds;
    public float excellentTimeSeconds;
    public float perfectTimeSeconds;

    public float goodSpeed;
    public float excellentSpeed;

    public enum State { good, excellent, perfect }
    public State timeState;
    public State speedState;
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
                        SongControlSystem.score += 1;
                        break;
                    case State.excellent:
                        Debug.Log("Excellent!");
                        SongControlSystem.score += 3;
                        break;
                    case State.perfect:
                        Debug.Log("Perfect!");
                        SongControlSystem.score += 5;
                        break;
                }

                if (boomType == 0)
                {
                    Instantiate(boomEffectBlue, transform.position, transform.rotation);
                    if (finalState == State.excellent)
                    {
                        Instantiate(boomEffectBlue_Perfect, transform.position, transform.rotation);
                    }
                }
                else
                {
                    Instantiate(boomEffectPink, transform.position, transform.rotation);
                    if (finalState == State.excellent)
                    {
                        Instantiate(boomEffectPink_Perfect, transform.position, transform.rotation);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}