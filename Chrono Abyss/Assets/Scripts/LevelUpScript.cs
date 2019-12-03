using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpScript : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawnPosition = new Vector3(500.0f, 500.0f, 500.0f);
    private GameObject player;
    private GameObject gameController;

    //----- UI ELEMENTS -----

    private Text totalCoinCount;
    private int totalGold;
    private Text totalLevelCount;
    private int totalLevel;

    private Text vitalityLevel;
    private int vitality;
    private Text nextVitalityLevel;

    private Text ammoLevel;
    private int attunement;
    private Text nextAmmoLevel;

    private Text agilityLevel;
    private int agility;
    private Text nextAgilityLevel;

    private Text strengthLevel;
    private int strength;
    private Text nextStrengthLevel;

    private Text dexterityLevel;
    private int dexterity;
    private Text nextDexterityLevel;

    private Text skillLevel;
    private int skill;
    private Text nextSkillLevel;

    private Text intelligenceLevel;
    private int intelligence;
    private Text nextIntelligenceLevel;

    private Text luckLevel;
    private int luck;
    private Text nextLuckLevel;

    private Text faithLevel;
    private int faith;
    private Text nextFaithLevel;

    private Text vigorLevel;
    private int vigor;
    private Text nextVigorLevel;

    private Text resistanceLevel;
    private int resistance;
    private Text nextResistanceLevel;

    private Text enduranceLevel;
    private int endurance;
    private Text nextEnduranceLevel;


    //----- END UI ELEMENTS -----

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CollectPlayerForLevelUp");

        // Get references to all the text fields - must be called from Awake or Enabled
        totalCoinCount = GameObject.Find("TotalCoinsText").GetComponent<Text>();
        totalLevelCount = GameObject.Find("TotalLevelText").GetComponent<Text>();
        vitalityLevel = GameObject.Find("VitalityLevelText").GetComponent<Text>();
        nextVitalityLevel = GameObject.Find("NextVitalityLevelCostText").GetComponent<Text>();
        ammoLevel = GameObject.Find("AmmoLevelText").GetComponent<Text>();
        nextAmmoLevel = GameObject.Find("NextAmmoLevelCostText").GetComponent<Text>();
        agilityLevel = GameObject.Find("AgilityLevelText").GetComponent<Text>();
        nextAgilityLevel = GameObject.Find("NextAgilityLevelCostText").GetComponent<Text>();
        strengthLevel = GameObject.Find("StrengthLevelText").GetComponent<Text>();
        nextStrengthLevel = GameObject.Find("NextStrengthLevelCostText").GetComponent<Text>();
        dexterityLevel = GameObject.Find("DexterityLevelText").GetComponent<Text>();
        nextDexterityLevel = GameObject.Find("NextDexterityLevelCostText").GetComponent<Text>();
        skillLevel = GameObject.Find("SkillLevelText").GetComponent<Text>();
        nextSkillLevel = GameObject.Find("NextSkillLevelCostText").GetComponent<Text>();
        intelligenceLevel = GameObject.Find("IntelligenceLevelText").GetComponent<Text>();
        nextIntelligenceLevel = GameObject.Find("NextIntelligenceLevelCostText").GetComponent<Text>();
        luckLevel = GameObject.Find("LuckLevelText").GetComponent<Text>();
        nextLuckLevel = GameObject.Find("NextLuckLevelCostText").GetComponent<Text>();
        faithLevel = GameObject.Find("FaithLevelText").GetComponent<Text>();
        nextFaithLevel = GameObject.Find("NextFaithLevelCostText").GetComponent<Text>();
        vigorLevel = GameObject.Find("VigorLevelText").GetComponent<Text>();
        nextVigorLevel = GameObject.Find("NextVigorLevelCostText").GetComponent<Text>();
        resistanceLevel = GameObject.Find("ResistanceLevelText").GetComponent<Text>();
        nextResistanceLevel = GameObject.Find("NextResistanceLevelCostText").GetComponent<Text>();
        enduranceLevel = GameObject.Find("EnduranceLevelText").GetComponent<Text>();
        nextEnduranceLevel = GameObject.Find("NextEnduranceLevelCostText").GetComponent<Text>();

        LoadPlayerData();

        totalLevelCount.text = totalLevel.ToString();
        totalCoinCount.text = totalGold.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CollectPlayerForLevelUp()
    {
        // bring over player
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPosition;
    }

    public void LoadPlayerData()
    {
        totalGold = player.GetComponent<PlayerController>().goldCollected;

        totalLevel = gameController.GetComponent<GameController>().playerData.PlayerLevel;
        vitality = gameController.GetComponent<GameController>().playerData.Vitality;
        attunement = gameController.GetComponent<GameController>().playerData.Attunement;
        agility = gameController.GetComponent<GameController>().playerData.Agility;
        strength = gameController.GetComponent<GameController>().playerData.Strength;
        dexterity = gameController.GetComponent<GameController>().playerData.Dexterity;
        skill = gameController.GetComponent<GameController>().playerData.Skill;
        intelligence = gameController.GetComponent<GameController>().playerData.Intelligence;
        luck = gameController.GetComponent<GameController>().playerData.Luck;
        faith = gameController.GetComponent<GameController>().playerData.Faith;
        vigor = gameController.GetComponent<GameController>().playerData.Vigor;
        resistance = gameController.GetComponent<GameController>().playerData.Resistance;
        endurance = gameController.GetComponent<GameController>().playerData.Endurance;
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Clicked");
        SaveGame();

        string randomFloorScene = ((FloorTheme)Random.Range(0, 4)).ToString() + "Floor";
        SceneManager.LoadScene(randomFloorScene);
        //May Need to bring player into the scene
    }
    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
        SaveGame();
        // Close the game application (used when demoing the game)
        Application.Quit();
    }

    public void SaveGame()
    {
        // Persist changed stats to playerData;
        player.GetComponent<PlayerController>().goldCollected = totalGold;

        gameController.GetComponent<GameController>().playerData.PlayerLevel = totalLevel;
        gameController.GetComponent<GameController>().playerData.Vitality = vitality;
        gameController.GetComponent<GameController>().playerData.Attunement = attunement;
        gameController.GetComponent<GameController>().playerData.Agility = agility;
        gameController.GetComponent<GameController>().playerData.Strength = strength;
        gameController.GetComponent<GameController>().playerData.Dexterity = dexterity;
        gameController.GetComponent<GameController>().playerData.Skill = skill;
        gameController.GetComponent<GameController>().playerData.Intelligence = intelligence;
        gameController.GetComponent<GameController>().playerData.Luck = luck;
        gameController.GetComponent<GameController>().playerData.Faith = faith;
        gameController.GetComponent<GameController>().playerData.Vigor = vigor;
        gameController.GetComponent<GameController>().playerData.Resistance = resistance;
        gameController.GetComponent<GameController>().playerData.Endurance = endurance;

        // Persist changed player data to storage;
        PlayerPersistence.SaveData(gameController.GetComponent<GameController>().playerData);
    }

    public void LevelUpVitality()
    {
        Debug.Log("Vitality Clicked");
        if (totalGold >= ((vitality + 1) * 10))
        {
            vitality++;
            totalLevel++;
            totalGold -= ((vitality + 1) * 10);
            vitalityLevel.text = vitality.ToString();
            nextVitalityLevel.text = (((vitality + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
}
    public void LevelUpAttunement()
    {
        Debug.Log("Attunement Clicked");
        if (totalGold >= ((attunement + 1) * 10))
        {
            attunement++;
            totalLevel++;
            totalGold -= ((attunement + 1) * 10);
            ammoLevel.text = attunement.ToString();
            nextAmmoLevel.text = (((attunement + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpAgaility()
    {
        Debug.Log("Agility Clicked");
        if (totalGold >= ((agility + 1) * 10))
        {
            agility++;
            totalLevel++;
            totalGold -= ((agility + 1) * 10);
            agilityLevel.text = agility.ToString();
            nextAgilityLevel.text = (((agility + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpStrength()
    {
        Debug.Log("Strength Clicked");
        if (totalGold >= ((strength + 1) * 10))
        {
            strength++;
            totalLevel++;
            totalGold -= ((strength + 1) * 10);
            strengthLevel.text = strength.ToString();
            nextStrengthLevel.text = (((strength + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpDexterity()
    {
        Debug.Log("Dexterity Clicked");
        if (totalGold >= ((dexterity + 1) * 10))
        {
            dexterity++;
            totalLevel++;
            totalGold -= ((dexterity + 1) * 10);
            dexterityLevel.text = dexterity.ToString();
            nextDexterityLevel.text = (((dexterity + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpSkill()
    {
        Debug.Log("Skill Clicked");
        if (totalGold >= ((skill + 1) * 10))
        {
            skill++;
            totalLevel++;
            totalGold -= ((skill + 1) * 10);
            skillLevel.text = skill.ToString();
            nextSkillLevel.text = (((skill + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpIntelligence()
    {
        Debug.Log("Intelligence Clicked");
        if (totalGold >= ((intelligence + 1) * 10))
        {
            intelligence++;
            totalLevel++;
            totalGold -= ((intelligence + 1) * 10);
            intelligenceLevel.text = intelligence.ToString();
            nextIntelligenceLevel.text = (((intelligence + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpLuck()
    {
        Debug.Log("Luck Clicked");
        if (totalGold >= ((luck + 1) * 10))
        {
            luck++;
            totalLevel++;
            totalGold -= ((luck + 1) * 10);
            luckLevel.text = luck.ToString();
            nextLuckLevel.text = (((luck + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpFaith()
    {
        Debug.Log("Faith Clicked");
        if (totalGold >= ((faith + 1) * 10))
        {
            luck++;
            totalLevel++;
            totalGold -= ((faith + 1) * 10);
            faithLevel.text = faith.ToString();
            nextFaithLevel.text = (((faith + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpVigor()
    {
        Debug.Log("Vigor Clicked");
        if (totalGold >= ((vigor + 1) * 10))
        {
            vigor++;
            totalLevel++;
            totalGold -= ((vigor + 1) * 10);
            vigorLevel.text = vigor.ToString();
            nextVigorLevel.text = (((vigor + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpResistance()
    {
        Debug.Log("Resistance Clicked");
        if (totalGold >= ((resistance + 1) * 10))
        {
            resistance++;
            totalLevel++;
            totalGold -= ((resistance + 1) * 10);
            resistanceLevel.text = resistance.ToString();
            nextResistanceLevel.text = (((resistance + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpEndurance()
    {
        Debug.Log("Endurance Clicked");
        if (totalGold >= ((endurance + 1) * 10))
        {
            endurance++;
            totalLevel++;
            totalGold -= ((endurance + 1) * 10);
            enduranceLevel.text = endurance.ToString();
            nextEnduranceLevel.text = (((endurance + 1) * 10).ToString());
            totalCoinCount.text = totalGold.ToString();
            totalLevelCount.text = totalLevel.ToString();
        }
    }
}
