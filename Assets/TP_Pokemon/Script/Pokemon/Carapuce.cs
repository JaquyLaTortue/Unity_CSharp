using UnityEngine;

public class Carapuce : Pokemon
{
    private void Start()
    {
        PokemonName = "Carapuce";
        Pv = 100;
        MaxPv = 100;
        attack = 50;
        defense = 60;
        speed = 50;
        Type = "Water";
        typeAdvantage = "Fire";
        typeDisadvantage = "Grass";
    }

    public override void Ability(Pokemon _target)
    {
        PistoletAO(_target);
    }

    private void PistoletAO(Pokemon _target)
    {
        if (!IsInPokeball)
        {
            Debug.Log($"{PokemonName} utilise Pistoler à O");
            float _baseDamage = attack * 0.9f;
            _target.TakeDamage(_baseDamage, "Water");
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
