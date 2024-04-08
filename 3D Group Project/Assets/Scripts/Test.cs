
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) 
        {
            GetComponent<Player>().counter++;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            GetComponent<Player>().counter--;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {

        }
    }
}
