using UnityEngine;
using HarryGame;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class CombatScript : MonoBehaviour
{
    //public const string PLAYER_HEALTH = "Player Health";
    //public const string PLAYER_DEFENCE = "Player Defence";

    public EventSystem eventSystem;

    public GameObject endRewardsPopUp;

    public TMP_Text energyText;
    public TMP_Text enemyHealthText;
    public TMP_Text enemyDefenceText;
    public TMP_Text enemyAttackText;

    public GameObject deathCultistGO, evilWizardGO, ghostKnightGO;

    public EnemyIndex currentEnemy;
    public EnemyIndex deathCultist;
    public EnemyIndex evilWizard;
    public EnemyIndex ghostKnight;
    public TMP_Text playerHealthText;
    public TMP_Text playerDefenceText;
    public TMP_Text turnTrackerText;

    public int playerHealth;
    int playerDefence;

    public int cardsPerTurn;
    int cardsPlayed = 0;
    float eventDelay = 0.75f;

    int currentAttack;
    int currentDefend;

    bool resetEvent;

    int playerCardDamage, playerCardDefence;

    //bool enemyTurn;
    bool playerTurn;
    public bool playerActionTaken;
    bool dontSkip = false;
    bool turnStart = true;

    string enemyToSpawn;

    public PlayCards playCardsScript;
    public HandManager handManagerScript;
    public PlayerManager playerManager;

    bool enemyIsDead;

    private void Start()
    {
        playerHealth = PlayerPrefs.GetInt("playerHealth");
        cardsPerTurn = PlayerPrefs.GetInt("playerEnergy");

        playerDefence = 0;

        ResetEnemies();

        SpawningNewEnemy();

        enemyHealthText.text = currentEnemy.health.ToString();
        playerHealthText.text = playerHealth.ToString();
        playerDefenceText.text = playerDefence.ToString(); 

        playerTurn = false;

    }

    private void Update()
    {
        AttackingEnemy();
        EnemyAttackLogic();
        IsEnemyDead();
        TurnTracking();

        IsPlayerDead();

        CombatEndRewards();

        PlayerPrefs.SetInt("playerHealth", playerHealth);
    }

    void IsPlayerDead()
    {
        if(playerHealth < 0)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void TurnTracking()
    {
        if (playerTurn)
        {
            turnTrackerText.text = ("Player Turn");
        }
        else
        {
            turnTrackerText.text = ("Enemy Turn");
            print("anemone");
        }
    }


    #region Spawning random enemy
    //Some of this may be in start?
    //Ignore until combat is finished

    //This needs to affect currentEnemy

    public void SpawningNewEnemy()
    {
        int spawnInt = Random.Range(0, 2); //What enemy spawns.  GET ANOTHER ENEMY, MAKE GHOST KNIGHT A BOSS

        if (PlayerPrefs.GetInt("gameFloor") == 5)
        {
            spawnInt = 2;
        }

        if (PlayerPrefs.GetInt("gameFloor") == 1)
        {
            spawnInt = 0;
        }

            if (spawnInt == 0)
        {
            currentEnemy.health = deathCultist.health;

            currentEnemy.attack1 = deathCultist.attack1;
            currentEnemy.attack2 = deathCultist.attack2;
            currentEnemy.attack3 = deathCultist.attack3;
            currentEnemy.attack4 = deathCultist.attack4;

            currentEnemy.defend1 = deathCultist.defend1;
            currentEnemy.defend2 = deathCultist.defend2;
            currentEnemy.defend3 = deathCultist.defend3;
            currentEnemy.defend4 = deathCultist.defend4;
             
            deathCultistGO.SetActive(true);

            SpawnDeathCultist();
        }
        if (spawnInt == 1)
        {
            currentEnemy.health = evilWizard.health;

            currentEnemy.attack1 = evilWizard.attack1;
            currentEnemy.attack2 = evilWizard.attack2;
            currentEnemy.attack3 = evilWizard.attack3;
            currentEnemy.attack4 = evilWizard.attack4;

            currentEnemy.defend1 = evilWizard.defend1;
            currentEnemy.defend2 = evilWizard.defend2;
            currentEnemy.defend3 = evilWizard.defend3;
            currentEnemy.defend4 = evilWizard.defend4;

            evilWizardGO.SetActive(true);

            SpawnEvilWizard();
        }
        if (spawnInt == 2)
        {
            currentEnemy.health = ghostKnight.health;

            currentEnemy.attack1 = ghostKnight.attack1;
            currentEnemy.attack2 = ghostKnight.attack2;
            currentEnemy.attack3 = ghostKnight.attack3;
            currentEnemy.attack4 = ghostKnight.attack4;

            currentEnemy.defend1 = ghostKnight.defend1;
            currentEnemy.defend2 = ghostKnight.defend2;
            currentEnemy.defend3 = ghostKnight.defend3;
            currentEnemy.defend4 = ghostKnight.defend4;

            ghostKnightGO.SetActive(true);

            SpawnGhostKnight();
        }
    }


    void SpawnDeathCultist()
    {
        currentEnemy.health = deathCultist.health;
        currentEnemy.enemySprite = deathCultist.enemySprite;
    }
    void SpawnGhostKnight()
    {
        currentEnemy.health = ghostKnight.health;
        currentEnemy.enemySprite = ghostKnight.enemySprite;
    }
    void SpawnEvilWizard()
    {
        currentEnemy.health = evilWizard.health;
        currentEnemy.enemySprite = evilWizard.enemySprite;
    }



    public void ResetEnemies() //reset each scriptable object
    {
        deathCultist.health = 25;
        ghostKnight.health = 60;
        evilWizard.health = 20;
    }

    #endregion

    #region Attacking enemy

    public void AttackingEnemy()
    {

        if (playerTurn)
        {
            if (turnStart)
            {
                playerActionTaken = false;
                turnStart = false;

                handManagerScript.RedrawCards();
                handManagerScript.UpdateCard1();
                handManagerScript.UpdateCard2();
                handManagerScript.UpdateCard3();

                eventDelay = 0.75f;
                cardsPlayed = 0;
                resetEvent = false;
            }

            if (cardsPlayed < cardsPerTurn)
            {
                if (resetEvent)
                {
                    if (eventDelay >= 0)
                    {
                        eventSystem.enabled = false;
                        eventDelay -= Time.deltaTime;

                        print(eventDelay);
                    }
                    if (eventDelay < 0)
                    {
                        eventSystem.enabled = true;
                        eventDelay = 0.75f;

                        resetEvent = false;
                    }
                }
                else if(playCardsScript.enemyIsSelected)
                {
                    if (playCardsScript.card1Selected && playCardsScript.cardIsPlayed)
                    {
                        Card1Logic();
                    }
                    if (playCardsScript.card2Selected && playCardsScript.cardIsPlayed)
                    {
                        Card2Logic();
                    }
                    if (playCardsScript.card3Selected && playCardsScript.cardIsPlayed)
                    {
                        Card3Logic();
                    }
                }
            }
            // else disable event system for 0.75 seconds and have text pop up saying you cannot play more cards this turn
            else
            {
                resetEvent = true;

                handManagerScript.card1Image.color = Color.gray;
                handManagerScript.card2Image.color = Color.gray;
                handManagerScript.card3Image.color = Color.gray;

            }
            energyText.text = (cardsPerTurn - cardsPlayed).ToString();

            enemyHealthText.text = currentEnemy.health.ToString();
            enemyDefenceText.text = currentDefend.ToString();
            enemyAttackText.text = currentAttack.ToString();
        }

        void Card1Logic()
        {

            if (handManagerScript.card1Type.text == "Attack")
            {
                if (handManagerScript.card1Name.text == "Kick")
                {
                    playerCardDamage = 6;
                }
                if (handManagerScript.card1Name.text == "Punch")
                {
                    playerCardDamage = 4;
                }
                if (handManagerScript.card1Name.text == "Fire Bolt")
                {
                    playerCardDamage = 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }

            if (handManagerScript.card1Type.text == "Skill")
            {
                if (handManagerScript.card1Name.text == "Blood Pact")
                {
                    playerHealth -= 1;
                    playerHealthText.text = playerHealth.ToString();

                    playerDefence += 11;
                }
                if (handManagerScript.card1Name.text == "Dive")
                {
                    playerDefence += 6;
                }
                if (handManagerScript.card1Name.text == "Dodge")
                {
                    playerDefence += 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }


                int accountForEnemyShield = currentDefend - playerCardDamage;

                if (accountForEnemyShield < 0)
                {
                    currentEnemy.health += accountForEnemyShield;
                    currentDefend = 0;
                }
                else
                {
                    currentDefend -= playerCardDamage;
                }

            playerDefenceText.text = playerDefence.ToString();
        }

        void Card2Logic()
        {

            if (handManagerScript.card2Type.text == "Attack")
            {
                if (handManagerScript.card2Name.text == "Kick")
                {
                    playerCardDamage = 6;
                }
                if (handManagerScript.card2Name.text == "Punch")
                {
                    playerCardDamage = 4;
                }
                if (handManagerScript.card2Name.text == "Fire Bolt")
                {
                    playerCardDamage = 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }

            if (handManagerScript.card2Type.text == "Skill")
            {
                if (handManagerScript.card2Name.text == "Blood Pact")
                {
                    playerHealth -= 1;
                    playerHealthText.text = playerHealth.ToString();

                    playerDefence += 11;
                }
                if (handManagerScript.card2Name.text == "Dive")
                {
                    playerDefence += 6;
                }
                if (handManagerScript.card2Name.text == "Dodge")
                {
                    playerDefence += 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }


            int accountForEnemyShield = currentDefend - playerCardDamage;

            if (accountForEnemyShield < 0)
            {
                currentEnemy.health += accountForEnemyShield;
                currentDefend = 0;
            }
            else
            {
                currentDefend -= playerCardDamage;
            }

            playerDefenceText.text = playerDefence.ToString();
        }

        void Card3Logic()
        {
            if (handManagerScript.card3Type.text == "Attack")
            {
                if (handManagerScript.card3Name.text == "Kick")
                {
                    playerCardDamage = 6;
                }
                if (handManagerScript.card3Name.text == "Punch")
                {
                    playerCardDamage = 4;
                }
                if (handManagerScript.card3Name.text == "Fire Bolt")
                {
                    playerCardDamage = 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }

            if (handManagerScript.card3Type.text == "Skill")
            {
                if (handManagerScript.card3Name.text == "Blood Pact")
                {
                    playerHealth -= 1;
                    playerHealthText.text = playerHealth.ToString();

                    playerDefence += 11;
                }
                if (handManagerScript.card3Name.text == "Dive")
                {
                    playerDefence += 6;
                }
                if (handManagerScript.card3Name.text == "Dodge")
                {
                    playerDefence += 4;
                }

                cardsPlayed++;
                playerActionTaken = true;
            }


            int accountForEnemyShield = currentDefend - playerCardDamage;

            if (accountForEnemyShield < 0)
            {
                currentEnemy.health += accountForEnemyShield;
                currentDefend = 0;
            }
            else
            {
                currentDefend -= playerCardDamage;
            }

            playerDefenceText.text = playerDefence.ToString();


            /*
            if (handManagerScript.card3Type.text == "Attack")
            {
                int accountForEnemyShield = currentDefend - handManagerScript.cardData3.damage;

                if (accountForEnemyShield < 0)
                {
                    currentEnemy.health += accountForEnemyShield;
                    currentDefend = 0;
                }
                else
                {
                    currentDefend -= handManagerScript.cardData3.damage;
                }

                playerActionTaken = true;
            }

            if (handManagerScript.card3Type.text == "Skill")
            {
                playerDefence += handManagerScript.cardData3.block;
                playerDefenceText.text = playerDefence.ToString();

                playerActionTaken = true;
            }*/
        }

        IsEnemyDead();
    }

    public void IsEnemyDead()
    {
        if (currentEnemy.health <= 0)
        {
            enemyIsDead = true;
        }
    }

    public void EndPlayerTurn()
    {
        playerTurn = false;
        turnStart = true;
    }

    #endregion

    #region Enemy attacks

    public void EnemyAttackLogic()
    {

        if (!playerTurn)
        {
            if (dontSkip)
            {
                int accountForShield = playerDefence - currentAttack;

                if (accountForShield < 0)
                {
                    playerHealth += accountForShield;
                }
                else
                {
                    playerDefence -= currentAttack;
                }
                playerDefence = 0;

                playerHealthText.text = playerHealth.ToString();
                playerDefenceText.text = playerDefence.ToString();

                dontSkip = false;
            }


            enemyDefenceText.text = "0";

            int[] possibleEnemyAttacks =
            {
            currentEnemy.attack1, currentEnemy.attack2, currentEnemy.attack3, currentEnemy.attack4
            };

            int[] possibleEnemyDefends =
            {
            currentEnemy.defend1, currentEnemy.defend2, currentEnemy.defend3, currentEnemy.defend4
            };

            bool validAttack = false;
            bool validDefend = false;
            bool validAction = false;

            while (!validAction)
            {
                int currentAttackSelection = Random.Range(0, possibleEnemyAttacks.Length);
                currentAttack = possibleEnemyAttacks[currentAttackSelection];

                int currentDefendSelection = Random.Range(0, possibleEnemyAttacks.Length);
                currentDefend = possibleEnemyAttacks[currentDefendSelection];

                if (currentAttack != 0)
                {
                    validAttack = true;
                }
                if (currentDefend != 0)
                {
                    validDefend = true;
                }

                if (validAttack || validDefend)
                {
                    validAction = true;
                }
            }

            if (!dontSkip)
            {
                playerTurn = true;
            }

            dontSkip = true;

            // enemyDefenceText.text = currentDefend.ToString();
            // enemyAttackText.text = currentAttack.ToString();
        }

        //Goal is to randomly select 1 attack and one defnd from each array
        //If both options are null, select again until at least one option is not null;
        //COMPLETED

    }

    IEnumerator EnemyDelay()
    {
        print("before");

        yield return new WaitForSeconds(3);

        print("after");

        playerTurn = true;
    }

    #endregion

    #region Rewards

    private void CombatEndRewards()
    {
        if (enemyIsDead)
        {
            if ((PlayerPrefs.GetInt("gameFloor") >= 5))
            {
                print("YAY");
                SceneManager.LoadScene(4);
            }
            else
            {
                PlayerPrefs.SetInt("playerEnergy", (PlayerPrefs.GetInt("playerEnergy") + 1));
                PlayerPrefs.SetInt("gameFloor", (PlayerPrefs.GetInt("gameFloor") + 1));

                SceneManager.LoadScene(2);
            }
        }
    }
    #endregion

    #region Go to next screen

    void GoToNextScene()
    {
        
    }


    #endregion
}
