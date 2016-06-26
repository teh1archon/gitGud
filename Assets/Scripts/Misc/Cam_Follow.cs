using UnityEngine;
using System.Collections;

public class Cam_Follow : MonoBehaviour {


    public Transform character;
    public float smooth;


	
	// Update is called once per frame
	void Update () {

        //The camera follows the character.
        transform.position = Vector3.Lerp(transform.position, character.position, smooth);

    }
}
