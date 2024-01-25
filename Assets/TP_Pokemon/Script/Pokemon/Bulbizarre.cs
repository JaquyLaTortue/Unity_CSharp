using UnityEngine;
public class Bulbizarre : Pokemon
{

    void Start()
    {
        PokemonName = "Bulbizarre";
        pv = 120;
        attack = 40;
        defense = 60;
        speed = 30;
        Type = "Grass";
        typeAdvantage = "Water";
        typeDisadvantage = "Fire";
    }

    public override void Ability(Pokemon _target)
    {
        soin();
    }

    public void soin()
    {
        if (!isInPokeball)
        {
            switch (pv)
            {
                case > 90:
                    pv = 120;
                    Debug.Log($"{PokemonName} s'est soigné et a maintenant {pv}PV");
                    break;
                default:
                    pv += 30;
                    Debug.Log($"{PokemonName} s'est soigné et a maintenant {pv}PV");
                    break;
            }
            return;
        }

        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }

    public void FouetLianes(Pokemon _target)
    {
        if (!isInPokeball)
        {
            float _baseDamage = attack * 0.8f;
            _target.TakeDamage(_baseDamage, "Grass");
        }
        Debug.Log($"{PokemonName} ne peut rien faire, il est dans sa pokeball");
    }
}
