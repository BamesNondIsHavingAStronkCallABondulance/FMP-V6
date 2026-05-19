using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    CombatScript combatScript;

    public static PlayerManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;

        }


        /* if (PlayerPrefs.GetInt("playerHealth") != 30)
         {
             PlayerPrefs.SetInt("playerHealth", 30);
         }
         else
         {
             combatScript.playerHealth = PlayerPrefs.GetInt("playerHealth");
         }

         if (PlayerPrefs.GetInt("playerEnergy") != 1)
         {
             PlayerPrefs.SetInt("playerEnergy", 1);
             print(PlayerPrefs.GetInt("playerEnergy"));
         }*/

        PlayerPrefs.SetInt("playerHealth", 30);
        PlayerPrefs.SetInt("gameFloor", 1);
        PlayerPrefs.SetInt("playerEnergy", 1);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            PlayerPrefs.SetInt("playerHealth", 30);
            PlayerPrefs.SetInt("gameFloor", 1);
            PlayerPrefs.SetInt("playerEnergy", 1);
        }

    }
}
