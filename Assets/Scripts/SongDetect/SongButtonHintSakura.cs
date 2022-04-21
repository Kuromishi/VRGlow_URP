using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButtonHintSakura : MonoBehaviour
{
    public int hintType;
    //0 = blue; 1 = pink
    public GameObject songButton;
    float timer = 0;
    bool buttonSpawned = false;
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= 1.00 && !buttonSpawned)
        {
            GameObject button = Instantiate(songButton, transform.parent.transform);
            button.transform.position = transform.position;
            if (hintType == 0)
            {
                button.GetComponent<SongButton>().boomType = 0;
            }
            else
            {
                button.GetComponent<SongButton>().boomType = 1;
            }
            buttonSpawned = true;
        }
    }
}