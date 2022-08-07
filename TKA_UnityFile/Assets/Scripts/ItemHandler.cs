using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not used in the final version of the game

public class ItemHandler : MonoBehaviour
{
    //creates a list of locations for each item type (apple/melon/chest)
    //also creates a list to store each item type in
    [SerializeField]
    private List<Vector3> applesPos = new List<Vector3>();
    private static List<GameObject> apples = new List<GameObject>();
    [SerializeField]
    private List<Vector3> melonsPos = new List<Vector3>();
    private static List<GameObject> melons = new List<GameObject>();
    [SerializeField]
    private List<Vector3> chestsPos = new List<Vector3>();
    private static List<GameObject> chests = new List<GameObject>();

    //stores a prefab of each item to duplicate from
    [SerializeField]
    private GameObject applePrefab;
    [SerializeField]
    private GameObject melonPrefab;
    [SerializeField]
    private GameObject chestPrefab;

    private void Start()
    {
        //the first items in each list of items is the base item which will be replicated
        apples.Add(applePrefab);
        melons.Add(melonPrefab);
        chests.Add(chestPrefab);
        //replicate the base item based on how many different item posisitions were received in the position lists
        if (apples[0] != null) for (int i = 0; i < applesPos.Count; i++) apples.Add(Instantiate(apples[0], applesPos[i], Quaternion.identity, this.transform));
        if (melons[0] != null) for (int i = 0; i < melonsPos.Count; i++) melons.Add(Instantiate(melons[0], melonsPos[i], Quaternion.identity, this.transform));
        if (apples[0] != null) for (int i = 0; i < chestsPos.Count; i++) chests.Add(Instantiate(chests[0], chestsPos[i], Quaternion.identity, this.transform));
    }

}
