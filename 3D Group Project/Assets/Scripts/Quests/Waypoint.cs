using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI distanceText;

    private void Update()
    {
        if(target != null)
        {
            if (!img.enabled)
            {
                img.enabled = true;
                distanceText.enabled = true;
            }
                

            float minX = img.GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;

            float minY = img.GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

            if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
            {
                //Target is behind the player.
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            img.transform.position = pos;
            distanceText.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
        }
        else
        {
            img.enabled = false;
            distanceText.enabled = false;
        }
        
    }
}
