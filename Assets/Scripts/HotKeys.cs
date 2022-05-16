using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeys : MonoBehaviour
{
    public GameObject menu;

    private float timeScale;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("YES");
            if (!menu.active)
            {
                timeScale = Time.timeScale;
                Time.timeScale = 0;
                menu.active = true;
            }
            else
            {
                Time.timeScale = timeScale;
                menu.active = false;
            }            
        }
    }
}
