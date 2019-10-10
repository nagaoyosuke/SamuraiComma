using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMakeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Stage1;
    [SerializeField]
    private GameObject Stage2;
    [SerializeField]
    private GameObject Stage3;

    [SerializeField]
    private Vector3 StagePosition;

    // Start is called before the first frame update
    void Awake()
    {
        Make();
    }

	private IEnumerator Start()
	{
		yield return new WaitForFixedUpdate();
		Save.windZ = 10;
	}

	void Make()
    {
        GameObject Stage = null;
        switch (Save.stageState)
        {
            case Save.StageState.STAGE1:
                Stage = Instantiate(Stage1);
                break;
            case Save.StageState.STAGE2:
                Stage = Instantiate(Stage2);
                break;
            case Save.StageState.STAGE3:
                Stage = Instantiate(Stage3);
                break;
        }

        Stage.transform.position = StagePosition;

    }
}
