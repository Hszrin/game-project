using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ISheildObject
{
    Transform ReturnSheildObject();
    void AdjustSheildHp(float val);
}
public class Sheild : MonoBehaviour, ISheildObject
{
    private Transform knight;
    private Player player;
    public SheildNumber sheildNumber;
    public ChracterComponent playerComponent;
    public float sheildDmg;

    private void OnEnable()
    {
        sheildDmg = 10;
        knight = transform.parent.parent;
        player = knight.GetComponent<Player>();
        sheildNumber.ResetNum();
        playerComponent = player.chracterComponent;
    }
    private void Start()
    {
        sheildNumber = new SheildNumber(gameObject, 3);
        player.resetSkill.AddListener(sheildNumber.ResetNum);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enumy")
        {
            Vector3 direction = collision.transform.position - transform.position;
            collision.transform.position += direction / 100;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enumy" && player.state == "Attack")
        {
            Debug.Log("sheild");
            Vector3 direction = collision.transform.position - transform.position;
            collision.transform.position += direction;
            player.hitProcess.CollisionProcess(collision, new CollisionComponent 
            {
                leftAttackAngle = new List<float> { 180 },
                rightAttackAngle = new List<float> { 180 },
                dmg = 10,
                speed = playerComponent.speed
            },transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Enumy")
        {
            collision.transform.GetComponent<IAttacked>().setStateAttack();
        }
    }
    public Transform ReturnSheildObject()
    {
        return knight;
    }
    public void AdjustSheildHp(float val)
    {
        sheildNumber.SubNum();
    }
}
[Serializable]
public class SheildNumber
{
    public int number;
    public int maxNum;
    public GameObject sheild;

    public SheildNumber(GameObject sheild, int num)
    {
        this.sheild = sheild;
        maxNum = num;
        ResetNum();
    }
    public void SubNum()
    {
        number--;
        CheckNum();
    }
    public void ResetNum()
    {
        number = maxNum;
        sheild.SetActive(true);
    }
    public void UpdateMaxNum(int num)
    {
        maxNum = num;
        ResetNum();
    }
    public void CheckNum()
    {
        if (number == 0) sheild.SetActive(false);
    }
}
