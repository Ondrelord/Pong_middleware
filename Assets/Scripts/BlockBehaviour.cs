using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    [Header("Block")]
    public int lives;
    public bool lostLife;

    public int scoreWorth = 1;

    private Renderer rend;
    public List<Material> materials = new List<Material>();

    [Header("Power")]
    public GameObject powerObjectPrefab;
    public Power[] power;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (lostLife)
        {
            if (lives - 1 < materials.Count)
                rend.material = materials[lives - 1];
            else
                rend.material = materials[materials.Count - 1];
        }

        lostLife = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
            OnHit();
    }

    public virtual void OnHit()
    {
        if (lives <= 1)
        {
            StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
            GameObject.Find("Arena").GetComponent<ArenaManager>().EarnScore(scoreWorth);
            GetComponent<ParticleSystem>().Play();
            SpawnPower();
        }
        else
        {
            lostLife = true;
            lives -= 1;
        }
    }

    private void SpawnPower()
    {
        if (power.Length > 0)
        {
            GameObject pGO = Instantiate(powerObjectPrefab, transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
            pGO.GetComponent<PowerUpObject>().power = power[UnityEngine.Random.Range(0, power.Length)];
        }
    }

}
