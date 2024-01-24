using UnityEngine;

public class Items_prefab_script : MonoBehaviour
{

    public string itemName;

    public bool consommable;

    public int durabilite;

    public Color color;

    public Vector3 size;

    public string effect;


    public void LoadData(Items item)
    {
        itemName = item.itemName;
        consommable = item.consommable;
        durabilite = item.durabilite;
        color = item.color;
        size = item.size;
        effect = item.effect;
    }
}