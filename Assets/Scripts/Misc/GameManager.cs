using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameController GameController;

    Jetpack jp;
    Impactor im;
    PickUp pu;
    soundWaveScript sw;
    Renderer rn;
    Rigidbody rb;
    Collider cl;

    //Patricle FX setting;

    public GameObject grindFX;

    bool isGrinding;

    public GameObject deathFX;


    internal int sparkCollected;
    List<GameObject> sparks = new List<GameObject>();



    //players life
    public int Life = 1;
    int currLife;
    public Text lifeText;
    public bool invulnerable = false;

    public bool gameOver = false;
    public AudioSource deathSF;
    private bool deathSF_Playing = false;

    public AudioSource enemyDeath;

    bool isDead = false;
    bool isHit = false;

    // Spark collection
    ArrayList collectedSparks = new ArrayList();
    public float rotationSpeed = 1.0f;
    public float sparkDistance = 1.5f;

    private float currentTime;
    private bool playerHit = false;

    void Start()
    {

        deathFX.SetActive(false);

        grindFX.SetActive(false);

        currLife = Life;

        lifeText.text = "Life: " + currLife + "/" + Life;

        jp = GetComponent<Jetpack>();
        im = GetComponent<Impactor>();
        pu = GetComponent<PickUp>();
        sw = GetComponent<soundWaveScript>();
        rn = GetComponentInChildren<Renderer>();
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
    }

    //Lists of objects that are true on collision.
    public void OnTriggerEnter(Collider collision)
    {




   
    }

    private void RestartAtSpawnPoint()
    {
        rb.velocity = Vector3.zero;
        this.transform.position = GameObject.Find("Player respawn").transform.position;
        jp.enabled = true;
        im.enabled = true;
        pu.enabled = true;
        sw.enabled = true;
        rn.enabled = true;
        cl.enabled = true;
        //play some sound or something
        //change color or something
        //emit particles or something

        Invoke("MakeVurnuable", 3f);
    }

    private void MakeVurnuable()
    {
        

        
        invulnerable = false;
        //play some sound or something
        //change color back or something
        //stop emitting particles or something
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "enemy" && !invulnerable && currLife > 0)
        {
            playerHit = true;
            currentTime = Time.time;
            currLife--;
            lifeText.text = "Life: " + currLife + "/" + Life;
            collision.collider.GetComponent<Enemy_AI>().Stun();

            //lazy coding respawn
            jp.enabled = false;
            im.enabled = false;
            pu.enabled = false;
            sw.enabled = false;
            rn.enabled = false;
            cl.enabled = false;
            invulnerable = true;

            im.FalsePulse();

            if (currLife <= 0)
            {
                GameController.StopAllCoroutines();
                gameOver = true;

                if(!deathSF_Playing)
                {
                    deathSF.Play();
                    deathSF_Playing = true;
                }
                
            }
            else
                Invoke("RestartAtSpawnPoint", 1);
        }


        if (collision.collider.tag == "wall")
        {
            isGrinding = true;
        }
        
           


     
    }




    void Update()
    {

        //explosion FX
        if (playerHit)
            PlayerHit();

        //Grinding FX
        WallGrinding();


        Ray rayUp = new Ray(transform.position, (Vector3.up * 1));
        Ray rayDown = new Ray(transform.position, (Vector3.down * 1));
        Ray rayLeft = new Ray(transform.position, (Vector3.left * 1));
        Ray rayRight = new Ray(transform.position, (Vector3.right * 1));

        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + (Vector3.up * 1), Color.cyan);
        if (Physics.Raycast(rayUp, out hit, 1) ||
            Physics.Raycast(rayDown, out hit, 1) ||
            Physics.Raycast(rayLeft, out hit, 1) ||
            Physics.Raycast(rayRight, out hit, 1))
        {
            if (hit.collider.tag == "wall")

            {
 //               print("hit");
                isGrinding = true;
            }
        }

        else
            isGrinding = false;




            //print("sparks collected : " + sparkCollected + "SparksREQ : " + sparkReq);



            for (int i = 0; i < collectedSparks.Count; ++i)
        {
            Vector3 targetPosition = new Vector3();

            float angleOffset = Time.timeSinceLevelLoad * rotationSpeed + (2.0f * Mathf.PI * i) / (float)collectedSparks.Count;

            //angleOffset *= ;

            targetPosition.x = Mathf.Cos(angleOffset) * sparkDistance;
            targetPosition.y = Mathf.Sin(angleOffset) * sparkDistance;
            targetPosition += transform.position;

            GameObject spark = (GameObject)collectedSparks[i];
            sparks.Add((GameObject)collectedSparks[i]);



            if (spark != null)
                spark.transform.position = targetPosition;
        }


    }

    public void PlayerHit()
    {
        //print("xxxxxxxxxxxxxxxxxxx");
        isHit = true;
        deathFX.SetActive(true);
            
        if (Time.time - currentTime >= 0.5f)
        {
            deathFX.SetActive(false);
            playerHit = false;
        }

    }

    public void EnemyDied()
    {
        enemyDeath.Play();
    }

    public void WallGrinding()
    {

        if (isGrinding == true)
        {
            grindFX.SetActive(true);
        }

        else
            grindFX.SetActive(false);

    }




}
