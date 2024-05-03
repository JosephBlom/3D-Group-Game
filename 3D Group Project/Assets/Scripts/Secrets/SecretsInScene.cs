using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretsInScene : MonoBehaviour
{
    public List<GameObject> secretsInScene = new List<GameObject>();

    [SerializeField] Player player;
    [SerializeField] SecretUI secretUI;

    private void Start()
    {
        foundSecrets(player);
    }

    public void foundSecrets(Player player)
    {
        for(int i = 0; i < secretsInScene.Count; i++)
        {
            for (int z = 0; z < player.foundSecretsNames.Count; z++)
            {
                if (player.foundSecretsNames[z] == secretsInScene[i].GetComponent<Secret>().secretName)
                {
                    secretsInScene[i].SetActive(false);
                }
            }
        }

        secretUI.updateSecretsCount();
    }
}
