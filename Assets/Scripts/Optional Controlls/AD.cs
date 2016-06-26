using UnityEngine;
using System.Collections;

public class AD : MonoBehaviour {


    public GameObject crosshair;

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey("a"))
        {
            transform.Rotate(Vector3.forward *  speed);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.back * speed);
        }

    }
}
