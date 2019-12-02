using UnityEngine;

internal class PlayerPersistence
{
    public static PlayerData LoadData()
    {
        int playerLevel = PlayerPrefs.GetInt("playerLevel");
        int vitality = PlayerPrefs.GetInt("vitality");
        int attunement = PlayerPrefs.GetInt("attunement");
        int agility = PlayerPrefs.GetInt("agility");
        int strength = PlayerPrefs.GetInt("strength");
        int dexterity = PlayerPrefs.GetInt("dexterity");
        int skill = PlayerPrefs.GetInt("skill");
        int intelligence = PlayerPrefs.GetInt("intelligence");
        int luck = PlayerPrefs.GetInt("luck");
        int faith = PlayerPrefs.GetInt("faith");
        int vigor = PlayerPrefs.GetInt("vigor");
        int endurance = PlayerPrefs.GetInt("endurance");
        int resistance = PlayerPrefs.GetInt("resistance");

        PlayerData playerData = new PlayerData()
        {
            PlayerLevel = playerLevel,
            Vitality = vitality,
            Attunement = attunement,
            Agility = agility,
            Strength = strength,
            Dexterity = dexterity,
            Skill = skill,
            Intelligence = intelligence,
            Luck = luck,
            Faith = faith,
            Vigor = vigor,
            Endurance = endurance,
            Resistance = resistance
        };

        return playerData;
    }

    public static void SaveData(PlayerData playerData)
    {
        PlayerPrefs.SetInt("playerLevel", playerData.PlayerLevel);
        PlayerPrefs.SetInt("vitality", playerData.Vitality);
        PlayerPrefs.SetInt("attunement", playerData.Attunement);
        PlayerPrefs.SetInt("agility", playerData.Agility);
        PlayerPrefs.SetInt("strength", playerData.Strength);
        PlayerPrefs.SetInt("skill", playerData.Skill);
        PlayerPrefs.SetInt("dexterity", playerData.Dexterity);
        PlayerPrefs.SetInt("intelligence", playerData.Intelligence);
        PlayerPrefs.SetInt("luck", playerData.Luck);
        PlayerPrefs.SetInt("faith", playerData.Faith);
        PlayerPrefs.SetInt("vigor", playerData.Vigor);
        PlayerPrefs.SetInt("endurance", playerData.Endurance);
        PlayerPrefs.SetInt("resistance", playerData.Resistance);
    }
}