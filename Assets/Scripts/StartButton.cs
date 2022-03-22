using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public float startSpeed;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Rigidbody>().velocity.y < 0 && collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > startSpeed)
        {
            transform.parent.transform.parent.GetComponent<Animator>().Play("SongTest");
            transform.parent.gameObject.SetActive(false);
        }
    }
}