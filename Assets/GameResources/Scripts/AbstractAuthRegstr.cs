using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class AbstractAuthRegstr : MonoBehaviour
{
    [SerializeField]
    protected Text login;
    [SerializeField]
    protected Text password;

    [SerializeField]
    protected GameObject popUpWindow;
    [SerializeField]
    protected Text popUpText;

    protected abstract void Start();
    protected abstract void OnDestroy();
}
