using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_sword_control : MonoBehaviour
{
    [Header("AudioSource, grow audioclip，retract audioclip, wave audioclip")]
    public AudioSource audio_source;
    public AudioClip audio_clip_grow;
    public AudioClip audio_clip_retract;
    public AudioClip audio_clip_wave;

    [Header("gameobject blade")]
    public GameObject game_obj_blade;
    public GameObject game_obj_blade_2;

    [Header("gameobject slash")]
    public GameObject game_obj_slash;

    [Header("grow speed. retract speed")]
    public float speed_grow;
    public float speed_retract;

    [Header("sword's Color")]
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color color_sword;

    private bool is_growing = false;
    private bool is_retarcting = false;

    // Start is called before the first frame update
    void Start()
    {
        //set color
        this.game_obj_blade.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",this.color_sword);
        this.game_obj_blade_2.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", this.color_sword);

        float factor = Mathf.Pow(2, -0.85f);
        Color color = new Color(color_sword.r * factor, color_sword.g * factor, color_sword.b * factor);


        //Material mat = new Material(GetComponent<ParticleSystemRenderer>().trailMaterial.shader);
        //mat = game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial;
        //mat.SetColor("_EmissionColor", color);
        //this.game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial = mat;

        //this.game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial.SetColor("_EmissionColor", color);
      
        this.game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial = Instantiate(this.game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial);
        this.game_obj_slash.GetComponent<ParticleSystemRenderer>().trailMaterial.SetColor("_EmissionColor", this.color_sword);

    }

    // Update is called once per frame
    void Update()
    {
        if (this.is_growing)
        {
            float scale_z = this.game_obj_blade.transform.localScale.z;
            float scale_z_2 = this.game_obj_blade_2.transform.localScale.z;

            scale_z = Mathf.Lerp(scale_z, 1f, Time.deltaTime * this.speed_grow);
            scale_z_2 = Mathf.Lerp(scale_z_2, 1f, Time.deltaTime * this.speed_grow);

            this.game_obj_blade.transform.localScale = new Vector3(1, 1, scale_z);
            this.game_obj_blade_2.transform.localScale = new Vector3(1, 1, scale_z_2);

            if (scale_z > 0.99f && scale_z_2 > 0.99f)
            {
                this.is_growing = false;

                this.game_obj_slash.SetActive(true);
            }
        }
        else if (this.is_retarcting)
        {

            float scale_z = this.game_obj_blade.transform.localScale.z;
            float scale_z_2 = this.game_obj_blade_2.transform.localScale.z;

            scale_z = Mathf.Lerp(scale_z, -0.1f, Time.deltaTime * this.speed_grow);
            scale_z_2 = Mathf.Lerp(scale_z_2, -0.1f, Time.deltaTime * this.speed_grow);

            this.game_obj_blade.transform.localScale = new Vector3(1, 1, scale_z);
            this.game_obj_blade_2.transform.localScale = new Vector3(1, 1, scale_z_2);

            if (scale_z < -0.01f && scale_z_2 < -0.01f)
            {
                this.is_retarcting = false;
            }
        }
    }


    public void light_grow()
    {
        if (this.is_growing || this.is_retarcting)
            return;

        if (this.game_obj_blade.transform.localScale.z > 0.99f || this.game_obj_blade_2.transform.localScale.z > 0.99f)
            return;

        this.game_obj_slash.SetActive(false);

        this.audio_source.clip = this.audio_clip_grow;
        this.audio_source.Play();

        this.is_growing = true;
    }

    public void light_retract()
    {
        if (this.is_growing || this.is_retarcting)
            return;

        if (this.game_obj_blade.transform.localScale.z < -0.01f || this.game_obj_blade_2.transform.localScale.z < -0.01f)
            return;

        this.game_obj_slash.SetActive(false);

        this.audio_source.clip = this.audio_clip_retract;
        this.audio_source.Play();

        this.is_retarcting = true;

    }

    public void play_wave_audio()
    {
        this.audio_source.clip = this.audio_clip_wave;
        this.audio_source.Play();
    }
}
