using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class EnemyScript : MonoBehaviour {

    public static double UpgradeLVLEnemy;
    public double Health = 30;
    public double RealHealth;
    public Slider slider;
    public int bounty;
    public int damage;
    private GameObject target;
    private int i = 0;
    public bool isAlive;
    

    // Use this for initialization
    void Start()
    {
        UpgradeLVLEnemy = 1 + GameObject.Find("GameController").GetComponent<GameControl>().lvl;
        RealHealth = Health + (Health * 0.1 * UpgradeLVLEnemy);
        slider.maxValue = (float)RealHealth;
        slider.minValue = 0;
        bounty = (int)(RealHealth / 10);
        damage = (int)(UpgradeLVLEnemy * 5);
        target = null;
        GameObject.Find("GameController").GetComponent<GameControl>().enemy.Add(gameObject);
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isAlive == true)
        {
            slider.value = (float)RealHealth;
            if (RealHealth <= 0)
            {
                isAlive = false;
                GameObject.Find("GameController").GetComponent<GameControl>().balance += bounty;
                GameObject.Find("GameController").GetComponent<GameControl>().enemy.Remove(gameObject);
                Destroy(this.gameObject, (float)0.1);

            }

            if (target == null)
            {
                target = GameObject.Find("Wall");

            }
            else if (Vector2.Distance(target.transform.position, this.transform.position) < 1)
            {
                tryAttack(target);
            }
            else if (Vector2.Distance(target.transform.position, this.transform.position) > 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * (float)1.2);
            }
        }
    }

    void OnMouseDown()
    {
        RealHealth = RealHealth - GameObject.Find("GameController").GetComponent<GameControl>().playerDmg;
    }

        public void tryAttack(GameObject enemy)
    {
        if (i == 0)
        {
            i++;
            StartCoroutine(AttackCoroutine(enemy));
            StopCoroutine(AttackCoroutine(enemy));
        }

    }
    IEnumerator AttackCoroutine(GameObject Enemy)
    {
        yield return new WaitForSeconds(1);
        if (Enemy!= null && GameObject.Find(Enemy.name) != null)
        {
            Enemy.GetComponent<WallScript>().RealHealth = Enemy.GetComponent<WallScript>().RealHealth - damage;
        }
            i = 0;
        yield break;
    }

    //GameObject GetNearestTarget()
    //{
    //    try
    //    {
    //        //so lets say you want the closest target from a array (in this case all Gameobjects with Tag "enemy") and let's assume this script right now is on the player (or the object with which the distance has to be calculated)
    //        return GameObject.FindGameObjectsWithTag("player").Aggregate((o1, o2) => Vector3.Distance(o1.transform.position, this.transform.position) > Vector3.Distance(o2.transform.position, this.transform.position) ? o2 : o1);
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //    }
}
