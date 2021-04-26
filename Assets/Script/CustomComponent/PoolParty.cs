using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.CustomComponents
{
    public class PoolParty : Singleton<PoolParty>
    {
        [SerializeField]
        private List<ObjectPool> objectPools = new List<ObjectPool>();
        #region Properties
        public List<ObjectPool> ObjectPools { get => objectPools; }
        #endregion
        protected override void Awake()
        {
            #region Singleton
            base.Awake();
            #endregion
        }

        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public static ObjectPool GetPool(string name)
        {
            for(int index = 0; index < Instance.objectPools.Count; index++)
            {
                if(Instance.objectPools[index].Name == name)
                {
                    return Instance.objectPools[index];
                }
            }
            return null;
        }
    }

}