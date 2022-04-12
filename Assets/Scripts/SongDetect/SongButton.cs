using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour
{
    public GameObject boomEffect;

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
            timeState = State.perfect;
        }

        else if (timer >= goodTimeSeconds && timer < goodTimeSeconds + excellentTimeSeconds)
        {
            timeState = State.excellent;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds && timer < goodTimeSeconds + excellentTimeSeconds + perfectTimeSeconds * 2)
        {
            timeState = State.good;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds + perfectTimeSeconds * 2 && timer < goodTimeSeconds + excellentTimeSeconds * 2 + perfectTimeSeconds * 2)
        {
            timeState = State.good;
        }

        else if (timer >= goodTimeSeconds + excellentTimeSeconds * 2 + perfectTimeSeconds * 2 && timer < goodTimeSeconds * 2 + excellentTimeSeconds * 2 + perfectTimeSeconds * 2)
        {
            timeState = State.good;
        }

        else if (timer >= goodTimeSeconds * 2 + excellentTimeSeconds * 2 + perfectTimeSeconds * 2)
        {
            Debug.Log("Missed!");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Saber_Main" || collision.gameObject.tag == "Saber_Sub")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

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
            Instantiate(boomEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}