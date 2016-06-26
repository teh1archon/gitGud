using UnityEngine;
using System.Collections;

public class Impactor : MonoBehaviour {


    public AudioSource shockWave;
    public GameObject blueWavePrefab;
    public GameObject redWavePrefab;
    public float maxNonCollisionImpactSize = 22;
    bool canPulse = true;
    public float pulseInverval = 2;
    public float minPulseInterval = 0.5f; //to avoid spamming when dragging against a wall

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall" && canPulse)
        {
            GameObject s = Instantiate(blueWavePrefab, this.transform.position, Quaternion.identity) as GameObject;
            print((this.GetComponent<Rigidbody>().velocity.magnitude));
            s.GetComponent<soundWaveScript>().setInitialScale(this.GetComponent<Rigidbody>().velocity.magnitude, true);
            StartCoroutine(PulseCooldown(minPulseInterval));
            shockWave.Play();
        }
    }

    void Update()
    {
 //     if (canPulse && Input.GetMouseButton(1))
        if (canPulse && Input.GetKeyDown(KeyCode.Z))
        {
            GameObject s = Instantiate(blueWavePrefab, this.transform.position, Quaternion.identity) as GameObject;
            s.GetComponent<soundWaveScript>().setInitialScale(maxNonCollisionImpactSize, true);
            StartCoroutine(PulseCooldown(pulseInverval));
            shockWave.Play();
        }

        else if (canPulse && Input.GetKeyDown(KeyCode.X))
        {
            GameObject s = Instantiate(redWavePrefab, this.transform.position, Quaternion.identity) as GameObject;
            s.GetComponent<soundWaveScript>().setInitialScale(maxNonCollisionImpactSize, true);
            StartCoroutine(PulseCooldown(pulseInverval));
            shockWave.Play();
        }
    }

    public void FalsePulse()
    {
        GameObject s = Instantiate(blueWavePrefab, this.transform.position, Quaternion.identity) as GameObject;
        s.GetComponent<soundWaveScript>().setInitialScale(maxNonCollisionImpactSize, false);
        StartCoroutine(PulseCooldown(pulseInverval));
        shockWave.Play();
    }

    IEnumerator PulseCooldown(float cooldownTime)
    {
        canPulse = false;
        yield return new WaitForSeconds(cooldownTime);
        canPulse = true;
    }
}
