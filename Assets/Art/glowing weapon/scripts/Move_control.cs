using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_control : MonoBehaviour
{
    public CharacterController character_controller;

    public Animator animator;

    public Rigidbody rigid_body;

    public Light_sword_control light_sword_control;

    public static Move_control instance;


    public float speed = 8.0F;

    private Vector3 moveDirection = Vector3.zero;


    public bool is_attacking = false;

    void Awake()
    {
        Move_control.instance = this;
    }



    void Update()
    {
        //show or hide weapon
        #region 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.light_sword_control.light_grow();
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            this.light_sword_control.light_retract();
        }
        #endregion

        //attack
        #region 
        if (Input.GetMouseButtonDown(0))
        {
            this.animator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.animator.Play("Great Sword Slash (2)");
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            this.animator.Play("Great Sword Slash 0");
            return;
        }

        if (this.animator.GetCurrentAnimatorClipInfo(0).Length == 0)
            return;

        if (this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Great Sword Slash" ||
            this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Great Sword Slash (2)" ||
            this.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Great Sword Slash 0")
        {
            this.is_attacking = true;
            return;
        }
        else
        {
            this.is_attacking = false;
        }
        #endregion

        //move jump
        #region 
        float offset_vertical = Input.GetAxis("Vertical");
        float offset_horizontal = Input.GetAxis("Horizontal");

        if (offset_vertical == 0 && offset_horizontal == 0)
        {
            this.animator.SetBool("is_run", false);
            this.animator.SetBool("is_idle", true);
        }
        else
        {
            this.animator.SetBool("is_run", true);
            this.animator.SetBool("is_idle", false);

            this.moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));

            this.moveDirection *= this.speed;

            if (Quaternion.Angle(this.transform.rotation, Quaternion.LookRotation(this.moveDirection)) < 5f)
            {
                if (this.transform.rotation != Quaternion.LookRotation(this.moveDirection))
                {
                    this.transform.rotation = Quaternion.LookRotation(this.moveDirection);
                }
                //this.transform.position += this.moveDirection;

                this.character_controller.Move(this.moveDirection);
            }
            else
            {
                this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(this.moveDirection), Time.deltaTime * 50);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.animator.SetBool("is_jump", true);
            //this.rigid_body.AddForce(Vector3.up * 1500);

            this.AddImpact(Vector3.up, 6);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.animator.SetBool("is_jump", false);
        }
        #endregion


        if (impact.magnitude > 0.2F)
            this.character_controller.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

    }

    float mass = 1F; // defines the character mass
    Vector3 impact = Vector3.zero;
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    public void on_attack_event()
    {
        this.light_sword_control.play_wave_audio();
    }
}
