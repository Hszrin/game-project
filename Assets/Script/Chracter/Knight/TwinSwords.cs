using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinSwords : MonoBehaviour
{
    private GameObject swordSwing;
    [SerializeField]
    private Transform leftSword;
    [SerializeField]
    private Transform rightSword;
    private Knight knight;
    private Player player;
    private SphereCollider sphereCollider;
    void Start()
    {
        swordSwing = transform.Find("SwordSwing").gameObject;
        swordSwing.AddComponent<TwinSWordSwing>();
        Destroy(swordSwing.GetComponent<NormalSwing>());

        leftSword = transform.Find("LeftSword");
        rightSword = transform.Find("RightSword");
        Vector3 vec = new Vector3(1.6f, 1.6f, 0.1f);
        leftSword.localScale = vec;
        rightSword.localScale = vec;

        knight = transform.GetComponent<Knight>();
        knight.onActivateLeftSword.AddListener(knight.ActiveLeftSword);
        knight.onDeActivateLeftSword.AddListener(knight.DeActiveLeftSword);

        player = transform.GetComponent<Player>();
        player.onCollisionEnterEvent.AddListener(Elasticity);
        player.GetComponent<IStateEvent>().ReturnStateEvent().onExitAttack.AddListener(Elasticity);

        sphereCollider = swordSwing.GetComponent<SphereCollider>();
        sphereCollider.radius = 2.3f;
    }
    public void Elasticity()
    {
        player.playerRigid.velocity *= 2f;
        player.onCollisionEnterEvent.RemoveListener(Elasticity);
    }
}
