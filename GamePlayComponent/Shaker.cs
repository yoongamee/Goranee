using UnityEngine;
using System.Collections;
using Goranee;
// 타일 루프 흔들림이랑, 건물 선택 흔들림이랑 이벤트 충돌이 있음. impact와 enable로 되서 서로 컨트롤이 다름..
public class Shaker : Goranee.Oscillator
{
    protected bool Play = false;
    public bool Loop = false;

    public override void Impact(Vector3 _originalLocalPosition, bool _destroy,
            float _duration, float _maxAngle, float _amplitude, float _count, AXIS _axis = AXIS.Y)
    {
        Play = true;
        enabled = true;
        base.Impact(_originalLocalPosition, _destroy, _duration, _maxAngle, _amplitude, _count, _axis);
        StopAllCoroutines();
        StartCoroutine(UpdatePos());
        
        
        
    }
  
    void OnDisable()
    {
        StopAllCoroutines();
        ForceFinish();
    }
    IEnumerator UpdatePos()
    {
        while(true)
        {
            if (Play == true)
            {
                UpdateLogic();
            }
            else
            {
                StopAllCoroutines();
                yield return null;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    protected override void Finish()
    {
        /*if (Loop == true)
        {
            Impact(originalPosition, destroy, duration, maxAngle, amplitude, count, axis);
        }
        else
        {
            base.Finish();
            Play = false;
        }*/
        base.Finish();
        Play = false;
    }
    public override void ForceFinish()
    {
        base.Finish();
        enabled = false;
        Play = false;
    }
}
