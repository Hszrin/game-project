using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceANature : Archer
{
    public Transform forceANatureAmmo;
    public ForceANatureAmmo forceANatureAmmoScript;
    public GameObject normalArrow;
    public Index ammoIndex;
    public List<GameObject> specialAmmoPrefab = new List<GameObject>();
    public ArrowAnimScript arrowAnimScript;
    public int curIndex;
    private void Start()
    {
        ammoIndex = new Index(2);
        normalArrow = Resources.Load<GameObject>("Arrow");
        specialAmmoPrefab.Add(Resources.Load<GameObject>("IceArrow")); 
        specialAmmoPrefab.Add(Resources.Load<GameObject>("FireArrow"));
        specialAmmoPrefab.Add(Resources.Load<GameObject>("WindArrow"));
        Destroy(gameObject.GetComponent<NormalMode>());
        AfterStart();
        forceANatureAmmo = UI.transform.Find("ForceANatureAmmo");
        forceANatureAmmo.gameObject.SetActive(true);
        forceANatureAmmoScript = forceANatureAmmo.GetComponent<ForceANatureAmmo>();
        curIndex = Random.Range(0, specialAmmoPrefab.Count);
        forceANatureAmmoScript.curColorIdx = curIndex;
        forceANatureAmmoScript.Reload();
        arrowAnimScript = transform.Find("Arrow").GetComponent<ArrowAnimScript>();
    }

    public ArrowComponent arrowComponent;
    public override void ShootArrow(Vector3 arrowDirection, Quaternion transformRotation)
    {
        playerRigid.velocity = transform.forward * player.gage.value * arrowDmg / 2;
        arrow = Instantiate(arrowPrefab, transform.position, transformRotation);
        arrowComponent = arrow.GetComponent<IArrow>().ReturnArrowComponent();
        arrow.GetComponent<Rigidbody>().velocity = -arrow.transform.forward * player.gage.value * arrowDmg * 2;
        arrowComponent.onDestroy.AddListener(turn.SubMovingCnt);
        arrowComponent.dmg = arrowDmg * player.gage.value;
        arrow.GetComponent<CheckOnGround>().outBorder.AddListener(turn.SubMovingCnt);
        turn.AddMovingCnt();

        forceANatureAmmoScript.ChangeAmmoColor(ammoIndex);
        if (ammoIndex.AddIndex())
        {
            arrowPrefab = normalArrow;
            curIndex = Random.Range(0, specialAmmoPrefab.Count);
            forceANatureAmmoScript.curColorIdx = curIndex;
            forceANatureAmmoScript.Reload();
            arrowAnimScript.magicHead = arrowAnimScript.magicHeadList[arrowAnimScript.magicHeadList.Count - 1];
        }
        if (ammoIndex.CheckIndex())
        {
            arrowAnimScript.magicHead = arrowAnimScript.magicHeadList[curIndex];
            arrowPrefab = specialAmmoPrefab[curIndex];
        }
    }
}

public class Index
{
    public int index;
    public int maxIndex;
    public Index(int num)
    {
        index = 0;
        maxIndex = num;
    }
    public bool AddIndex()
    {
        index++;
        if (index == maxIndex + 1)
        {
            index = 0;
            return true;
        }
        return false;
    }
    public bool CheckIndex()
    {
        if (index == maxIndex) return true;
        else return false;
    }
}