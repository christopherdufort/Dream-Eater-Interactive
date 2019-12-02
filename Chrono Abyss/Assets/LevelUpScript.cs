using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Clicked");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Clicked");
    }

    //On button click check if you have enough money
    //Detract money
    //Increment Total level + individual stat
    //Upgrade button and level text

    public void LevelUpVitality()
    {
        Debug.Log("Vitality Clicked");
    }
    public void LevelUpAttunement()
    {
        Debug.Log("Attunement Clicked");
    }
    public void LevelUpAgaility()
    {
        Debug.Log("Agility Clicked");
    }
    public void LevelUpStrength()
    {
        Debug.Log("Strength Clicked");
    }
    public void LevelUpDexterity()
    {
        Debug.Log("Dexterity Clicked");
    }
    public void LevelUpSkill()
    {
        Debug.Log("Skill Clicked");
    }
    public void LevelUpIntelligence()
    {
        Debug.Log("Intelligence Clicked");
    }
    public void LevelUpLuck()
    {
        Debug.Log("Luck Clicked");
    }
    public void LevelUpFaith()
    {
        Debug.Log("Faith Clicked");
    }
    public void LevelUpVigor()
    {
        Debug.Log("Vigor Clicked");
    }
    public void LevelUpResistance()
    {
        Debug.Log("Resistance Clicked");
    }
    public void LevelUpEndurance()
    {
        Debug.Log("Endurance Clicked");
    }
}
