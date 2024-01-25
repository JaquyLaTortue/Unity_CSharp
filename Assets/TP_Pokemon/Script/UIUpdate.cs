using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    [SerializeField] private Game gameScript;

    [SerializeField] private TMP_Text turnText;

    private void Start()
    {
        if (gameScript == null)
        {
            gameScript = gameObject.GetComponent<Game>();
        }

        gameScript.OnTurnChange += UpdateTurnText;
    }

    private void UpdateTurnText()
    {
        Human _temp;
        Pokemon _tempPokemon;
        switch (gameScript.TurnIndex % 2)
        {
            case 0:
                _temp = gameScript.Opponent1;
                _tempPokemon = gameScript.Oponent1CurrentPokemon;
                break;
            case 1:
                _temp = gameScript.Opponent2;
                _tempPokemon = gameScript.Oponent2CurrentPokemon;
                break;
            default:
                return;
        }

        turnText.text = $"C'est au tour de {_temp.CharacterName} et de son {_tempPokemon.PokemonName}";
        Debug.Log($"C'est au tour de {_temp.CharacterName} et de son {_tempPokemon.PokemonName}");
    }
}
