    using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public Transform crossair;
    Vector3 mouse_pos;
    Vector3 object_pos;
    private float angle;


    internal bool isGround = false;


    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {


        //Mouse tracker
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg - 90);
    }


}



