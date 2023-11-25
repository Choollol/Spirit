using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinkTowerBase : PuzzleComponent
{
    private FallinkTowerHolder tower;

    private int towerSiblingIndex;
    public override void Awake()
    {
        base.Awake();

        tower = transform.parent.GetComponent<FallinkTowerHolder>();

        towerSiblingIndex = tower.transform.GetSiblingIndex();

        isRangedInteractable = true;
    }
    public override void OnEnable()
    {
        base.OnEnable();

        tower.IncrementBaseCount();
    }
    public override void OnDisable()
    {
        base.OnDisable();

        tower.DecrementBaseCount();
    }

    public override void RangedInteract()
    {
        base.RangedInteract();

        if (towerSiblingIndex == 0)
        {
            GetFirstActiveBase(controllerTransform.GetChild(towerSiblingIndex + 1)).gameObject.SetActive(false);
        }
        else if (towerSiblingIndex == tower.transform.parent.childCount - 1)
        {
            GetFirstActiveBase(controllerTransform.GetChild(towerSiblingIndex - 1)).gameObject.SetActive(false);
        }
        else
        {
            GetFirstActiveBase(controllerTransform.GetChild(towerSiblingIndex - 1)).gameObject.SetActive(false);
            GetFirstActiveBase(controllerTransform.GetChild(towerSiblingIndex + 1)).gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        AudioPlayer.PlaySound("Fallink Tower Break Sound", 0.9f, 1.1f, false);
    }
    private Transform GetFirstActiveBase(Transform tower)
    {
        for (int i = 1; i < tower.childCount; i++)
        {
            if (tower.GetChild(i).gameObject.activeSelf)
            {
                return tower.GetChild(i);
            }
        }
        return null;
    }
}
