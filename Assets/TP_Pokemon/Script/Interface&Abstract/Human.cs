using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    public Game gameScript { get; set;}
    public string characterName { get; protected set; }
    public string characterType { get; protected set; }
    [field: SerializeField] public List<Pokemon> pokemons { get; private set; } = new List<Pokemon>(2);

    public void CatchPokemon(Pokemon _pokemon)
    {
        pokemons.Add(_pokemon);
    }
}
