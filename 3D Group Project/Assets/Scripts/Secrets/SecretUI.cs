using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecretUI : MonoBehaviour
{
    public int totalSecretsCount;
    [SerializeField] Player player;

    public void updateSecretsCount()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Secrets: " + player.foundSecretsNames.Count + " / " + totalSecretsCount;
    }
}
