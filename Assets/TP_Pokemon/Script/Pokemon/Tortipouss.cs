using UnityEngine;

public class Tortipouss : Pokemon
{
    private void Start()
    {
        PokemonName = "Tortipouss";
        Pv = 90;
        MaxPv = 80;
        attack = 70;
        defense = 60;
        speed = 40;
        Type = "Grass";
        typeAdvantage = "Water";
        typeDisadvantage = "Fire";
    }

    public override void Ability(Pokemon _target)
    {
        TranchHerbe(_target);
    }

    public void TranchHerbe(Pokemon _target)
    {
        if (!IsInPokeball)
        {
            Debug.Log($"{PokemonName} utilise Tranch'Herbe");
            float _baseDamage = attack * 1.1f;
            _target.TakeDamage(_baseDamage, "Grass");
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
