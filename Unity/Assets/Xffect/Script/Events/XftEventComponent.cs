using UnityEngine;
using System.Collections;
using Xft;

//deprecated
public enum XftEventType
{
	CameraShake,
	Sound,
	Light,
	CameraRadialBlur,
	CameraGlow,
    CameraRadialBlurMask,
    CameraColorInverse,
    TimeScale,
}

public enum XEventType
{
	CameraShake,
	Sound,
	Light,
	CameraEffect,
    TimeScale,
}

public class XftEventComponent : MonoBehaviour 
{
	//deprecated
	public XftEventType EventType;
	
	public XEventType Type;
    public float StartTime = 0f;
    public float EndTime = -1f;
	
	public CameraEffectEvent.EType CameraEffectType = CameraEffectEvent.EType.Glow;

    public Shader RadialBlurShader;
    public Transform RadialBlurObj;
    public float RBSampleDist = 0.3f;
	
	public MAGTYPE RBStrengthType = MAGTYPE.Fixed;
	public float RBSampleStrength = 1f;
	public AnimationCurve RBSampleStrengthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	
	
    //radial blur tex add event
    public Shader RadialBlurTexAddShader;
    public Texture2D RadialBlurMask;
    
    public float RBMaskSampleDist = 3f;
    public MAGTYPE RBMaskStrengthType = MAGTYPE.Fixed;
    public float RBMaskSampleStrength = 5f;
    public AnimationCurve RBMaskSampleStrengthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    
	//glow event
	public Shader GlowCompositeShader;
	public Shader GlowBlurShader;
	public Shader GlowDownSampleShader;
	public float GlowIntensity = 1.5f;
	public int GlowBlurIterations = 3;
	public float GlowBlurSpread = 0.7f;
	public Color GlowColorStart = new Color(0f,7f / 255f,209f / 255f,112f / 255f);
	public Color GlowColorEnd = new Color(76f / 255f,150f / 255f,1f,1f);
	/*deprecated, default is COLOR_GRADUAL_TYPE.CURVE now*/
	public COLOR_GRADUAL_TYPE GlowColorGradualType = COLOR_GRADUAL_TYPE.CLAMP;
	public float GlowColorGradualTime = 2f;
    public AnimationCurve ColorCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	//glow per obj
	public Shader GlowPerObjReplacementShader;
	public Shader GlowPerObjBlendShader;
    
    //color inverse event
    public Shader ColorInverseShader;
    public AnimationCurve CIStrengthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	
	//Sound Event
	public AudioClip Clip;
	public float Volume = 1f;
	public float Pitch = 1f;
	
	//Camera Shake Event
	public Vector3 PositionForce = new Vector3(0,6,0);
	public Vector3 RotationForce = Vector3.zero;
	public float PositionStifness = 0.3f;
	public float PositionDamping = 0.1f;
	public float RotationStiffness = 0.1f;
	public float RotationDamping = 0.25f;
	
	public bool UseEarthQuake = false;
	public float EarthQuakeMagnitude = 2f;
	public MAGTYPE EarthQuakeMagTye = MAGTYPE.Fixed;
	public AnimationCurve EarthQuakeMagCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	public float EarthQuakeTime = 2f;
	public float EarthQuakeCameraRollFactor = 0.1f;
	
	//Light Event
	public Light LightComp;
	public float LightIntensity = 1f;
	public MAGTYPE LightIntensityType = MAGTYPE.Fixed;
	public AnimationCurve LightIntensityCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	public float LightRange = 10f;
	public MAGTYPE LightRangeType = MAGTYPE.Fixed;
	public AnimationCurve LightRangeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 20));
	
    
    //Time Scale Event
    public float TimeScale = 1f;
    public float TimeScaleDuration = 1f;

    protected XftEvent m_eventHandler;

	
	protected float m_elapsedTime = 0f;
	
	protected bool m_finished = false;
	
    public void Initialize()
    {
		switch (Type)
		{
		case XEventType.CameraShake:
			m_eventHandler = new CameraShakeEvent(this);
			break;
		case XEventType.Light:
			m_eventHandler = new LightEvent(this);
			break;
		case XEventType.Sound:
			m_eventHandler = new SoundEvent(this);
			break;
		case XEventType.CameraEffect:
			if (CameraEffectType == CameraEffectEvent.EType.ColorInverse)
			{
				m_eventHandler = new ColorInverseEvent(this);
			}
			else if (CameraEffectType == CameraEffectEvent.EType.Glow)
			{
				m_eventHandler = new GlowEvent(this);
			}
			else if (CameraEffectType == CameraEffectEvent.EType.GlowPerObj)
			{
				m_eventHandler = new GlowPerObjEvent(this);
			}
			else if (CameraEffectType == CameraEffectEvent.EType.RadialBlur)
			{
				m_eventHandler = new RadialBlurEvent(this);
			}
			else if (CameraEffectType == CameraEffectEvent.EType.RadialBlurMask)
			{
				m_eventHandler = new RadialBlurTexAddEvent(this);
			}
			break;
        case XEventType.TimeScale:
            m_eventHandler = new TimeScaleEvent(this);
            break;
		default:
			Debug.LogWarning("invalid event type!");
			break;
		}
        m_eventHandler.Initialize();
		m_elapsedTime = 0f;
		m_finished = false;
    }
	
	
    public void ResetCustom()
    {
		if (m_eventHandler != null)
        	m_eventHandler.Reset();
		m_elapsedTime = 0f;
		m_finished = false;
    }
	
	
    public void UpdateCustom(float deltaTime)
    {
		if (m_finished)
			return;
		
		if (m_eventHandler != null)
		{
			m_elapsedTime += deltaTime;
			
			if (!m_eventHandler.CanUpdate && m_elapsedTime >= StartTime && StartTime >= 0f)
				m_eventHandler.OnBegin();
			
			if (m_eventHandler.CanUpdate)
				m_eventHandler.Update(deltaTime);
		
			//if this evet is hault by the time limitation, set finished true.
			//and then wait to the next Active() or Reset() to trigger its to unfinished.
			if (m_eventHandler.CanUpdate && m_elapsedTime > EndTime && EndTime > 0f)
			{
				ResetCustom();
				m_finished = true;
			}
		} 	
    }
}
