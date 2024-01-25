using UnityEngine;

public abstract class Pokemon : MonoBehaviour
{
    public string PokemonName { get; protected set; }
    public float pv { get; protected set; }
    protected int attack;
    protected int defense;
    protected int speed;
    public bool isInPokeball { get; protected set; } = true;

    public string Type { get; protected set; }
    protected string typeAdvantage;
    protected string typeDisadvantage;

    public void Takle(Pokemon _target)
    {
        if (!isInPokeball)
        {
            float baseDamage = attack * 0.2f;
            Debug.Log($"{PokemonName} attaque Charge");

            _target.TakeDamage(baseDamage,"Normal") ;
        }
    }

    /// <summary>
    /// For each pokemon, Launch his ability
    /// </summary>
    /// <param name="_target"></param>
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
            float _finalDamage;
            if (_attackType == typeDisadvantage)
            {
                _finalDamage = _damage * 2f;
            }
            else if (_attackType == typeAdvantage)
            {
                _finalDamage = _damage * 0.5f;
            }
            else
            {
                _finalDamage = _damage;
            }

            pv -= _finalDamage;
            Debug.Log($"{PokemonName} a subis {_finalDamage} dégâts et a maintenant {pv}PV");

            if (pv <= 0)
            {
                Debug.Log($"{PokemonName} est KO, il rentre dans sa pokeball");
                GetInPokeball();
            }
            return;
        }
        Debug.Log($"{PokemonName} ne peut pas prendre de dégâts, il est dans sa pokeball");
    }

    /// <summary>
    /// The pokemon get out of his pokeball and can be use in the battle
    /// </summary>
    public void GetOutPokeball()
    {
        if (pv <= 0)
        {
            Debug.Log($"{PokemonName} ne peut pas sortir de sa pokeball, il est KO");
            return;
        }
        Debug.Log($"{PokemonName} sort de sa pokeball et a {pv}PV");
        isInPokeball = false;
    }

    /// <summary>
    /// The pokemon get in his pokeball and can't be use in the battle
    /// </summary>
    public void GetInPokeball()
    {
        Debug.Log($"{PokemonName} rentre dans sa pokeball");
        isInPokeball = true;
    }
}
