using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Transform SpawnArea;
    public int maxPowerUpAmount;
    public int spawnInterval;
    public Vector2 powerUpAreaMin;
    public Vector2 powerUpAreaMax;
    public List<GameObject> powerUPTemplateList;
    
    private List<GameObject> powerUPList;

    private float timer;

    private void Start()
    {
        powerUPList = new List<GameObject>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            GenerateRandomPowerUp();
            timer -= spawnInterval;
        }
    }

    public void GenerateRandomPowerUp()
    {
        GenerateRandomPowerUp(new Vector2(Random.Range(powerUpAreaMin.x, powerUpAreaMax.x), Random.Range(powerUpAreaMin.y, powerUpAreaMax.y)));
    }

    public void GenerateRandomPowerUp(Vector2 position)
    {
        if (powerUPList.Count >= maxPowerUpAmount)
        {
            return;
        }
        
        if (position.x < powerUpAreaMin.x || 
            position.x > powerUpAreaMax.x || 
            position.y < powerUpAreaMin.y || 
            position.y > powerUpAreaMax.y) 
        { 
            return; 
        } 
            int randomIndex = Random.Range(0, powerUPTemplateList.Count);

            GameObject powerUp = Instantiate(powerUPTemplateList[randomIndex], new Vector3 (position.x, position.y, powerUPTemplateList[randomIndex].transform.position.z), Quaternion.identity, SpawnArea);
            powerUp.SetActive(true);

            powerUPList.Add(powerUp);
    }

    public void RemovePowerUp(GameObject powerUp)
    {
        powerUPList.Remove(powerUp);
        Destroy(powerUp);
    }

    public void RemoveAllPowerUp()
    {
        while(powerUPList.Count > 0)
        {
           RemovePowerUp(powerUPList[0]);
        }
    }
}
