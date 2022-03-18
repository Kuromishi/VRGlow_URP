using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_control : MonoBehaviour
{
    public GameObject prefab_broken;

    // Start is called before the first frame update
    void Start()
    {
        //this.create_broken_piece();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void create_broken_piece()
    {

        //this.prefab_broken.SetActive(true);
        //foreach (Transform child in this.prefab_broken.transform)
        //{
        //    child.gameObject.AddComponent<Rigidbody>();
        //}

        if (Move_control.instance.is_attacking == false)
            return;


        GameObject obj = Instantiate(this.prefab_broken);
        obj.transform.position = new Vector3(this.transform.position.x,0, this.transform.position.z); 
        //obj.transform.rotation = this.transform.rotation;
        obj.transform.localScale = this.transform.localScale;

        //obj.transform.LookAt(new Vector3(0, 0, 0));


        obj.transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, 0, GameObject.FindGameObjectWithTag("Player").transform.position.z));
        //obj.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        //obj.transform.rotation = new Quaternion(0,0,0,0); //Quaternion.Euler(0, 0, -90);




        Destroy(this.gameObject);

    }
}
