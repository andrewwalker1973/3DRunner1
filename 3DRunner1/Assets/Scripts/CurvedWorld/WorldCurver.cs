using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	public float curveStrength = 0.01f;

    [Range(-500f, 500f)]
    [SerializeField]
    float y = 0f;

    int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
    }

	void Update()
	{
		Shader.SetGlobalFloat(m_CurveStrengthID, curveStrength);
	}
}
