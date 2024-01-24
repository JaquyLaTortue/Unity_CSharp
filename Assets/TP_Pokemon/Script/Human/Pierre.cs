using UnityEngine;

public class Pierre : Healer
{
    public override void SetUpHealerValues()
    {
        characterType = "Healer";
    }

    public override void HealAll()
    {
        throw new System.NotImplementedException();
    }

    public override void HealOne()
    {
        throw new System.NotImplementedException();
    }


    private void Start()
    {
        characterName = "Pierre";
        Debug.Log(characterType);
    }
}
