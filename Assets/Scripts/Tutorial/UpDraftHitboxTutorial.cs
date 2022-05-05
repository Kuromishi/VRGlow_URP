using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDraftHitboxTutorial : UpDraftHitBox
{
    void Update()
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
                GetComponentInParent<Tutorialll>().perfectCount++;
            }
            else
            {
                GetComponentInParent<Tutorialll>().missCount++;
            }
            Destroy(gameObject);
        }
    }
}
