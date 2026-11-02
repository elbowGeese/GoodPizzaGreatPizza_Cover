using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static GameObject instance;

    void Awake()
    {
        if(instance == null) 
        { 
            instance = this.gameObject;
            DontDestroyOnLoad(instance);
        }
        else 
        { 
            Destroy(this.gameObject); 
        }
    }
}
