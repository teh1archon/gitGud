using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    GameObject grabed_obj;
    bool grabed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Drop
        if (grabed && Input.GetMouseButtonUp(1))
        {
            print("PickUp: Drop");
            grabed_obj.transform.position = transform.position + transform.right;   //Side grab
            grabed_obj.transform.SetParent(null);
            grabed_obj.GetComponent<Rigidbody>().isKinematic = false;
            grabed_obj.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            grabed = false;
        }
    }

    void OnTriggerStay(Collider obj)
    {
        if (Input.GetMouseButton(1) && obj.tag == "box" && !grabed)
        {
            print("PickUp: Pickup");
            grabed_obj = obj.gameObject;
            grabed_obj.GetComponent<Rigidbody>().isKinematic = true;
            grabed_obj.transform.position = transform.position + transform.forward;     //Side grab
            //grabed_obj.transform.position = transform.position + transform.right;     //Front Grab
            grabed_obj.transform.SetParent(transform);
            grabed = true;
        }

    }
}