using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float startSpeed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Saber_Main" || collision.gameObject.tag == "Saber_Sub")
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.y < 0 && collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > startSpeed)
            {
                transform.parent.GetComponent<SongControlSystem>().playing = true;
                transform.parent.transform.GetChild(0).GetComponent<VideoPlay>().videoPlaying = true;
                Destroy(gameObject);
            }
        }
    }
}