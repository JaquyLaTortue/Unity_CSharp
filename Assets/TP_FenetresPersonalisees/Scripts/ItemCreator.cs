using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.TerrainTools;
using Unity.VisualScripting;

public class ItemCreator : EditorWindow
{
    string Name;
    bool consommable;
    int durabilite;
    Color color;
    Vector3 size;
    string effect;
    GameObject prefab;
    [MenuItem("Window/La fenêtre du seigneur")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreator>();
    }

    private void OnGUI()
    {

        GUILayout.Label("Name", EditorStyles.boldLabel);
        Name = EditorGUILayout.TextField(Name);
        GUILayout.Space(10);

        GUILayout.Label("Consommable", EditorStyles.boldLabel);
        consommable = EditorGUILayout.Toggle(consommable);
        GUILayout.Space(10);

        GUILayout.Label("Durabilité", EditorStyles.boldLabel);
        durabilite = EditorGUILayout.IntField(durabilite);
        GUILayout.Space(10);

        GUILayout.Label("Couleur", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField(color);
        GUILayout.Space(10);

        GUILayout.Label("Object Size", EditorStyles.boldLabel);
        size = EditorGUILayout.Vector3Field("", size);
        GUILayout.Space(10);

        GUILayout.Label("Effet", EditorStyles.boldLabel);
        effect = EditorGUILayout.TextField(effect);

        if (GUILayout.Button("Create an Item") && Name != "")
        {

            Creation();
        }

        GUILayout.Space(30);

        GUILayout.Label("Prefab", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        prefab = (GameObject)EditorGUILayout.ObjectField(prefab, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Instantiate a Prefab"))
        {
            Repaint();
            Instantiation();
        }
    }

    private void Update()
    {
        Repaint();
    }

    void Creation()
    {
        Items item = (Items)CreateInstance("Items");

        item.itemName = this.Name;
        item.consommable = this.consommable;
        item.durabilite = this.durabilite;
        item.color = this.color;
        item.size = this.size;
        item.effect = this.effect;
        AssetDatabase.CreateAsset(item, $"Assets/TP_FenetresPersonalisees/ScriptableObjects/{Name}.asset");
    }

    void Instantiation()
    {
        Items item = (Items)CreateInstance("Items");
        
        item.itemName = this.Name;
        item.consommable = this.consommable;
        item.durabilite = this.durabilite;
        item.color = this.color;
        item.size = this.size;
        item.effect = this.effect;
        AssetDatabase.CreateAsset(item, $"Assets/TP_FenetresPersonalisees/ScriptableObjects/{Name}.asset");

        GameObject prefabCreated = Instantiate(prefab);
        Items_prefab_script ipsScript = prefabCreated.AddComponent<Items_prefab_script>();
        ipsScript.LoadData(item);
    }
}
