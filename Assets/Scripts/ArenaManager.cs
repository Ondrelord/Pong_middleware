using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    [Header("Game Variables")]
    public float defaultSpeed;
    public float speed;

    public int lives;
    public Transform LifeCounter;
    public GameObject LifeImage;

    public Transform scoreCounter;
    public int score;

    public GameObject ballPrefab;
    public Transform Balls;

    [Space]
    [Header("Blocks")]
    public GameObject Blocks;
    public bool randomPowers;
    public Power[] powerUps;
    public Power[] powerDowns;

    /*[Header("Arena Size")]
    [Range(2,15)]
    public float arenaWidth;
    [Range(2, 15)]
    public float arenaHeight;
    [Range(5, 30)]
    public float arenaLenght;

    public GameObject wallPrefab;*/
    
    // Start is called before the first frame update
    void Start()
    {
        //CreateArena();

        score = FindObjectOfType<GameManager>().score;
        scoreCounter.GetComponent<TextMeshProUGUI>().text = score.ToString();
        
        lives = FindObjectOfType<GameManager>().lives;
        for (int i = 0; i < lives; i++)
            NewLife();

        // Random power assignment to blocks
        if (randomPowers)
        {
            int numPowers = Mathf.CeilToInt(Blocks.transform.childCount / 4f);

            // powerups
            while (numPowers > 0)
            {
                int randomBlockIdx = Random.Range(0, Blocks.transform.childCount);
                BlockBehaviour block = Blocks.transform.GetChild(randomBlockIdx).GetComponent<BlockBehaviour>();

                if (block.power.Length != 0)
                    continue;

                block.materials.Clear();

                if (Random.Range(0f, 1f) < 0.5f)
                {
                    block.power = powerUps;
                    block.materials.Add(Blocks.GetComponent<BlocksMaterials>().UpBlock);
                }
                else
                {
                    block.power = powerDowns;
                    block.materials.Add(Blocks.GetComponent<BlocksMaterials>().DownBlock);
                }

                numPowers--;
            }

            // 2 lives 
            for (int i = 0; i < Blocks.transform.childCount; ++i)
            {
                BlockBehaviour block = Blocks.transform.GetChild(i).GetComponent<BlockBehaviour>();

                if (block.materials.Count == 0)
                    block.materials.Add(Blocks.GetComponent<BlocksMaterials>().BasicBlock);

                if (Random.Range(0f, 1f) <= 0.3f)
                {
                    block.lives++;
                    block.materials.Add(Blocks.GetComponent<BlocksMaterials>().StrongBlock);
                }

                block.lostLife = true;
            }
        }

        CreateBall();
    }
     
    // Update is called once per frame
    void Update()
    {
        if (Blocks.transform.childCount == 0)
        {
            FindObjectOfType<GameManager>().NextLevel(lives);
        }

        if (Balls.childCount == 0)
        {
            LostLife();
        }

    }

    /*void CreateArena()
    {
        createWall(1, 0, 0);
        createWall(-1, 0, 0);
        createWall(0, 1, 0);
        createWall(0, -1, 0);
        createWall(0, 0, 1);
    }

    void createWall(int x, int y, int z)
    {
        GameObject wall;

        wall = Instantiate(wallPrefab, transform);

        if (z == 0)
        {
            wall.transform.localScale = new Vector3(arenaHeight, 0.1f, arenaLenght);
            wall.transform.position = new Vector3(x * arenaWidth / 2, y * arenaHeight / 2, arenaLenght / 2);
        }
        else
        {
            wall.transform.localScale = new Vector3(arenaHeight, 0.1f, arenaWidth);
            wall.transform.position = new Vector3(x * arenaWidth / 2, y * arenaHeight / 2, arenaLenght);
        }

        float zRotation = (y >= 0) ? y * 180 + x * 90 : x * 90;
        wall.transform.rotation = Quaternion.Euler(z * -90, 0, zRotation);
    }
    */

    public void CreateBall(bool multiply = false)
    {
        Vector3 position;
        if (!multiply)
            position = Balls.position;
        else
            position = Balls.GetChild(0).position;
        GameObject GO = Instantiate(ballPrefab, position, Quaternion.identity, Balls);
        GO.GetComponent<BallController>().speed = speed;

        if (multiply) GO.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * speed, ForceMode.VelocityChange);
    }

    public void NewLife()
    {
        Instantiate(LifeImage, LifeCounter);
    }

    public void ChangeSpeed(float value)
    {
        for (int i = 0; i < Balls.childCount; ++i)
        {
            Balls.GetChild(i).GetComponent<BallController>().speed *= value;
        }
    }

    public void LostLife()
    {
        if (lives > 1)
        {
            Destroy(LifeCounter.GetChild(LifeCounter.childCount - 1).gameObject);
            lives--;

            if (Balls.childCount == 0)
                CreateBall();
        }
        else
        {
            SceneManager.LoadScene("Main Menu");
        }

    }

    public void EarnScore(int amount)
    {
        FindObjectOfType<GameManager>().score = score += amount;
        scoreCounter.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
