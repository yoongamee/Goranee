using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class  GOTriggerTest : MonoBehaviour
{
    ///public MonoBehaviour exescript;  이미 인스턴스화된 스크립트만 연결 가능.
    public TestClass1 Tester;

    public ITriggerConditionTest Condition = new TriggerAreaConditionTest();
    public TriggerAreaConditionTest areaoCondition;
    //public TestClass1 Tester2;

    //public List<TestClass1> Tests;
    //public List<ITriggerConditionTest> CondTest;
    public void Start()
    {
        //System.DateTime TestTime = new DateTime(2015, 12, 9, 14, 59, 30);
        System.DateTime TestTime = new DateTime(2015, 10, 8, 13, 59, 30);
        System.DateTime TestNowTime = new DateTime(System.DateTime.Now.Ticks);

        Debug.Log("TestTime " + TestTime);
        Debug.Log("NowTime " + TestNowTime);

        Debug.Log("TestTimeLong " + TestTime.ToBinary());
        Debug.Log("NowTimeLong " + TestNowTime.ToBinary());

        

        Debug.Log("timeGap " + (TestTime - TestNowTime));

        TimeSpan ts = TestTime - TestNowTime;


        Debug.Log("TimeGapDays Long " + ts.Days);
        Debug.Log("TimeGapMinutes Long " + ts.Minutes);
        Debug.Log("TimeGapTotalMinutes Long " + ts.TotalMinutes);
        Debug.Log("TimeGapSeconds Long " + ts.Seconds);
        Debug.Log("TimeGapTotalSeconds Long " + ts.TotalSeconds);
        // string으로 저장한 다음에 다시  long으로 변환해서 가져오장
        // 연.월.일 차이가 없어도 1씩 들어가 있으니까 빼자.
        // 빼기는 
    }
}
