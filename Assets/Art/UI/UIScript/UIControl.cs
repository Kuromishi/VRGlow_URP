using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Sprite normalImage;
    public Sprite selectedImage;

    private void Start()
    {
        normalImage = GetComponent<Sprite>();
        selectedImage = GetComponent<Sprite>();

    }
    public static void OnImageClick()
    {
        
    }
}
