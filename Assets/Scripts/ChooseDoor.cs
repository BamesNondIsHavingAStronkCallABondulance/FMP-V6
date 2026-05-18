using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseDoor : MonoBehaviour //IPointerClickHandler
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject bossDoor;

    public GameObject leftCombatImage, leftBossImage, leftRestImage, leftEventImage, rightCombatImage, rightBossImage, rightRestImage, rightEventImage;

    string leftDoorOption;
    int leftDoorOptionInt;

    string rightDoorOption;
    int rightDoorOptionInt;

    bool lCombatEncounter, lEventEncounter, lBossEncounter, lRestEncounter, rCombatEncounter, rEventEncounter, rBossEncounter, rRestEncounter;

    int floorNumber;

    string[] possibleEncounterStrings =
    {
        "combat", "rest", "boss" //Used to have 4th option of event
    };

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            EncounterSetsLogic();
        }

        floorNumber = PlayerPrefs.GetInt("gameFloor", 1);
        print(floorNumber);
    }

    void EncounterSetsLogic()
    {
        bool rerollEncounter = true;

        if (floorNumber == 1)
        {
            leftDoorOptionInt = Random.Range(0, possibleEncounterStrings.Length - 2);
            leftDoorOption = possibleEncounterStrings[leftDoorOptionInt];

            while (rerollEncounter)
            {
                rightDoorOptionInt = Random.Range(0, possibleEncounterStrings.Length - 2);
                if (rightDoorOptionInt != leftDoorOptionInt)
                {
                    rerollEncounter = false;
                    rightDoorOption = possibleEncounterStrings[rightDoorOptionInt];
                }
                else
                {
                    rerollEncounter = true;
                }
            }
        }

        if (floorNumber > 1 && floorNumber < 5)
        {
            leftDoorOptionInt = Random.Range(0, 2);
            leftDoorOption = possibleEncounterStrings[leftDoorOptionInt];

            while (rerollEncounter)
            {
                rightDoorOptionInt = Random.Range(0, 2);

                if (rightDoorOptionInt != leftDoorOptionInt)
                {
                    rerollEncounter = false;
                    rightDoorOption = possibleEncounterStrings[rightDoorOptionInt];
                }
                else
                {
                    rerollEncounter = true;
                }
            }

            print(rightDoorOption);
            print(rightDoorOptionInt);
        }

        if (floorNumber == 5)
        {
            lBossEncounter = true;
            rBossEncounter = true;

            leftDoorOptionInt = 2;
            leftDoorOption = possibleEncounterStrings[leftDoorOptionInt];

            rightDoorOptionInt = 2;
            rightDoorOption = possibleEncounterStrings[rightDoorOptionInt];
        }

        leftDoorSpriteLogic();
        rightDoorSpriteLogic();

        UpdateRightEncounterBools();
        UpdateLeftEncounterBools();
    }

    #region SpriteAndSceneLogic
    void leftDoorSpriteLogic()
    {
        if (leftDoorOption == "combat")
        {
            leftCombatImage.SetActive(true);
            leftEventImage.SetActive(false);
            leftRestImage.SetActive(false);
            leftBossImage.SetActive(false);
        }
        if (leftDoorOption == "boss")
        {
            leftCombatImage.SetActive(false);
            leftEventImage.SetActive(false);
            leftRestImage.SetActive(false);
            leftBossImage.SetActive(true);
        }
        if (leftDoorOption == "rest")
        {
            leftCombatImage.SetActive(false);
            leftEventImage.SetActive(false);
            leftRestImage.SetActive(true);
            leftBossImage.SetActive(false);
        }
        if (leftDoorOption == "event")
        {
            leftCombatImage.SetActive(false);
            leftEventImage.SetActive(true);
            leftRestImage.SetActive(false);
            leftBossImage.SetActive(false);
        }
    }

    void rightDoorSpriteLogic()
    {
        if (rightDoorOption == "combat")
        {
            rightCombatImage.SetActive(true);
            rightEventImage.SetActive(false);
            rightRestImage.SetActive(false);
            rightBossImage.SetActive(false);
        }
        if (rightDoorOption == "boss")
        {
            rightCombatImage.SetActive(false);
            rightEventImage.SetActive(false);
            rightRestImage.SetActive(false);
            rightBossImage.SetActive(true);
        }
        if (rightDoorOption == "rest")
        {
            rightCombatImage.SetActive(false);
            rightEventImage.SetActive(false);
            rightRestImage.SetActive(true);
            rightBossImage.SetActive(false);
        }
        if (rightDoorOption == "event")
        {
            rightCombatImage.SetActive(false);
            rightEventImage.SetActive(true);
            rightRestImage.SetActive(false);
            rightBossImage.SetActive(false);
        }
    }

    public void UpdateLeftScene()
    {
        if (lCombatEncounter)
        {
            SceneManager.LoadScene(1);
        }
        if (lRestEncounter)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void UpdateRightScene()
    {
        if (rCombatEncounter)
        {
            SceneManager.LoadScene(1);
        }
        if (rRestEncounter)
        {
            SceneManager.LoadScene(3);
        }
    }

    public void UpdateLeftEncounterBools()
    {
        if (leftDoorOption == "combat")
        {
            lCombatEncounter = true;
            lBossEncounter = false;
            lRestEncounter = false;
            lEventEncounter = false;
        }
        if (leftDoorOption == "boss")
        {
            lCombatEncounter = false;
            lBossEncounter = true;
            lRestEncounter = false;
            lEventEncounter = false;
        }
        if (leftDoorOption == "rest")
        {
            lCombatEncounter = false;
            lBossEncounter = false;
            lRestEncounter = true;
            lEventEncounter = false;
        }
        if (leftDoorOption == "event")
        {
            lCombatEncounter = false;
            lBossEncounter = false;
            lRestEncounter = false;
            lEventEncounter = true;
        }
    }

    public void UpdateRightEncounterBools()
    {
        if (rightDoorOption == "combat")
        {
            rCombatEncounter = true;
            rBossEncounter = false;
            rRestEncounter = false;
            rEventEncounter = false;
        }
        if (rightDoorOption == "boss")
        {
            rCombatEncounter = false;
            rBossEncounter = true;
            rRestEncounter = false;
            rEventEncounter = false;
        }
        if (rightDoorOption == "rest")
        {
            rCombatEncounter = false;
            rBossEncounter = false;
            rRestEncounter = true;
            rEventEncounter = false;
        }
        if (rightDoorOption == "event")
        {
            rCombatEncounter = false;
            rBossEncounter = false;
            rRestEncounter = false;
            rEventEncounter = true;
        }
    }
    #endregion 
}
