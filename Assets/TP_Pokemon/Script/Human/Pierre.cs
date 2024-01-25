public class Pierre : Human, IHealer
{
    public  void SetUpHealerValues()
    {
        characterType = "Healer";
    }

    public  void HealAll()
    {
        throw new System.NotImplementedException();
    }

    public  void HealOne()
    {
        throw new System.NotImplementedException();
    }


    private void Start()
    {
        characterName = "Pierre";
        SetUpHealerValues();
    }
}
