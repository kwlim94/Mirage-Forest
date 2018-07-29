﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PressurePlateManagerScript : RespawnManagerScript
{

    public static PressurePlateManagerScript Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }


    //! to check which elevation it is now
    int floorCount;
    //! used for increament of the lighting reveal
    int element;
    //! used for keeping track of the element that the player already pressed
    int currentElement;
    float sequenceTime;
    float interval;
    bool isPlaySequence;
    bool isMoveHigher;
    Vector3 initialLocation;

    public float incrementHeight;
    public List<GameObject> pressurePlateList;
    public List<GameObject> currentSeqenceList;
    //! How many pattern to randomize for the particular elevation
    public List<int> patternCountList;

    public override void OtherStart ()
    {
        floorCount = 0;
        sequenceTime = 0.0f;
        currentElement = 0;
        interval = 1.0f;
        isPlaySequence = true;
        initialLocation = transform.position;
        RandomizeSequence();
    }

    public override void OtherUpdate()
    {
       
        if(isMoveHigher)
            MoveHigher();
        else
            PlaySequence();
    }

    public void SequenceCheck(string name)
    {
        //HT if it matches the one in the sequence
        if (currentSeqenceList[currentElement].name == name)
        {
            //HT increase it by one if it is not the last one
            if(currentElement < currentSeqenceList.Count - 1)
            {
                currentElement++;
                print("Correct");
            }
            //HT if it is the last one in the list then move higher
            else
            {
                floorCount++;
                RandomizeSequence();

                sequenceTime = 0.0f;
                isMoveHigher = true;
                currentElement = 0;
                print("moving higher");
            }
        }
        //HT if it doesnt match the one in the sequence
        else
        {
            //! if the current element is zero no need to check the previous one
            if(currentElement == 0)
            {
                Respawn(1.0f);
                RandomizeSequence();
                StartCoroutine(WaitForSomeTime(2.1f));

                isPlaySequence = true;
                currentElement = 0;
                print("respawn");
            }
            //! else, check if the one pressed it's the same one as before, to avoid complications of getting a respawn by pressing the same one twice
            else if (currentSeqenceList[currentElement - 1].name != name)
            {
                Respawn(1.0f);
                RandomizeSequence();
                StartCoroutine(WaitForSomeTime(2.1f));

                isPlaySequence = true;
                currentElement = 0;
                print("respawn");
            }
        }
    }

    void RandomizeSequence()
    {
        if(floorCount < patternCountList.Count - 1)
        {
            int prevRandomPressurePlate = -1;

            for(int i = 0; i < patternCountList[floorCount] - 1; i++)
            {
                int randomPressurePlate = -1;

                do
                {
                    randomPressurePlate = Random.Range(0, pressurePlateList.Count);
                    print("Randomizing Sequence for element " + i);
                }
                while (randomPressurePlate == prevRandomPressurePlate);
                currentSeqenceList[i] = pressurePlateList[randomPressurePlate];
                prevRandomPressurePlate = randomPressurePlate;
            }
        }
    }

    void PlaySequence()
    {
        /*
         Which one in the list?
         Play in seqence 1 sec at a time
         */

        if(isPlaySequence)
        {

            element = 0;

            CharacterControlScript.Instance.enabled = false;
            if (sequenceTime < element + interval)
            {
                currentSeqenceList[element].transform.GetChild(0).GetComponent<Light>().enabled = true;
            }
            else
            {
                currentSeqenceList[element].transform.GetChild(0).GetComponent<Light>().enabled = false;

                if (element < currentSeqenceList.Count - 1)
                    element++;
                else
                {
                    CharacterControlScript.Instance.enabled = true;
                    isPlaySequence = false;
                    sequenceTime = 0.0f;
                }
            }
            sequenceTime += Time.deltaTime;
        } 
            
    }

    void MoveHigher ()
    {
        if (sequenceTime < 1.0f)
        {
            CharacterControlScript.Instance.enabled = false;
            CharacterControlScript.Instance.anim.SetBool("Walk", false);
            CharacterControlScript.Instance.transform.SetParent(this.transform);
            transform.position = new Vector3(initialLocation.x, transform.position.y + incrementHeight * Time.deltaTime, initialLocation.z);
            sequenceTime += Time.deltaTime;
        }
        else
        {
            CharacterControlScript.Instance.enabled = true;
            //HT Unity scripting doesn't support remving one child, so will need to remove all children first
            transform.DetachChildren();

            //HT Then reparent all the pressure plates back
            for(int i = 0; i < pressurePlateList.Count - 1; i++)
            {
                pressurePlateList[i].transform.SetParent(this.transform);
            }

            sequenceTime = 0.0f;
            isMoveHigher = false;
            isPlaySequence = true;
        }
    }

    IEnumerator WaitForSomeTime (float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
