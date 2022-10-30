using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("4"))
        {
            Time.timeScale = 4;
        }
        if (Input.GetKeyDown("1"))
        {
            Time.timeScale = 1;
        }
    }
}
