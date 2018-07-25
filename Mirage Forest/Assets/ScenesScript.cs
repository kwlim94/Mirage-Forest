using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesScript : MonoBehaviour
{
	void Start () 
	{
		Cursor.visible = true; //HT Temporary put it here first
	}

	public void StartGame ()
	{
		SceneManagerScript.Instance.LoadScene(1);
	}

	public void Quit ()
	{
		Application.Quit();
	}

    public void MainMenu()
    {
        SceneManagerScript.Instance.LoadScene(101);
    }

    public void Setting()
    {
        SceneManagerScript.Instance.LoadScene(102);
    }

    public void SoundSetting()
    {
        SceneManagerScript.Instance.LoadScene(103);
    }

    public void ControlSetting()
    {

    }
    public void Credits()
    {
        SceneManagerScript.Instance.LoadScene(104);
    }

    public void OnPause()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
        }
        else
            Time.timeScale = 1;
    }
    








}
