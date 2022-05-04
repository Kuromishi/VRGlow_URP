using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetect : MonoBehaviour
{
    float timer = 0;
    public bool needHint;
    bool resulted = false;
    bool scored = true;

    float saberSpeed;

    public GameObject effect;
    GameObject rightArrow;
    GameObject leftArrow;
    bool rightAppeared = false;
    bool leftAppeared = false;

    public float goodLength;
    public float excellentLength;
    private void Start()
    {
        rightArrow = transform.GetChild(1).gameObject;
        leftArrow = transform.GetChild(0).gameObject;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (needHint == true)
        {
            if (timer >= 0f && !rightAppeared)
            {
                rightArrow.SetActive(true);
                rightAppeared = true;
            }
            if (timer >= 1.1108f && !leftAppeared)
            {
                leftArrow.SetActive(true);
                leftAppeared = true;
            }
        }
        
        if (timer >= 2.05f && !resulted)
        {
            if (saberSpeed != 0f)
            {
                Debug.Log("Wave Good!");
                SongControlSystem.score += 1;
                SongControlSystem.combo += 1;
                scored = true;
                Instantiate(effect, gameObject.transform.parent);
            }
            resulted = true;
        }
        if (timer >= 3f)
        {
            if (!scored)
            {
                SongControlSystem.combo = 0;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberSpeed = other.gameObject.transform.parent.GetComponentInParent<Rigidbody>().velocity.magnitude;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberSpeed = 0f;
        }
    }
}