using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour
{
    MeshRenderer mr;

    public GameObject boomEffect;

    float timer = 0;

    public float goodTimeFrame;
    public float excellentTimeFrame;
    public float perfectTimeFrame;

    public float goodSpeed;
    public float excellentSpeed;

    public enum State { good, excellent, perfect }
    public State timeState;
    public State speedState;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    void FixedUpdate()
    {
        timer += 1;

        if (timer < perfectTimeFrame)
        {
            timeState = State.perfect;
            mr.material.color = Color.green;
        }

        else if (timer >= perfectTimeFrame && timer < excellentTimeFrame + perfectTimeFrame)
        {
            timeState = State.excellent;
            mr.material.color = Color.yellow;
        }

        else if (timer >= excellentTimeFrame + perfectTimeFrame && timer < goodTimeFrame + excellentTimeFrame + perfectTimeFrame)
        {
            timeState = State.good;
            mr.material.color = Color.red;
        }

        else if (timer >= goodTimeFrame + excellentTimeFrame + perfectTimeFrame)
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
                    break;
                case State.excellent:
                    Debug.Log("Excellent!");
                    break;
                case State.perfect:
                    Debug.Log("Perfect!");
                    break;
            }
            Instantiate(boomEffect, transform.parent.transform);
            Destroy(gameObject);
        }
    }
}