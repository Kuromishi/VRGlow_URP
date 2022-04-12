using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButtonHintSakura : MonoBehaviour
{
    public GameObject songButton;
    float timer = 0;
    bool buttonSpawned = false;
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1.25 && !buttonSpawned)
        {
            Instantiate(songButton, transform.parent.transform);
            buttonSpawned = true;
        }
    }
}
