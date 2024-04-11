using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int counter;
    public float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }
}
