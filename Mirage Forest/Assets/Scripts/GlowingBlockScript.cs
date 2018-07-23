using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    Two Things
    - Off after a certian timing
    - Interval
    - Stay
    - Pitfall
 
*/

enum GlowSwitch
{
    ON = 0,
    OFF = 1,
}

public enum BlockType
{
    PITFALL = 0,
    TIMED_GLOW,
    INTERVAL_GLOW,
    CONSTANT_GLOW,
}

public class GlowingBlockScript : MonoBehaviour
{
    //! private variables

    //HT The light used to make the block glow
    Light GlowingLight;
    //HT To determine whether the block will glow or not
    GlowSwitch glowSwitch;
    //HT delta time update per frame
    float time;
   
    //! public variables

    //HT Determines what type of block it is
    public BlockType blockType;
    //HT Used for TIMED_GLOW and INTERVAL_GLOW
    public float timeInterval;

    void Start ()
    {
        GlowingLight = gameObject.transform.GetChild(0).GetComponent<Light>();
        glowSwitch = GlowSwitch.OFF;
        time = 0.0f;
    }

    void Update ()
    {
        switch(blockType)
        {
            case BlockType.PITFALL:
                PitfallFunction();
                break;

            case BlockType.TIMED_GLOW:
                TimedGlowFunction();
                break;

            case BlockType.INTERVAL_GLOW:
                IntervalGlowFunction();
                break;

            case BlockType.CONSTANT_GLOW:
                glowSwitch = GlowSwitch.ON;
                break;
        }

        LightControl();

        time += Time.deltaTime;
    }

    void LightControl ()
    {
        if(glowSwitch == GlowSwitch.ON)
        {
            GlowingLight.enabled = true;
        }
        else
        {
            GlowingLight.enabled = false;
        }
    }

    void PitfallFunction ()
    {
        
    }

    void TimedGlowFunction ()
    {
        if(time < timeInterval)
        {
            glowSwitch = GlowSwitch.ON;
        }
        else
        {
            glowSwitch = GlowSwitch.OFF;
        }
    }

    void IntervalGlowFunction ()
    {
        if (time < timeInterval)
        {
            glowSwitch = GlowSwitch.ON;
        }
        else
        {
            glowSwitch = GlowSwitch.OFF;
        }

        if(time >= timeInterval * 2.0f)
        {
            time = 0.0f;
        }
    }
}
