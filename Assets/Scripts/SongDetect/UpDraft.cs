using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDraft : MonoBehaviour
{
    public int score = 0;

    float timer = 0;

    Vector3 draftPosition;
    bool isDrafting = false;

    GameObject hitBox_Start;
    GameObject hitBox_Sub_1;
    GameObject hitBox_Sub_2;
    GameObject hitBox_End;

    bool hitBoxStartActivated = false;
    bool hitBoxOneActivated = false;
    bool hitBoxTwoActivated = false;
    bool hitBoxEndActivated = false;
    bool scored = false;

    private void Update()
    {
        if (isDrafting)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.position = draftPosition;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        timer += Time.deltaTime;

        if(timer >= 1.0f && !hitBoxStartActivated)
        {
            hitBox_Start.SetActive(true);
            hitBoxStartActivated = true;
        }
        else if (timer >= 1.55f && !hitBoxOneActivated)
        {
            hitBox_Sub_1.SetActive(true);
            hitBoxOneActivated = true;
        }
        else if (timer >= 2.1f && !hitBoxTwoActivated)
        {
            hitBox_Sub_2.SetActive(true);
            hitBoxTwoActivated = true;
        }
        else if (timer >= 2.65f && !hitBoxEndActivated)
        {
            hitBox_End.SetActive(true);
            hitBoxEndActivated = true;
        }
        else if (timer >= 4f && !scored)
        {
            scored = true;
            SongControlSystem.score += score;
            if (score == 0)
            {
                SongControlSystem.combo = 0;
            }
            SongControlSystem.combo += 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            if(other.gameObject.GetComponentInParent<Rigidbody>().velocity.y >= 0)
            {
                isDrafting = true;
                draftPosition = other.gameObject.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            isDrafting = false;
        }
    }
}
