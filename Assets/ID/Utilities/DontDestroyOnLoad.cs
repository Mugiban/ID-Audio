using UnityEngine;

namespace ID.Runtime.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    } 
}

