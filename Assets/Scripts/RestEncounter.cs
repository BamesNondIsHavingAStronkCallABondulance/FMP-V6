using UnityEngine;
using UnityEngine.SceneManagement;

public class RestEncounter : MonoBehaviour
{
    public void PlayerRest()
    {
        int tempInt;
        tempInt = PlayerPrefs.GetInt("playerHealth");

        tempInt += 10;

        if (tempInt > 30)
        {
            tempInt = 30;
        }

        PlayerPrefs.SetInt("playerHealth", tempInt);
    }

    public void GoToCrossroads()
    {
        SceneManager.LoadScene(2);
    }

}
