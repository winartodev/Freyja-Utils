using UnityEngine;

namespace Freyja.Utils
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        #region Static

        protected static T instance;

        public static bool HasInstance
        {
            get => instance != null;
        }

        public static T TryGetInstance()
        {
            return HasInstance ? instance : null;
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + "_AutoCreated");
                        instance = go.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Fields

        public bool AutoUnparentOnAwake = true;

        #endregion

        #region Methods

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            if (AutoUnparentOnAwake)
            {
                transform.SetParent(null);
            }

            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        #endregion
    }
}