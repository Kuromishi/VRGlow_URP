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

    private void Start()
    {
        hitBox_Start = transform.GetChild(2).transform.GetChild(0).gameObject;
        hitBox_Sub_1 = transform.GetChild(3).transform.GetChild(0).gameObject;
        hitBox_Sub_2 = transform.GetChild(4).transform.GetChild(0).gameObject;
        hitBox_End = transform.GetChild(5).transform.GetChild(0).gameObject;
    }
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

        if(timer >= 1.25f && !hitBoxStartActivated)
        {
            hitBox_Start.SetActive(true);
            hitBoxStartActivated = true;
        }
        else if (timer >= 1.8f && !hitBoxOneActivated)
        {
            hitBox_Sub_1.SetActive(true);
            hitBoxOneActivated = true;
        }
        else if (timer >= 2.35f && !hitBoxTwoActivated)
        {
            hitBox_Sub_2.SetActive(true);
            hitBoxTwoActivated = true;
        }
        else if (timer >= 2.9f && !hitBoxEndActivated)
        {
            hitBox_End.SetActive(true);
            hitBoxEndActivated = true;
        }
        else if (timer >= 4f)
        {
            SongControlSystem.score += score;
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
