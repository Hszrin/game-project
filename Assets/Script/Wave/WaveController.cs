using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using UnityEngine.Events;

public class WaveController : MonoBehaviour
{
    public TurnManager turn;
    public GameObject knight;
    public GameObject archer;
    private Rigidbody knightRigid;
    private Rigidbody archerRigid;
    public GameObject wavePanel;
    public GameObject gameEndPanel;
    public int waveRound = 0;
    public UnityEvent resetEvent;

    private void Start()
    {
        knightRigid = knight.GetComponent<Rigidbody>();
        archerRigid = archer.GetComponent<Rigidbody>();
    }
    public void WaveStart()
    {
        waveRound++;
        if (waveRound == 1) InstantiateChracter();
        if (waveRound == 5) gameEndPanel.SetActive(true);

        wavePanel.SetActive(true);
    }

    public void InstantiateChracter()
    {
        string path = string.Format("Json/Wave{0}/Wave", waveRound);
        TextAsset jsonTextFile = Resources.Load(path) as TextAsset;
        WaveList currentWave = JsonUtility.FromJson<WaveList>(jsonTextFile.ToString());
        int waveIndex = Random.Range(0, currentWave.Wave.Count);

        for (int i = 0; i < currentWave.Wave[waveIndex].enumys.Count; i++)
        {
            turn.InstantiateEnumy(currentWave.Wave[waveIndex].enumys[i], currentWave.Wave[waveIndex].positions[i]);
        }
        if (knight != null)
        {
            knightRigid.velocity = Vector3.zero;
            knight.transform.position = currentWave.Wave[waveIndex].playerPosFirst;
        }
        if (archer != null)
        {
            archerRigid.velocity = Vector3.zero;
            archer.transform.position = currentWave.Wave[waveIndex].playerPosSec;
        }

        EnumyInfo.Instance.GetAllEnumyList();
        resetEvent.Invoke();
    }
}

[System.Serializable]
public class WaveInfo
{
    public List<string> enumys = new List<string>();
    public List<Vector3> positions = new List<Vector3>();
    public Vector3 playerPosFirst = new Vector3();
    public Vector3 playerPosSec = new Vector3();
}

[System.Serializable]
public class WaveList
{
    public List<WaveInfo> Wave = new List<WaveInfo>();
}
