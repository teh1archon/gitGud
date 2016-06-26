using UnityEngine;
using System.Collections;

public class Pfx : MonoBehaviour {

    public Jetpack Jetpack_Access;

    ParticleSystem Psystem;
    public Transform cube_offset;

	// Use this for initialization
	void Start () {
        
        transform.position = cube_offset.position;

        Psystem = GetComponent<ParticleSystem>();

        var em = Psystem.emission;
        em.enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {

        //fires up particles once player activate action key.
        transform.position = cube_offset.position;
        if (Jetpack_Access.isFloat)
        {
            var em = Psystem.emission;
            em.enabled = true;
        }

        else
        {
            var em = Psystem.emission;
            em.enabled = false;
        }

    }
}
