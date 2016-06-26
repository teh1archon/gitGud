using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    //stage start
    public Image blackBG;

    //timer
    public Text timerText;

    //SCORE
    public Text scoreText;

    //enemies respawn
    static HashSet<Enemy_AI> activeEnemies = new HashSet<Enemy_AI>();
    public Transform[] spawnPoints;
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;

    public static void AddEnemy(Enemy_AI enemy)
    {
        activeEnemies.Add(enemy);
    }


    public static void RemoveEnemy(Enemy_AI enemy)
    {
        activeEnemies.Remove(enemy);
    }



    public int maxEnemies = 3;
    private void SpawnEnemies()
    {
        if (activeEnemies.Count < maxEnemies)
        {
            int enemType = Random.Range(0, 2);
            GameObject enemyPrefab = enemType == 0 ? redEnemyPrefab : blueEnemyPrefab;
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity)as GameObject;
            enemy.GetComponent<Enemy_AI>().speed += enemySpeedBonus * numOfDiffRamps;
        }
    }
    public GameManager manager_Access;

    public Text lifeText;
    private int currentScore;
    private int guiScore;

    public Image fuel;
    public float fuelBurn;
    private Vector2 currentFuel;

    public int revivngPrice;
    public bool shapeAlive;

    public void Awake()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        float time = 0;
        Color invis = new Color(0.1f, 0.05f, 0.1f, 0);
        while (time <= 1)
        {
            blackBG.color = Color.Lerp(Color.black, invis, time);
            time += (Time.deltaTime / 1.5f);
            yield return null;
        }
        //Destroy(blackBG.gameObject);
        blackBG.gameObject.SetActive(false);
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        float time = 60;
        InvokeRepeating("RampDifficultyUp", 0, time / numOfDiffRamps);
        while (time > 0)
        {
            //yield return new WaitWhile(() => manager_Access.invulnerable);
            timerText.text = "TIME " + ((int)time).ToString();
            time -= Time.deltaTime;
            yield return null;
        }
        manager_Access.gameOver = true;
        blackBG.GetComponentInChildren<Text>().text = "YOU WIN!";
        //add score and shit;
        blackBG.gameObject.SetActive(true);
        manager_Access.invulnerable = true;
    }

    public float enemySpeedBonus = 0.36f;
    public int numOfDiffRamps = 10;
    private int diffModifier = 0;

    private void RampDifficultyUp()
    {
        diffModifier++;
        maxEnemies++;
        foreach(Enemy_AI enem in activeEnemies)
        {
            enem.speed += enemySpeedBonus;
        }
    }

    // Use this for initialization
    void Start()
    {

        InvokeRepeating("SpawnEnemies", 5, 2);


        //currentFuel = fuel.rectTransform.sizeDelta;

    }

    // Update is called once per frame
    void Update()
    {
        //ScoreController();
        //fuelContoller();

        //if (Input.GetKeyDown("e"))
        //	UpdateSore(true, 50);

        //else if (Input.GetKeyDown("r"))
        //	UpdateSore(false, 50);

        //if (Input.GetKeyDown("space"))
        //		ReviveShape(revivngPrice);
    }

    private void ScoreController()
    {//@ func that display the score of the player
        scoreText.text = "LIFE: " + guiScore.ToString() + "/" + manager_Access.Life;

        if (guiScore < currentScore)
        {
            if (currentScore - guiScore >= 500)
                guiScore += 100;

            else if (currentScore - guiScore >= 50)
                guiScore += 10;

            else
                guiScore++;
        }
        else if (guiScore > currentScore)
        {
            if (guiScore - currentScore >= 500)
                guiScore -= 100;

            else if (guiScore - currentScore >= 50)
                guiScore -= 10;

            else
                guiScore--;
        }
    }

    internal void UpdateSore(bool isAdding, int amount)
    {//@ func that update the score when the player collect or lose points
        if (isAdding)
            currentScore += amount;

        else
        {
            if (currentScore - amount > 0)
                currentScore -= amount;

            else
                currentScore = 0;
        }
    }

    private void fuelContoller()
    {


        fuel.rectTransform.sizeDelta = new Vector2(currentFuel.x, currentFuel.y);

        if (Input.GetKey("space"))
            FuelUpdate(fuelBurn);

        if (Input.GetKey("v"))
            FuelUpdate(5);

    }

    private void FuelUpdate(float amount)
    {
        float maxFuel = 370;

        if ((currentFuel.x + amount < maxFuel) && (currentFuel.x + amount > 0))
            currentFuel.x += amount;

        else if (currentFuel.x + amount > maxFuel)
            currentFuel.x = maxFuel;

        else if (currentFuel.x + amount < 0)
            currentFuel.x = 0;
    }

    private void ReviveShape(int amount)
    {//@ func that revive a dead shape if possible
        if (shapeAlive == false)
            if (currentScore - amount >= 0)
            {
                UpdateSore(false, amount);
                shapeAlive = true;
                print("the shape is alive");
            }
    }
}
