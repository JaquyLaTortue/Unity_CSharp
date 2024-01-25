using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    [SerializeField] Game gameScript;

    [SerializeField] TMP_Text TurnText;

    private void Start()
    {
        if (gameScript == null)
        {
            gameScript = gameObject.GetComponent<Game>();
        }

        gameScript.OnTurnChange += UpdateTurnText;
    }

    void UpdateTurnText()
    {
        Human _temp;
        Pokemon _tempPokemon;
        switch (gameScript.TurnIndex % 2)
        {
            case 0:
                _temp = gameScript.opponent1;
                _tempPokemon = gameScript.oponent1CurrentPokemon;
                break;
            case 1:
                _temp = gameScript.opponent2;
                _tempPokemon = gameScript.oponent2CurrentPokemon;
                break;
            default:
                return;
        }
        TurnText.text = $"c'est au tour de {_temp.characterName} et de son {_tempPokemon.PokemonName}";
        Debug.Log($"c'est au tour de {_temp.characterName} et de son {_tempPokemon.PokemonName}");
    }
}
