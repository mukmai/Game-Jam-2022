using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance {
        get {
            if (!_instance) {
                _instance = FindObjectOfType<ObjectPool>();
                _instance.InitObjectPool();
            }
            return _instance;
        }
    }

    private void Awake() {
        InitObjectPool();
    }
    
    private List<int> _poolKeys;
    private Dictionary<int, List<GameObject>> _pool;

    public void InitObjectPool() {
        _poolKeys = new List<int>();
        _pool = new Dictionary<int, List<GameObject>>();
    }

    public void DestroyPoolAll() {
        // ***** Will cause lag *****
        foreach (var pair in _pool) {
            if (pair.Value != null) {
                foreach (var obj in pair.Value) {
                    if (obj != null) {
                        Destroy(obj);
                    }
                }
            }
        }
        _poolKeys = new List<int>();
        _pool = new Dictionary<int, List<GameObject>>();
    }

    public GameObject CreateObject(GameObject obj, Transform parent = null, Vector3 localPos = default(Vector3),
        Quaternion localRot = default(Quaternion)) {
        int key = obj.GetInstanceID();

        if (_poolKeys.Contains(key) == false && _pool.ContainsKey(key) == false) {
            _poolKeys.Add(key);
            _pool.Add(key, new List<GameObject>());
        }

        List<GameObject> goList = _pool[key];
        goList.RemoveAll(g => g == null);
        GameObject go = null;

        for (int i = goList.Count - 1; i >= 0; i--) {
            go = goList[i];
            if (go.activeSelf == false) {
                go.transform.SetParent(parent);
                go.transform.localPosition = localPos;
                go.transform.localRotation = localRot;
                go.transform.localScale = Vector3.one;
                go.SetActive(true);
                return go;
            }
        }

        // Instantiate because there is no free GameObject in object pool.
        go = (GameObject)Instantiate(obj);
        go.transform.SetParent(parent);
        go.transform.localPosition = localPos;
        go.transform.localRotation = localRot;
        go.transform.localScale = Vector3.one;
        go.SetActive(true);
        goList.Add(go);

        return go;
    }

    public void DestroyObject(GameObject obj) {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}
