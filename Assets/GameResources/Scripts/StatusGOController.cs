using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Включает лист GO, если все тоглы isOn
/// </summary>
public class StatusGOController : MonoBehaviour
{
    [SerializeField]
    private List<Toggle> toggles;

    [SerializeField]
    private List<GameObject> gameObjects;

    private void Start()
    {
        foreach(Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(OnChange);
        }
    }

    private void OnDestroy()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.RemoveListener(OnChange);
        }
    }

    private void OnChange(bool status)
    {
        bool state = true;
        foreach (Toggle toggle in toggles)
        {
            state &= toggle.isOn;
            Debug.Log(toggle.gameObject.name + " " + toggle.isOn);
        }

        foreach (GameObject go in gameObjects)
        {
            go.SetActive(state);
        }
    }
}
