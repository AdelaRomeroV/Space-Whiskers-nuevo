using System.Collections.Generic;

namespace UnityEngine.Pool
{
    public abstract class PoolBehaviour : PoolBase
    {
        [SerializeReference] protected PoolObject prefab;
        
        private IObjectPool<IObjectPooled> _pool;

        protected virtual void Awake()
        {
            _pool = new ObjectPool<IObjectPooled>(OnCreate, OnGet, OnRelease, OnDestroyObject);
        }
        protected virtual IObjectPooled OnCreate()
        {
            var @object = Instantiate(prefab, parent).GetComponent<IObjectPooled>();
            @object.SetPoolReference(_pool);
            return @object;
        }

        protected IObjectPooled GetPrefab()
        {
            return _pool.Get();
        }
    }
    public abstract class PoolArrayBehaviour : PoolBase
    {
        [SerializeReference] protected PoolObject[] prefabs;
        
        private readonly Dictionary<int, IObjectPool<IObjectPooled>> _pools = new();
        
        protected virtual void Awake()
        {
            foreach (var @object in prefabs)
                _pools.Add(@object.GetEntityId(), new ObjectPool<IObjectPooled>(() => OnCreate(@object), OnGet, OnRelease, OnDestroyObject));
        }
        protected virtual IObjectPooled OnCreate(PoolObject prefab)
        {
            var obj = Instantiate(prefab, parent).GetComponent<IObjectPooled>();
            obj.SetPoolReference(_pools[prefab.GetEntityId()]);
            return obj;
        }

        protected IObjectPooled GetPrefabByIndex(int index)
        {
            int entityId = prefabs[index].GetEntityId();
            return _pools[entityId].Get();
        }
        protected IObjectPooled GetPrefabRandom()
        {
            int entityId = prefabs[Random.Range(0, prefabs.Length)].GetEntityId();
            return _pools[entityId].Get();
        }
    }
}