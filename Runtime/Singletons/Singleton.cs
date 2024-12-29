using UnityEngine;

namespace Freyja.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
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

            instance = this as T;
        }

        #endregion
    }
}