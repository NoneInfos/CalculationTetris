using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instacne 
    {
        get 
        {
            if(_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }

            if (_instance == null)
            {
                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                _instance.gameObject.name = typeof(T).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }


            return _instance;
        }
    }





}
