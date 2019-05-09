using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrayTweener : MonoBehaviour
{
    [SerializeField] tweener[] anim;
    public int obj;

    void Start()
    {
        for (int i = 0; i < anim.Length; i++)
        {
            if (anim[i].ease == Ease.Unset)
                anim[i].ease = Ease.OutBack;
            anim[i].obj.DOMove(anim[i].posisiAkhir, anim[i].waktu).SetDelay(anim[i].delay).SetEase(anim[i].ease);
        }
        for (int i = obj; i < anim.Length; i++)
        {
            anim[i].obj.DOScale(1.5f, .75f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    [System.Serializable]
    public class tweener
    {
        public Transform obj;
        public Vector3 posisiAkhir;
        public float waktu = 0.75f;
        public float delay = 0;
        public Ease ease = Ease.OutBack;
    }
}

