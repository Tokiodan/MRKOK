using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class DisplayItemDetails : MonoBehaviour
{
    private TextMeshProUGUI _ItemName;
    private TextMeshProUGUI _ItemDesc;
    private GameObject _ItemStatList;
    public GameObject TextPrefab;

    [SerializeField] private float Y_OFFSET = 44f;
    private float Y_SIZE = 14f;

    private void Start()
    {
        _ItemName = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _ItemDesc = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _ItemStatList = gameObject.transform.GetChild(2).gameObject;
    }

    public void LoadData(string ItemName, string ItemDesc, Dictionary<string, object> ItemStats)
    {
        _ItemName.text = ItemName;
        _ItemDesc.text = ItemDesc;
        int i = 0;

        // loops though all the item stats given and displays them.
        //small offset calculation so the stats don't do collapse on themselves.
        foreach (KeyValuePair<string, object> Stat in ItemStats)
        {
            // forces item to not make more item stats than it can show.
            if (i > 6)
            {
                break;
            }

            GameObject textline = Instantiate(TextPrefab, _ItemStatList.transform);
            textline.GetComponent<RectTransform>().localPosition = TextPos(i);

            i++;

        }

    }
    private Vector3 TextPos(int i)
    {
        return new Vector3(0, Y_OFFSET - (Y_SIZE * i), 0);
    }
}
