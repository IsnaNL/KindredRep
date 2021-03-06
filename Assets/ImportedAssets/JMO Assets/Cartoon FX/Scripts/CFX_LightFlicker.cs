using UnityEngine;
using System.Collections;

// Cartoon FX  - (c) 2015 Jean Moreno

namespace UnityEngine.Experimental.Rendering.Universal
{
	[RequireComponent(typeof(Light2D))]
	// Randomly changes a light's intensity over time.
	public class CFX_LightFlicker : MonoBehaviour
	{
		// Loop flicker effect
		public bool loop;

		// Perlin scale: makes the flicker more or less smooth
		public float smoothFactor = 1f;

		/// Max intensity will be: baseIntensity + addIntensity
		public float addIntensity = 1.0f;

		private float minIntensity;
		private float maxIntensity;
		private float baseIntensity;

		void Awake()
		{
			baseIntensity = GetComponent<Light2D>().intensity;
		}

		void OnEnable()
		{
			minIntensity = baseIntensity;
			maxIntensity = minIntensity + addIntensity;
		}

		void Update()
		{
			GetComponent<Light2D>().intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * smoothFactor, 0f));
		}
	}
}


