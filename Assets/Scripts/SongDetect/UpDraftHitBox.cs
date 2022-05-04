using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDraftHitBox : MonoBehaviour
{
    float timer = 0;
    float scoreTimer = 0;
    bool colliderOpened = false;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f && !colliderOpened)
        {
            GetComponent<SphereCollider>().enabled = true;
            colliderOpened = true;
        }
        if (timer >= 3f)
        {
            if (scoreTimer >= 0.75)
            {
                SongControlSystem.score += 1 * SongControlSystem.scoreRatio;
                SongControlSystem.combo += 1;
            }
            else
            {
                SongControlSystem.combo = 0;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            scoreTimer += Time.deltaTime;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Saber_Main") || other.gameObject.CompareTag("Saber_Sub"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}