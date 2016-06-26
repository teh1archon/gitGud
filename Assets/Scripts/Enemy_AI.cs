using UnityEngine;
using System.Collections;

public class Enemy_AI : MonoBehaviour {

    public bool redEnemy;

    public GameObject player;
    GameManager gameManager;

    public GameObject enemy;

    float distance;

    Transform hideout;

    public float speed;

    bool enemyHit;

    bool stunned = false;
    public float stunTime = 1.2f;

    // public void OnEnable()
    // {
    // // GameController.AddEnemy(this);
    // }

    public void OnDestroy()
    {
        GameController.RemoveEnemy(this);
    }
    

    public void Stun()
    {
        stunned = true;
        Invoke("Unstun", stunTime);
    }

    private void Unstun()
    {
        stunned = false;
    }
    

    // Use this for initialization
    void Start()
    {
        GameController.AddEnemy(this);
        player = GameObject.FindGameObjectWithTag("player");
        enemyHit = false;
        gameManager = player.GetComponent<GameManager>();
        //hideout.transform.position = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (player != null && !stunned && !gameManager.invulnerable)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 100)
            {
                Vector3 direction = player.transform.position - transform.position;
                transform.position += (direction.normalized * speed * Time.deltaTime);
            }

            transform.LookAt(player.transform.position);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f)), ForceMode.Force);
        }

        if(enemyHit == true)
        {
            print("enemy is dead");
        }
    }

    //void OnCollisionEnter(Collider col)
    //{
    //    print("COLLISION");
    //    if (gameObject.tag == "box")
    //    {
    //        enemyHit = true;
    //        Destroy(gameObject, 0.1f);
    //    }
    //}

   

}
