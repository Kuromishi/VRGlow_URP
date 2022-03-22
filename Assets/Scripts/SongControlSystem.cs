using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongControlSystem : MonoBehaviour
{
    public GameObject Button;
    void ButtonAppear()
    {
        Instantiate(Button, gameObject.transform);
    }
}