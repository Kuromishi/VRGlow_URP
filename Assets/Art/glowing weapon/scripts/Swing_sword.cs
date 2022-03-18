using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing_sword : MonoBehaviour
{
    public GameObject[] game_obj_sowrds;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float num;
    bool is_left=false;

    // Update is called once per frame
    void Update()
    {
        //Quaternion.Angle(this.transform.rotation, Quaternion.LookRotation(this.moveDirection))


        //this.game_object.rectTransform.rotation = Quaternion.Euler(0, 0, -90);


        if (is_left == false)
        {
            //num = Mathf.Lerp(num, 70, Time.deltaTime * this.speed);

            num += Time.deltaTime * this.speed;

            if (num > 69.5f)
            {
                is_left = true;
            }
        }
        else
        {
            //num = Mathf.Lerp(num, -70, Time.deltaTime * this.speed);

            num -= Time.deltaTime * this.speed;

            if (num <- 69.5f)
            {
                is_left = false;
            }
        }
      



        for (int i = 0; i < this.game_obj_sowrds.Length; i++)
        {
            this.game_obj_sowrds[i].transform.rotation= Quaternion.Euler(num, 0, -80);
        }
    }
}
