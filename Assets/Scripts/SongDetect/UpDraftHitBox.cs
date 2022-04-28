using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDraftHitBox : MonoBehaviour
{
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            if(other.gameObject.GetComponentInParent<Rigidbody>().velocity.y >= 0)
            {
                GetComponentInParent<UpDraft>().score++;
                Destroy(gameObject);
            }
        }
    }
}