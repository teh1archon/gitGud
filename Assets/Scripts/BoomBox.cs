using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoomBox : MonoBehaviour {

    public bool giveInitialVelocity;
    Rigidbody rigidBody;
    public bool chargedUp = false;
    bool chargedRed; //false = blue
    public float minVelocityToBeCharged = 0.1f;
    public int HP = 5;
    [SerializeField]
    Material chargedUpMaterialRed;
    [SerializeField]
    Material chargedUpMaterialBlue;
    private Material defaultMaterial;
    private Renderer renderer;
 //   public float velocity;
    private int currentScore = 0;
    public GameObject FloatingScorePrefab;
    //magic numbers for scoring hard coded. change in case of more robust use is needed

    //void Update()
    //{
    //    velocity = rigidBody.velocity.magnitude;
    //}

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;

        if (giveInitialVelocity)
        {
            rigidBody.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
            rigidBody.AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
        }
    }

    public void ChargeUp(bool red)
    {
        if (chargedUp) return;
        print(this.name + " powered up at " + Time.time);
        chargedUp = true;
        chargedRed = red;
        renderer.material = red? chargedUpMaterialRed : chargedUpMaterialBlue;
        StopAllCoroutines();
        StartCoroutine("PowerDown");
    }

    IEnumerator PowerDown()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(0.2f);
//        print("initial velocity is " + rigidBody.velocity.magnitude);
        yield return new WaitUntil(() => rigidBody.velocity.magnitude <= minVelocityToBeCharged);
        chargedUp = false;
        renderer.material = defaultMaterial;
        HP--;
        currentScore = 0;
        if (HP <= 0)
            Destroy(this.gameObject);
 //       print(this.name + "powered down at " + Time.time);
    }
    
    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    public void OnCollisionEnter(Collision collision)
    {
        if (!chargedUp)
            return;

        if (collision.collider.tag == "enemy")
        {
            if (collision.collider.GetComponent<Enemy_AI>().redEnemy != chargedRed)
                return;
 //           print(this.name + " fucked up " + collision.collider.name + " for " + rigidBody.velocity.magnitude + " damage and killed it");
            Destroy(collision.collider.gameObject);
            currentScore += 100;
            PutScore(collision.contacts[0].point);
            //GameObject updateScore = GameObject.Find("Score");
            //int prevScore = System.Convert.ToInt32(updateScore.GetComponent<Text>());
            //updateScore.GetComponent<Text>().text = (prevScore + currentScore).ToString();
            return;
        }

        if (collision.collider.tag == "wall")
        {
            currentScore += 50;
            //PutScore(collision.contacts[0].point);
            return;
        }

        if (collision.collider.tag == "Pushable")
        {
            currentScore += 25;
            //PutScore(collision.contacts[0].point);
            return;
        }
    }



    private void PutScore(Vector3 position)
    {
        GameObject score = Instantiate(FloatingScorePrefab, position, Quaternion.identity) as GameObject;
        if (currentScore > 100)
            score.GetComponent<TextMesh>().text = currentScore.ToString() + "\nTrick Shot!";
        else
            score.GetComponent<TextMesh>().text = currentScore.ToString();
        Destroy(score, 1.5f);
    }

    //// OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider
    //public void OnCollisionStay(Collision collision)
    //{

    //}

    //// OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider
    //public void OnCollisionExit(Collision collision)
    //{

    //}
}
