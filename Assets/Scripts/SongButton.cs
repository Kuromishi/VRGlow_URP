using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour
{
    MeshRenderer mr;

    float timer;

    public float goodTimeRange;
    public float excellentTimeRange;
    public float perfectTimeRange;

    public float goodSpeed;
    public float excellentSpeed;


    public enum State { good, excellent, perfect }
    public State timeState;
    public State speedState;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < goodTimeRange)
        {
            timeState = State.good;
            mr.material.color = Color.green;
        }

        else if (timer >= goodTimeRange && timer < goodTimeRange + excellentTimeRange)
        {
            timeState = State.excellent;
            mr.material.color = Color.yellow;
        }

        else if (timer >= goodTimeRange + excellentTimeRange && timer < goodTimeRange + excellentTimeRange + 2 * perfectTimeRange)
        {
            timeState = State.perfect;
            mr.material.color = Color.red;
        }

        else if (timer >= goodTimeRange + excellentTimeRange + 2 * perfectTimeRange && timer < goodTimeRange + 2 * excellentTimeRange + 2 * perfectTimeRange)
        {
            timeState = State.excellent;
            mr.material.color = Color.yellow;
        }

        else if (timer >= goodTimeRange + 2 * excellentTimeRange + 2 * perfectTimeRange && timer < 2 * goodTimeRange + 2 * excellentTimeRange + 2 * perfectTimeRange)
        {
            timeState = State.good;
            mr.material.color = Color.green;
        }

        else if (timer >= (goodTimeRange + excellentTimeRange + perfectTimeRange) * 2)
        {
            Debug.Log("Missed!");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
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
            Destroy(gameObject);
        }
    }
}