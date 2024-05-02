using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretsInScene : MonoBehaviour
{
    public List<GameObject> secretsInScene = new List<GameObject>();

    [SerializeField] GameObject player;

    private void Start()
    {
        foundSecrets(player.GetComponent<Player>());
    }

    public void foundSecrets(Player player)
    {
        for(int i = 0; i < secretsInScene.Count; i++)
        {
            for(int z = 0; z < player.foundSecrets.Count; z++)
            {
                if (player.foundSecrets[z].secretName.Equals(secretsInScene[i]))
                {
                    secretsInScene[i].SetActive(false);
                }
            }
        }
    }
}
