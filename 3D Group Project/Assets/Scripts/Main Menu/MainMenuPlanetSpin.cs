using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlanetSpin : MonoBehaviour
{

    void Update()
    {
        gameObject.transform.Rotate(0, 0.001f, 0);
    }
}
