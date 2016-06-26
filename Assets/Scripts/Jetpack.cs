using UnityEngine;
using System.Collections;

public class Jetpack : MonoBehaviour {

    public PanelController Panel_access;

    public GameObject shockwave;


    public Rigidbody player_rigid;
    public Transform UP;
    public Transform CrossAir;
    public GameObject character_sprite;

    

    public float jetpackForce;

    public EllipsoidParticleEmitter PFX;

    public AudioSource jetpackSF;
    

    private bool jetpackSoundOn = false;
    internal bool isFloat = false;



    void Start()
    {
        PFX.enabled = false;
    }

    void Update()

    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 186.72f);


        JetPackSoundController();
    }



    //Jetpack control;
    void FixedUpdate()
    {

        Vector3 direction = UP.transform.position - this.transform.position;
        if (!Panel_access.panelIsOpen)
        {
            if (Input.GetMouseButton(0))
            {
               
                PFX.enabled = true;

                isFloat = true;

                //Calculating The mouse position using vectors.
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
                //mousePosition.z = 186.72f;

                //Rotates toward the mouse
                player_rigid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);

                //Judge the distance from the object and the mouse
                float distanceFromObject = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).magnitude;

                //Turns on the trajectory , gives the character who using it, a boost up , in pivot direction.
                player_rigid.velocity = (CrossAir.up * jetpackForce * Time.deltaTime);
            }

            else if(Input.GetMouseButtonUp(0))
            {
                isFloat = false;
                jetpackSoundOn = false;
                jetpackSF.Stop();
            }

            else
            {
                isFloat = false;
                PFX.enabled = false;
            }

     



        }

        //if (Input.GetKeyDown("space"))
        //{
        //    GameObject sw = Instantiate(shockwave, transform.position, transform.rotation) as GameObject;
        //    sw.GetComponent<soundWaveScript>().setInitialScale(25f);
        //    shockWave.Play();

        //}
    }

    private void JetPackSoundController()
    {
        if(isFloat)
            if(!jetpackSoundOn)
            {
                jetpackSF.Play();
                jetpackSoundOn = true;
            }
    }

    
}
