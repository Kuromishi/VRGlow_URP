using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetect : MonoBehaviour
{
    Vector3 saberPosition;
    Vector3 startPosition;
    Vector3 endPosition;

    Vector3 moveVector;

    public float goodLength;
    public float excellentLength;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Saber_Main")
        {
            saberPosition = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Saber_Main" || other.tag == "Saber_Sub")
        {
            saberPosition = new Vector3 (0, 0, 0);
        }
    }
    void StartDetect_Left()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        startPosition = saberPosition;
    }
    void EndDetect_Left()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        endPosition = saberPosition;
        moveVector = endPosition - startPosition;
        if (moveVector.z < 0)
        {
            if (Mathf.Abs(moveVector.z) < goodLength)
            {
                Debug.Log("Good!");
            }
            else if(Mathf.Abs(moveVector.z) < excellentLength)
            {
                Debug.Log("Excellent!");
            }
            else
            {
                Debug.Log("Perfect!");
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
    void StartDetect_Right()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        startPosition = saberPosition;
    }
    void EndDetect_Right()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        endPosition = saberPosition;
        moveVector = endPosition - startPosition;
        if (moveVector.z > 0)
        {
            if (Mathf.Abs(moveVector.z) < goodLength)
            {
                Debug.Log("Good!");
            }
            else if (Mathf.Abs(moveVector.z) < excellentLength)
            {
                Debug.Log("Excellent!");
            }
            else
            {
                Debug.Log("Perfect!");
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
    void EndWave()
    {
        Destroy(gameObject);
    }
}