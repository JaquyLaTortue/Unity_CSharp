
public interface  IHealer
{
    /// <summary>
    /// Each healer have to set the characterType to "Healer" in the Start() method and have the gameScript Referenced.
    /// </summary>
    public abstract void SetUpHealerValues();

    /// <summary>
    /// Heal all the pokemons of Eahc player.
    /// </summary>
    public abstract void HealAll();

    /// <summary>
    /// Heal The current pokemon of the player that have to play.
    /// </summary>
    public abstract void HealOne();
}
