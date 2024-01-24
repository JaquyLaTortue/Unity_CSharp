using UnityEngine;

public abstract class Pokemon : MonoBehaviour
{
    public string PokemonName { get; protected set; }
    protected float pv;
    protected int attack;
    protected int defense;
    protected int speed;
    public bool isInPokeball { get; protected set; } = true;

    public string Type { get; protected set; }
    protected string typeAdvantage;
    protected string typeDisadvantage;

    public void Takle(Pokemon _target)
    {
        Debug.Log($"{PokemonName} attaque Charge");
        _target.pv -= attack * 0.2f;
    }

    //For each pokemon, Launch his ability
    public abstract void Ability(Pokemon _target);

    /// <summary>
    /// The pokemon take the damage modified by the type advantage or disadvantage
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_attackType"></param>
    public void TakeDamage(float _damage, string _attackType)
    {
        if (!isInPokeball)
        {
            if (_attackType == typeDisadvantage)
            {
                pv -= _damage * 2f;
            }
            else if (_attackType == typeAdvantage)
            {
                pv -= _damage * 0.5f;
            }
            else
            {
                pv -= _damage;
            }
        }
        Debug.Log($"{PokemonName} ne peut pas prendre de dégâts, il est dans sa pokeball");
    }

    /// <summary>
    /// The pokemon get out of his pokeball and can be use in the battle
    /// </summary>
    public void GetOutPokeball() { isInPokeball = false; }

    /// <summary>
    /// The pokemon get in his pokeball and can't be use in the battle
    /// </summary>
    public void GetInPokeball() { isInPokeball = true; }
}
