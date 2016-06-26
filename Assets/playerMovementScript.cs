using UnityEngine;
using System.Collections;

public class playerMovementScript : MonoBehaviour
{
    public float speedChange;
    Vector3 velocity;
    public float maxSpeed;
    public GameObject soundWavePrefab;

    // Use this for initialization
    void Start()
    {
        velocity = GetComponent<Rigidbody>().velocity;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, speedChange));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -speedChange));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(-speedChange, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(speedChange, 0, 0));
        }
        GetComponent<Rigidbody>().velocity = ClampMovement();
    }

    Vector3 ClampMovement()
    {
        return new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(GetComponent<Rigidbody>().velocity.y, -maxSpeed, maxSpeed), Mathf.Clamp(GetComponent<Rigidbody>().velocity.z, -maxSpeed, maxSpeed));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            GameObject s = Instantiate(soundWavePrefab, this.transform.position, Quaternion.identity) as GameObject;
            print((this.GetComponent<Rigidbody>().velocity.magnitude));
            s.GetComponent<soundWaveScript>().setInitialScale(this.GetComponent<Rigidbody>().velocity.magnitude, true);
        }
    }
}
