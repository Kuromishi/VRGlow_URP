using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetect : MonoBehaviour
{
    protected float timer = 0;
    public bool needStartHint;
    public bool needHint;
    protected bool resulted = false;
    protected bool scored = true;

    protected float saberSpeed;

    public GameObject effect;
    protected GameObject rightArrow;
    protected GameObject leftArrow;
    protected bool rightAppeared = false;
    protected bool leftAppeared = false;
    protected void Start()
    {
        rightArrow = transform.GetChild(0).gameObject;
        leftArrow = transform.GetChild(1).gameObject;
        if (needStartHint)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (needHint == true)
        {
            if (timer >= 1.4f && !rightAppeared)
            {
                rightArrow.SetActive(true);
                rightAppeared = true;
            }
            if (timer >= 2.5108f && !leftAppeared)
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
                SongControlSystem.score += 1 * SongControlSystem.scoreRatio;
                SongControlSystem.combo += 1;
                scored = true;
                Instantiate(effect, gameObject.transform.parent);
            }
            resulted = true;
        }
        if (timer >= 4f)
        {
            if (!scored)
            {
                SongControlSystem.combo = 0;
            }
            Destroy(gameObject);
        }
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberSpeed = other.gameObject.transform.parent.GetComponentInParent<Rigidbody>().velocity.magnitude;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main"))
        {
            saberSpeed = 0f;
        }
    }
}