using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpScript : MonoBehaviour
{
    private GameObject player;
    private GameObject gameController;
    private GameObject inGameUI;

    //----- UI ELEMENTS and Local Vars -----

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
    //----- END UI ELEMENTS and Local Vars-----

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("LevelUpScript Very Start");
        // Remove the player and UI from the scene as they are uneeded
        player = GameObject.FindGameObjectWithTag("Player");
        inGameUI = GameObject.FindGameObjectWithTag("InGameUI");
        Destroy(player);
        Destroy(inGameUI);

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

        gameController = GameObject.Find("GameController");
        if (gameController != null)
        {
            LoadPlayerData();
        }
        else
        {
            throw new System.Exception("Can't find the GameController logic can not continue...");
        }
		
		Debug.Log("LevelUpScript After");	
    }
    
    // Update is called once per frame
    void Update()
    {
        //totalGold = gameController.GetComponent<GameController>().goldCollected;
        GameObject.Find("TotalCoinsText").GetComponent<Text>().text = totalGold.ToString();
    }

    public void LoadPlayerData()
    {
        // Update the local variables based on content retrieved from gamecontroller
        totalGold = gameController.GetComponent<GameController>().goldCollected;
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

        // Update the level up UI
        totalLevelCount.text = totalLevel.ToString();

        vitalityLevel.text = vitality.ToString();
        nextVitalityLevel.text = (((vitality + 1) * 10).ToString() + "g");

        ammoLevel.text = attunement.ToString();
        nextAmmoLevel.text = (((attunement + 1) * 10).ToString() + "g");

        agilityLevel.text = agility.ToString();
        nextAgilityLevel.text = (((agility + 1) * 10).ToString() + "g");

        strengthLevel.text = strength.ToString();
        nextStrengthLevel.text = (((strength + 1) * 10).ToString() + "g");

        dexterityLevel.text = dexterity.ToString();
        nextDexterityLevel.text = (((dexterity + 1) * 10).ToString() + "g");

        skillLevel.text = skill.ToString();
        nextSkillLevel.text = (((skill + 1) * 10).ToString() + "g");

        intelligenceLevel.text = intelligence.ToString();
        nextIntelligenceLevel.text = (((intelligence + 1) * 10).ToString() + "g");

        luckLevel.text = luck.ToString();
        nextLuckLevel.text = (((luck + 1) * 10).ToString() + "g");

        faithLevel.text = faith.ToString();
        nextFaithLevel.text = (((faith + 1) * 10).ToString() + "g");

        vigorLevel.text = vigor.ToString();
        nextVigorLevel.text = (((vigor + 1) * 10).ToString() + "g");

        resistanceLevel.text = resistance.ToString();
        nextResistanceLevel.text = (((resistance + 1) * 10).ToString() + "g");

        enduranceLevel.text = endurance.ToString();
        nextEnduranceLevel.text = (((endurance + 1) * 10).ToString() + "g");
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Clicked");
        SaveGame();
        gameController.GetComponent<GameController>().setLevel(gameController.GetComponent<GameController>().getLevel() + 1);

        string randomFloorScene = ((FloorTheme)Random.Range(0, 4)).ToString() + "Floor";
        FindObjectOfType<AudioManager>().StopCurrent();
		SceneManager.LoadScene(randomFloorScene);
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
        // Carry remaining gold into next level
        gameController.GetComponent<GameController>().goldCollected = totalGold;
        // Persist changed stats to playerData;
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

    //TODO all of the following should be refactored into a single function where you pass in the stat name..
    public void LevelUpVitality()
    {
        Debug.Log("Vitality Clicked");
        if (totalGold >= ((vitality+1) * 10))
        {
            Debug.Log("Vitality Increased");
            totalGold -= ((vitality + 1) * 10); // Take gold first
            vitality++;
            totalLevel++;
            vitalityLevel.text = vitality.ToString();
            nextVitalityLevel.text = (((vitality + 1) * 10).ToString()+"g");
            totalLevelCount.text = totalLevel.ToString();
        }
}
    public void LevelUpAttunement()
    {
        Debug.Log("Attunement/Ammo Clicked");
        if (totalGold >= ((attunement+1) * 10))
        {
            Debug.Log("Attunement/Ammo Increased");
            totalGold -= ((attunement + 1) * 10);
            attunement++;
            totalLevel++;
            ammoLevel.text = attunement.ToString();
            nextAmmoLevel.text = (((attunement + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpAgaility()
    {
        Debug.Log("Agility Clicked");
        if (totalGold >= ((agility+1) * 10))
        {
            Debug.Log("Agility Increased");
            totalGold -= ((agility + 1) * 10);
            agility++;
            totalLevel++;
            agilityLevel.text = agility.ToString();
            nextAgilityLevel.text = (((agility + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpStrength()
    {
        Debug.Log("Strength Clicked");
        if (totalGold >= ((strength+1) * 10))
        {
            Debug.Log("Strength Increased");
            totalGold -= ((strength + 1) * 10);
            strength++;
            totalLevel++;
            strengthLevel.text = strength.ToString();
            nextStrengthLevel.text = (((strength + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpDexterity()
    {
        Debug.Log("Dexterity Clicked");
        if (totalGold >= ((dexterity + 1) * 10))
        {
            Debug.Log("Dexterity increased");
            totalGold -= ((dexterity + 1) * 10);
            dexterity++;
            totalLevel++;
            dexterityLevel.text = dexterity.ToString();
            nextDexterityLevel.text = (((dexterity+1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpSkill()
    {
        Debug.Log("Skill Clicked");
        if (totalGold >= ((skill + 1) * 10))
        {
            Debug.Log("Skill Increased");
            totalGold -= ((skill + 1) * 10);
            skill++;
            totalLevel++;
            skillLevel.text = skill.ToString();
            nextSkillLevel.text = (((skill + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpIntelligence()
    {
        Debug.Log("PowerUps/Intelligence Clicked");
        if (totalGold >= ((intelligence + 1) * 10))
        {
            Debug.Log("Powers/Intelligence Increased");
            totalGold -= ((intelligence + 1) * 10);
            intelligence++;
            totalLevel++;
            intelligenceLevel.text = intelligence.ToString();
            nextIntelligenceLevel.text = (((intelligence + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpLuck()
    {
        Debug.Log("Luck Clicked");
        if (totalGold >= ((luck + 1) * 10))
        {
            Debug.Log("Luck Increased");
            totalGold -= ((luck + 1) * 10);
            luck++;
            totalLevel++;
            luckLevel.text = luck.ToString();
            nextLuckLevel.text = (((luck + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpFaith()
    {
        Debug.Log("Faith Clicked");
        if (totalGold >= ((faith + 1) * 10))
        {
            Debug.Log("Faith Increased");
            totalGold -= ((faith + 1) * 10);
            luck++;
            totalLevel++;
            faithLevel.text = faith.ToString();
            nextFaithLevel.text = (((faith + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpVigor()
    {
        Debug.Log("Vigor Clicked");
        if (totalGold >= ((vigor + 1) * 10))
        {
            Debug.Log("Vigor Increased");
            totalGold -= ((vigor + 1) * 10);
            vigor++;
            totalLevel++;
            vigorLevel.text = vigor.ToString();
            nextVigorLevel.text = (((vigor + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpResistance()
    {
        Debug.Log("Resistance Clicked");
        if (totalGold >= ((resistance + 1) * 10))
        {
            Debug.Log("Resistance Increased");
            totalGold -= ((resistance + 1) * 10);
            resistance++;
            totalLevel++;
            resistanceLevel.text = resistance.ToString();
            nextResistanceLevel.text = (((resistance + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
    public void LevelUpEndurance()
    {
        Debug.Log("Endurance Clicked");
        if (totalGold >= ((endurance + 1) * 10))
        {
            Debug.Log("Endurance Increased");
            totalGold -= ((endurance + 1) * 10);
            endurance++;
            totalLevel++;
            enduranceLevel.text = endurance.ToString();
            nextEnduranceLevel.text = (((endurance + 1) * 10).ToString() + "g");
            totalLevelCount.text = totalLevel.ToString();
        }
    }
}
