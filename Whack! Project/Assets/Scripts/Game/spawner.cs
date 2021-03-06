﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class spawner : MonoBehaviour {

    public List<GameObject> moles = new List<GameObject>();

    public List<GameObject> poofs = new List<GameObject>();

    public GameObject gameMan;

    public GameObject poofSprite;

    public GameObject whack;

    public float timer = 0.0f;

    public int selectedMole;

    public int x, y;

    public int score;

    bool spawned = false;

    bool onetime = false;  

    GameObject other;

    void Start()
    {        
        for(int x = 0; x <= 8; x++)
        {
            //Adds all the spawns to the list
            moles.Add(GameObject.FindGameObjectWithTag("mole"));
            moles[x].SetActive(false);
        }

        for(int y = 0; y <= 8; y++) //sP stands for selectedPoof
        {
            poofs.Add(GameObject.FindGameObjectWithTag("poof"));
            //poofs[y].SetActive(false);
        }

        if (!spawned) //Whilst nothing has been spawned
            {
                //Spawn moles
                selectedMole = Random.Range(0, 9);
                //Debug.Log("Selected mole is " + selectedMole); 
                moles[selectedMole].SetActive(true);
                //poofs[selectedMole].SetActive(true);
                spawned = true;
                onetime = false;
            }
    }


    void LoseLife()
    {
        GameObject gameMan = GameObject.FindWithTag("gameMan"); //Creates an instance of game manager so that the component lives can be used
        lives player = gameMan.GetComponent<lives>(); //Creates an instance of the lives script so that lives can be accessed
        player._lives--; 
        onetime = true;
    }

	// Update is called once per frame
	void Update () {
            other = moles[selectedMole];
            //poofAnim = moles[selectedMole].GetComponent<enablePoof>().poofAnim;
        if (other.GetComponent<hit>().whacked == true) 
            {
                other.GetComponent<hit>().whacked  = false;
                //Now spawn new mole
                selectedMole = Random.Range(0, 9);

                moles[selectedMole].SetActive(true);
            }
            //Mole missed
	        else if (timer >= 2.0f)
	        {
                moles[selectedMole].SetActive(false);

                selectedMole = Random.Range(0, 9);

                moles[selectedMole].SetActive(true);
               
                timer = 0.0f;
                //Now lose a life.
                if(!onetime)
                {
                    LoseLife();
                }
	        	//Decrement lives
	        	else if (timer >= 3.5f)
	       		{
	        		timer = 0.0f;
	        		spawned = false;
	        	}
	        }
	        timer += Time.deltaTime;
	        //Debug.Log("Timer is at: " +  timer);

           
    }

}
