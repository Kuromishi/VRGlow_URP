using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRolling : MonoBehaviour
{
    ScrollRect rect;
    void Start()
    {
        rect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        rect.horizontalNormalizedPosition += 0.08f * Time.deltaTime;
    }
}
