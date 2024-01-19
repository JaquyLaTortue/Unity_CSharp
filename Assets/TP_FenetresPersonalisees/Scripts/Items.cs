using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Items", menuName = "TP_FenetresPersonalisees/ScriptableObjects/Items", order = 1)]
public class Items : ScriptableObject
{
    public string itemName;

    public bool consommable;

    public int durabilite;
    
    public Color color;
    
    public Vector3 size;
    
    public string effect;
}
