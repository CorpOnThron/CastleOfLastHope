using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WallScript : MonoBehaviour {

    public static double UpgradeLVLWall = 2;
    public double Health = 200;
    public double RealHealth;
    public Slider slider;
    public Button healButton;
    
	// Use this for initialization
	void Start () {
        RealHealth = Health + ((Health + (10 * UpgradeLVLWall)) * 0.1 * UpgradeLVLWall);
        slider.maxValue = (float)RealHealth;
        slider.minValue = 0;
    }

    public void Heal()
    {
        if (RealHealth < slider.maxValue)
        {
            RealHealth += 2;
        }
    }

	// Update is called once per frame
	void Update () {
        if (RealHealth < slider.maxValue)
        {
            healButton.enabled = true;
        }
        else
        {
            healButton.enabled = false;
        }
        slider.value = (float)RealHealth;
        if (RealHealth <= 0) {

            Destroy(this.gameObject, 1);
            SceneManager.LoadScene("FailScreen");
        }
	}

    
}
