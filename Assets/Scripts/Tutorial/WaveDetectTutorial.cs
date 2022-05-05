using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDetectTutorial : WaveDetect
{
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (needStartHint == true)
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
                GetComponentInParent<Tutorialll>().perfectCount++;
                scored = true;
                Instantiate(effect, gameObject.transform.parent);
            }
            resulted = true;
        }
        if (timer >= 4f)
        {
            if (!scored)
            {
                GetComponentInParent<Tutorialll>().missCount++;
            }
            Destroy(gameObject);
        }
    }
}