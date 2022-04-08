using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButtonHintSakura : MonoBehaviour
{
    public GameObject songButton;
    float timer = 0;
    private void FixedUpdate()
    {
        timer += 1;
        if (timer == 40)
        {
            Instantiate(songButton, transform.parent.transform);
        }
    }
}
