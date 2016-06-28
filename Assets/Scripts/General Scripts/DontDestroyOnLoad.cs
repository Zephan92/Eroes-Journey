using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
