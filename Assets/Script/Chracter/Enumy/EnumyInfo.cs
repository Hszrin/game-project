using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnumyInfo : MonoBehaviour
{
    private static EnumyInfo instance;
    public CurrentTurn currentTurn = new CurrentTurn();
    public List<GameObject> enumys = new List<GameObject>();
    public UnityEvent onEnumyAllDead;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static EnumyInfo Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Start()
    {
        onEnumyAllDead.Invoke();
    }
    public void RemoveEnumy(GameObject removeEnumy)
    {
        int enumyIdx = enumys.IndexOf(removeEnumy);
        enumys.Remove(removeEnumy);

        if (enumyIdx < currentTurn.index || enumyIdx == enumys.Count)
        {
            currentTurn.subIndex();
        }

        if (enumys.Count <= 2)
        {
            currentTurn.maxCount = enumys.Count;
        }
        Destroy(removeEnumy);
    }
    public void CheckEnumyCount()
    {
        if (enumys.Count == 0) onEnumyAllDead.Invoke();
    }
    public void GetAllEnumyList()
    {
        GameObject[] enumyArr = GameObject.FindGameObjectsWithTag("Enumy");
        enumys.Clear();

        for (int i = 0; i < enumyArr.Length; i++)
        {
            enumys.Add(enumyArr[i]);
        }
        if (enumys.Count <= 2)
        {
            currentTurn.maxCount = enumys.Count;
        }
    }
}
[Serializable]
public class CurrentTurn//현재 공격할 적
{
    public int index;
    public int maxCount = 2;
    public CurrentTurn()
    {
        index = 0;
    }
    public void addIndex()
    {
        index++;
        CheckIndex();
    }
    public void subIndex()
    {
        index--;
        CheckIndex();
    }
    public void CheckIndex()
    {
        if (index >= EnumyInfo.Instance.enumys.Count || index < 0)
        {
            index = 0;
        }
    }
}