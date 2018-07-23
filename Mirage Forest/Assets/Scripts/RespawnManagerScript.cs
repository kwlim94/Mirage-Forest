using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManagerScript : MonoBehaviour
{
    public Transform respawmPoint;
    public Image blackScreen;
    bool isRespawn;
    float fadeTime;
    float time;
    float fadeInterval;

    void Start()
    {
        isRespawn = false;  
    }

    void Update()
    {
        if(isRespawn)
        {
            if(time < fadeTime)
            {
                blackScreen.color = new Vector4(0.0f, 0.0f, 0.0f, blackScreen.color.a + fadeInterval * Time.deltaTime);
            }
            else if(time > fadeTime && time < fadeTime * 2)
            {
                blackScreen.color = new Vector4(0.0f, 0.0f, 0.0f, blackScreen.color.a - fadeInterval * Time.deltaTime);
            }
            else if(time > fadeTime * 2)
            {
                CharacterControlScript.Instance.enabled = true;
                CharacterControlScript.Instance.transform.position = respawmPoint.position;
                isRespawn = false;
            }
            time += Time.deltaTime;
        }
    }

    public void Respawn(float _fadeTime)
    {
        isRespawn = true;
        fadeTime = _fadeTime;
        time = 0.0f;
        fadeInterval = 1.0f / fadeTime;
        CharacterControlScript.Instance.enabled = false;
        CharacterControlScript.Instance.anim.SetBool("Walk", false);
    }
}
