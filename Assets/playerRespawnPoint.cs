using UnityEngine;
using System.Collections;

public class playerRespawnPoint : MonoBehaviour {

	public bool centerOfArena = true;

	// Use this for initialization
	void Start ()
	{
		if (centerOfArena)
		{
            float avgX = 0;
            float avgY = 0;
            GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
            foreach(GameObject wall in walls)
            {
                avgX += wall.transform.position.x;
                avgY += wall.transform.position.y;
            }
            avgX /= walls.Length;
            avgY /= walls.Length;

            this.transform.position = new Vector3(avgX, avgY, walls[0].transform.position.z);

		}
	}
	

}
