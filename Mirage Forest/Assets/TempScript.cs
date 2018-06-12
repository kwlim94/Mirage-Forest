using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
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


 





}
