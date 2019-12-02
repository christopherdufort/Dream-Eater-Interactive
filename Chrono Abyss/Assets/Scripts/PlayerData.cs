/**
 * DataStructure to store persisted player data.
 * These are the adjustable stats that users can increment
 * Each stat affects an element of gameplay.
 */
public class PlayerData
{
    // ---- Active Stats ----

    // Player Level = Total Stats - Starting stats
    public int PlayerLevel { get; set; }
    // Affects Player Health pool
    public int Vitality { get; set; }
    // Affects the Player ammo count
    public int Attunement { get; set; }
    // Affects the players movement speed
    public int Agility { get; set; }
    // Affects the players melee weapon damage
    public int Strength { get; set; }
    // Affects the Players ranged weapon damage
    public int Dexterity { get; set; }
    // Affects the players ranged projectile distance
    public int Skill { get; set; }
    // Affects the duration of players powerup use time length
    public int Intelligence { get; set; }
    // Affects the drop rate of gold and health
    public int Luck { get; set; }

    // ---- Stats to be implemented (maybe) ----
    // Affects the enemy and hazard spawn rates reducing your enemy count
    public int Faith { get; set; }
    // Affects melee distance range increasing the swords swing distance
    public int Vigor { get; set; }
    // Affects the players resistance to damage, allowing them to take more damage.
    public int Resistance { get; set; }
    // Affects the amount of powerups that can be held at the same time.
    public int Endurance { get; set; }
}