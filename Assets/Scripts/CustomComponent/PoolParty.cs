using System;
using System.Collections.Generic;

namespace UnityEngine.CustomComponents
{
    [Serializable]
    public class PoolParty
    {
        [SerializeField]
        private string name;
        [SerializeField]
        protected List<ObjectPool> pools = new List<ObjectPool>();
        #region Properties
        public List<ObjectPool> Pools { get => pools; }
        #endregion
        #region Constructor
        public PoolParty()
        {
        }
        public PoolParty(string name)
        {
            this.name = name;
        }
        public PoolParty(string name, List<ObjectPool> pools)
        {
            this.name = name;
            this.pools = pools;
        }
        #endregion
        public virtual ObjectPool GetPool(string name)
        {
            for (int index = 0; index < pools.Count; index++)
            {
                if (pools[index].Name == name)
                {
                    return pools[index];
                }
            }
            return null;
        }

        public virtual ObjectPool GetPool(GameObject gameObject)
        {
            foreach (ObjectPool pool in CharacterPoolParty.Instance.Party.Pools)
            {
                foreach (GameObject pooledObject in pool.PooledObjects)
                {
                    if (gameObject == pooledObject)
                    {
                        return pool;
                    }
                }
            }
            return null;
        }
    }

}
