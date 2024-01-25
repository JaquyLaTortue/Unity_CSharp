public interface IHealer
{
    /// <summary>
    /// Each healer have to set the CharacterType to "Healer" in the Start() method and have the GameScript Referenced.
    /// </summary>
    public abstract void SetUpHealerValues();

    /// <summary>
    /// Heal all the Pokemons of Eahc player.
    /// </summary>
    public abstract void HealAll();

    /// <summary>
    /// Heal The current pokemon of the player that have to play.
    /// </summary>
    public abstract void HealOne(Pokemon _target);
}
