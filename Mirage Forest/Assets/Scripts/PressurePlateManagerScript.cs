using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sequence
{
    public List<GameObject> sequenceList;
}

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
    public List<Sequence> listOfSeqenceList;

    public override void OtherStart ()
    {
        element = 0;
        sequenceTime = 0.0f;
        interval = 1.0f;
        isPlaySequence = true;
        initialLocation = transform.position;
    }

    public override void OtherUpdate()
    {
        PlaySequence();
        MoveHigher();
    }

    public void SequenceCheck(string name)
    {
        if (listOfSeqenceList[0].sequenceList[currentElement].name == name)
        {
            if(currentElement < listOfSeqenceList[0].sequenceList.Count - 1)
            {
                currentElement++;
                print("Correct");
            }
            else
            {
                sequenceTime = 0.0f;
                isMoveHigher = true;
                currentElement = 0;
                element = 0;
                print("moving higher");
            }
        }
        else if(listOfSeqenceList[0].sequenceList[currentElement - 1 ].name == name)
        {
            //! do nothing
        }
        else
        {
            Respawn(1.0f);
            StartCoroutine(WaitForSomeTime(2.1f));
            isPlaySequence = true;
            currentElement = 0;
            element = 0;
            print("respawn");
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
            CharacterControlScript.Instance.enabled = false;
            if (sequenceTime < element + interval)
            {
                listOfSeqenceList[0].sequenceList[element].transform.GetChild(0).GetComponent<Light>().enabled = true;
            }
            else
            {
                listOfSeqenceList[0].sequenceList[element].transform.GetChild(0).GetComponent<Light>().enabled = false;

                if (element < listOfSeqenceList[0].sequenceList.Count - 1)
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
        if (isMoveHigher)
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
                transform.DetachChildren();
                isMoveHigher = false;
            }

        }
    }

    IEnumerator WaitForSomeTime (float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
