using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlayerSpecialStatList
{
    public PlayerSpecialStatList ReturnPlayerSpecialStatList();
}
[Serializable]
public class PlayerSpecialStatList
{
    public List<SpecialStat> specialStatList = new List<SpecialStat> ();

    public void AddSpecialList(string text, float amount)
    {
        specialStatList.Add(new SpecialStat { statName = text, amount = amount });
    }

    public string ReturnStatName(int idx)
    {
        return specialStatList[idx].statName;
    }
    public float ReturnAmount(int idx)
    {
        return specialStatList[idx].amount;
    }
    [Serializable]
    public class SpecialStat
    {
        public string statName;
        public float amount;
    }
}