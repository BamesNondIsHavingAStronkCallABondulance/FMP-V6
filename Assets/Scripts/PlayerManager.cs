using UnityEngine;

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

        if (PlayerPrefs.GetInt("playerHealth") != 30)
        {
            PlayerPrefs.SetInt("playerHealth", 30);
        }
        else
        {
            combatScript.playerHealth = PlayerPrefs.GetInt("playerHealth");
        }
        
        PlayerPrefs.SetInt("gameFloor", 1);

        if (PlayerPrefs.GetInt("playerEnergy") != 1)
        {
            PlayerPrefs.SetInt("playerEnergy", 1);
            print(PlayerPrefs.GetInt("playerEnergy"));
        }
    }

    private void Update()
    {
        //PlayerPrefs.SetInt("playerHealth", combatScript.playerHealth);
    }
}
