using UnityEngine;
using System.Collections;

public class soundWaveScript : MonoBehaviour
{
    public float initialLifetime = 0.5f;
    public float lingeringLifetime = 1f;
    public float expansionSizeModifier = 1.1f;
    public float impactSizeModifier;
    public float maxRadius = 22; //max unclamped allowed radius
    public float forceModifier = 100; //adjust overall force
    float impactSize; //radius size AKA impact force input from the player
    bool doDamage;
    public bool redWave = false;

    public void setInitialScale(float impactSize, bool _doDamage)
    {
        //transform.localScale *= impactSize * impactSizeModifier;
        doDamage = _doDamage;
        this.impactSize = impactSize;
        StartCoroutine(InitialExpansion());

    }

    IEnumerator InitialExpansion()
    {
        float time = 0f;
        Vector3 originalScale = transform.localScale;
        //Detect enemies here!!!

        //And only then expand the dong
        while (time < initialLifetime)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * impactSize, time / initialLifetime);
            time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(SoftExpansion());
    }

    IEnumerator SoftExpansion()
    {
        float time = 0f;
        Vector3 originalScale = transform.localScale;
        while (time < lingeringLifetime)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.1f, time / lingeringLifetime);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pushable")
        {
            BoomBox bb = other.GetComponent<BoomBox>();
            if (bb == null) return;

            if (bb.chargedUp)
                return;

            //maybe apply force on the point of impact? not quite easy to get with trigger
            Vector3 dir = other.transform.position - this.transform.position;
            Rigidbody boomBoxRigidBody = other.GetComponent<Rigidbody>();

            //could consider these:
            /*
            boomBoxRigidBody.AddForceAtPosition
            boomBoxRigidBody.AddExplosionForce
            */

            //old version: impact force higher the closer the object is to the impact point
            //boomBoxRigidBody.AddForce(dir.normalized * (Mathf.Abs((1 - dir.magnitude / impactSize)) / maxRadius) * forceModifier, ForceMode.Impulse);

            //new version: forced is the same for every object, determined by the blast radius
            boomBoxRigidBody.AddForce(dir.normalized * impactSize, ForceMode.Impulse);

            if (doDamage)
                bb.ChargeUp(redWave);
        }
        else if (other.tag == "enemy")
        {
            Enemy_AI enem = other.GetComponent<Enemy_AI>();
            if (enem.redEnemy == this.redWave)
            {
                if (doDamage)
                    enem.Stun();
                else
                    other.GetComponent<Rigidbody>().AddForce((other.transform.position - this.transform.position).normalized * impactSize, ForceMode.Impulse);
            }
        }
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    public void OnTriggerStay(Collider other)
    {

    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    public void OnTriggerExit(Collider other)
    {

    }
}
