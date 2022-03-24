using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetect : MonoBehaviour
{
    float timer;

    MeshRenderer mr;

    Vector3 saberPosition;
    Vector3 startPosition;
    Vector3 endPosition;

    Vector3 moveVector;

    public float goodLength;
    public float excellentLength;

    private void Start()
    {
        timer = 0;
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberPosition = other.transform.position;
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