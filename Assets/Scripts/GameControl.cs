using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public int balance;
    public int lvl;
    public int costClick;
    public int costArcher;
    public List<GameObject> walls;
    public Text balanceCount;
    public Text lvlCounter;
    public Text breakTime;
    public Text archerCost;
    public Text clickCost;
    public List<GameObject> enemy = new List<GameObject>();
    public int RandAmount;
    private int t;
    public double timeBreak;
    public int playerDmg;
    public int playerDmgUpg;
    public int archerDmgUpg;
    public Button btnArcher;
    public Button btnClick;
    //spawner
    public GameObject enemyInstance;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    // Use this for initialization
    void Start () {
        balance = 0;
        lvl = 1;
        costClick = 5;
        costArcher = 5;
        t = 0;
        timeBreak = 5.0f;
        breakTime.enabled = false;
        playerDmg = 1;
        playerDmgUpg = 1;
        archerDmgUpg = 1;
}
	
	// Update is called once per frame
	void Update () {
        
        
        
        SetCostText();
        SetCountText();
        SetlvlCounterText();

        if (balance >= costClick)
        {
            btnClick.enabled = true;
        }
        else
        {
            btnClick.enabled = false;
        }

        if (balance >= costArcher)
        {
            btnArcher.enabled = true;
        }
        else
        {
            btnArcher.enabled = false;
        }

        if (enemy.Count == 0)
        {
            breakTime.enabled = true;
            breakTime.text = "Break: " + (int)timeBreak;
            timeBreak -= Time.deltaTime;
            if (enemy.Count == 0 && t == 0)
            {
                t++;
                StartCoroutine(SpawnCoroutine());
                StopCoroutine(SpawnCoroutine());
            }
        }
    }

    public void UpgradeClick()
    {
        if (balance >= costClick)
        {
            playerDmgUpg++;
            balance = balance - costClick;
            playerDmg = playerDmg * playerDmgUpg;
            costClick = costClick * playerDmgUpg;
        }
    }
    public void UpgradeArcher()
    {
        if (balance >= costArcher)
        {
            archerDmgUpg++;
            balance = balance - costArcher;
            costArcher = costArcher * archerDmgUpg;
        }

    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(5);

        lvl++;
        RandAmount = Random.Range(1, 3) * lvl;


        while (RandAmount > 0)
        {
            randX = Random.Range(-5f, 5);
            randX = Random.Range(-3f, 3);
            whereToSpawn = new Vector2(randX, GameObject.Find("Spawn").GetComponent<Transform>().position.y - randY);
            Instantiate(enemyInstance, whereToSpawn, Quaternion.identity);
            RandAmount--;
        }
        timeBreak = 5;
        breakTime.enabled = false;
        t--;
    }

    void SetCountText()
    {   
        balanceCount.text = "Money: " + balance.ToString();
    }
    void SetlvlCounterText()
    {
        lvlCounter.text = "Level: " + lvl;
    }

    void SetCostText()
    {
        archerCost.text = "Cost: " + costArcher.ToString();
        clickCost.text = "Cost: " + costClick.ToString();
    }

    

}
