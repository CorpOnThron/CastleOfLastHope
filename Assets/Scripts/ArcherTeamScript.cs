using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class ArcherTeamScript : MonoBehaviour {
    public int UpgradeLVLTeamArcher;
    public double Health = 30;
    public double RealHealth;
    public int damage;
    public GameObject target;
    public int i = 0;
    public bool isAlive;

    //bullet control
    public GameObject prefabArrow;
    //under construction

    // Use this for initialization
    void Start () {
        RealHealth = Health;
        
        target = null;
        isAlive = true;
    }
	
	// Update is called once per frame
	void Update () {
        UpgradeLVLTeamArcher = GameObject.Find("GameController").GetComponent<GameControl>().archerDmgUpg;
        damage = (int)(UpgradeLVLTeamArcher * 5);

        if (isAlive == true)
        {
            
            if (RealHealth <= 0)
            {
                isAlive = false;
                Destroy(this.gameObject, 1);

            }

            if (target == null)
            {
                target = GetNearestTarget();

            }
            else if (Vector2.Distance(target.transform.position, this.transform.position) < 20)
            {
                if (target.GetComponent<EnemyScript>().isAlive == true)
                {
                    tryAttack(target);
                }
                else
                {
                    target = GetNearestTarget();
                }
            }
            else if (Vector2.Distance(target.transform.position, this.transform.position) > 20)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 3);
            }
        }
    }

    public void tryAttack(GameObject enemy)
    {
        
        
        if (i == 0)
        {
            //this.transform.LookAt(enemy.GetComponent<Transform>());
            GameObject Arrow = Instantiate(prefabArrow, transform.position, Quaternion.identity);
            Arrow.GetComponent<Transform>().Rotate(0,0,180);
            Arrow.GetComponent<ArrowControl>().Enemy = enemy;
            //prefabArrow.GetComponent<ArrowControl>().Enemy = enemy;
            //prefabArrow.GetComponent<ArrowControl>().ParentName = gameObject.name;
            i++;
            if (enemy.GetComponent<EnemyScript>().isAlive == true)
            {
                StartCoroutine(AttackCoroutine(enemy));
                StopCoroutine(AttackCoroutine(enemy));
            }
        }

    }
    IEnumerator AttackCoroutine(GameObject Enemy)
    {
        yield return new WaitForSeconds(1);
        try
        {
            if (GameObject.Find(target.name) != null && target.GetComponent<EnemyScript>().isAlive == true)
            {




                //target.GetComponent<EnemyScript>().RealHealth = target.GetComponent<EnemyScript>().RealHealth - damage;
            }
        }
        catch
        {
        }
        i = 0;
        yield break;
    }


    GameObject GetNearestTarget()
    {

        if (GameObject.Find("GameController").GetComponent<GameControl>().enemy.Count() == 0)
        {
            Debug.Log(GameObject.Find("GameController").GetComponent<GameControl>().enemy.Count());
            return null;
        }
        else
        {
            try
            {
                return GameObject.Find("GameController").GetComponent<GameControl>().enemy.First(p => p != null && p.GetComponent<EnemyScript>().isAlive == true);
            }
            catch
            {
                return null;
            }
            }
            
        }

    
}
