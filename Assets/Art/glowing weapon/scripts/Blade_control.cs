using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade_control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //碰撞检测 Impact checking
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "skeleton")
        {
            //Debug.Log("collider>>>" + collider.gameObject.name);

            collider.gameObject.GetComponent<Skeleton_control>().create_broken_piece();

            //if (GameObject.FindGameObjectWithTag("skeleton") != null)
            //    GameObject.FindGameObjectWithTag("skeleton").GetComponent<Skeleton_control>().create_broken_piece();
        }
    }
}
