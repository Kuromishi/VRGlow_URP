using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float startSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Saber_Main" || other.gameObject.tag == "Saber_Sub")
        {
            if (other.gameObject.GetComponentInParent<Rigidbody>().velocity.y < 0 && other.gameObject.GetComponentInParent<Rigidbody>().velocity.magnitude > startSpeed)
            {
                transform.parent.GetComponent<SongControlSystem>().playing = true;
                transform.parent.transform.GetChild(0).GetComponent<VideoPlay>().videoPlaying = true;
                Destroy(gameObject);
            }
        }
    }
}