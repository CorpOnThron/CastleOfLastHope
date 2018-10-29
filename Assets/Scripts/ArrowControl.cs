using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControl : MonoBehaviour
{

    public GameObject Enemy;

    // Use this for initialization

    void Start ()
    {
        //Enemy = GameObject.Find("team-archer1").GetComponent<ArcherTeamScript>().target;
	}

    // Update is called once per frame
    void Update()
    {
        if (Enemy != null)
        {
            //this.transform.LookAt(Enemy.GetComponent<Transform>());
            transform.position = Vector2.MoveTowards(transform.position, Enemy.GetComponent<Transform>().position, Time.deltaTime * 4);
            if (Vector2.Distance(Enemy.transform.position, this.transform.position) == 0)
            {
                Enemy.GetComponent<EnemyScript>().RealHealth = Enemy.GetComponent<EnemyScript>().RealHealth - GameObject.Find("team-archer1").GetComponent<ArcherTeamScript>().damage;
                Destroy(gameObject,0.1f);
            }
        }
        if (Enemy == null)
        {
            Destroy(gameObject, 0.1f);
        }
        if (Enemy == null )
        {
            Destroy(gameObject, 0.1f);
            
        }
    }
}
