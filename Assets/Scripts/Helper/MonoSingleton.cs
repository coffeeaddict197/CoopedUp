using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    static bool shuttingDown = false;
    
    public static T Instance
    {
        get
        {
            if (m_Instance == null && !shuttingDown && Application.isPlaying)
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;

                if (m_Instance == null)
                {
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }
            }

            return m_Instance;
        }
    }

    protected virtual void Awake()
    {
        if (m_Instance == null)
            m_Instance = this as T;
        else if (m_Instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
            DestroyImmediate(gameObject);
            return;
        }
    }

    protected virtual void OnDestroy()
    {
        if (this == m_Instance)
            m_Instance = null;
    }

    private void OnApplicationQuit()
    {
        m_Instance = null;
        shuttingDown = true;
    }
}
