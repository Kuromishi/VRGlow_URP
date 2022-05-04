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

    protected float timer = 0;

    [Header("判定时间窗口，good + excellent + 一半的perfect时间 = 0.5s")]
    public float goodTimeSeconds;
    public float excellentTimeSeconds;
    public float perfectTimeSeconds;

    [Header("判定速度，这个检测y轴向下速度，因此建议弄小一点= =")]
    public float goodSpeed;
    public float excellentSpeed;

    protected enum State { good, excellent, perfect }
    protected State timeState;
    protected State speedState;
    protected virtual void FixedUpdate()
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
            SongControlSystem.combo = 0;
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
                        SongControlSystem.score += 1 * SongControlSystem.scoreRatio;
                        SongControlSystem.combo += 1;
                        break;
                    case State.excellent:
                        Debug.Log("Excellent!");
                        SongControlSystem.score += 3 * SongControlSystem.scoreRatio;
                        SongControlSystem.combo += 1;
                        break;
                    case State.perfect:
                        Debug.Log("Perfect!");
                        SongControlSystem.score += 5 * SongControlSystem.scoreRatio;
                        SongControlSystem.combo += 1;
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