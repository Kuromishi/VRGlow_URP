using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetect : MonoBehaviour
{
    float timer = 0;
    bool detected = false;
    public bool needHint;

    float saberSpeed;

    Animator anim;

    public GameObject effect;

    public float goodLength;
    public float excellentLength;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (needHint == true)
        {
            anim.Play("WaveIntro");
        }
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 2.05f && !detected)
        {
            detected = true;
            if (saberSpeed != 0f)
            {
                Debug.Log("Wave Good!");
                Debug.Log(saberSpeed);
                Instantiate(effect, gameObject.transform);
            }
            else
            {
                Debug.Log("Wave Bad!");
            }
        }
        else if (timer >= 2.5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberSpeed = other.gameObject.transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
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