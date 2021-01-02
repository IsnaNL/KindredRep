using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.Experimental.Rendering.Universal
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AttachGameObjectsToParticles : MonoBehaviour
    {
        public GameObject m_Prefab;

        private ParticleSystem m_ParticleSystem;
        private List<GameObject> m_Instances = new List<GameObject>();
        private List<Light2D> m_InstanceLights = new List<Light2D>();
        private ParticleSystem.Particle[] m_Particles;
        private int count;
        
        [SerializeField] bool colorEqIntensity;

        private const float alphaColorDivide = 0.004f;

        [Range(0, 5)]
        [SerializeField] private float intensityModifier;

        private bool worldSpace;

        void Start()
        {
            intensityModifier *= alphaColorDivide;
            m_ParticleSystem = GetComponent<ParticleSystem>();
            m_Particles = new ParticleSystem.Particle[m_ParticleSystem.main.maxParticles];
            worldSpace = (m_ParticleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World);
        }
        void LateUpdate()
        {

            while (m_Instances.Count < count)
            {
                CreateAndAddToLists(); // needs refactoring
            }
            UpdateLists();
        }
        private void CreateAndAddToLists()
        {
            GameObject tempGo = Instantiate(m_Prefab, m_ParticleSystem.transform);
            m_Instances.Add(tempGo);
            m_InstanceLights.Add(tempGo.GetComponent<Light2D>());
        }
        private void UpdateLists()
        {
            count = m_ParticleSystem.GetParticles(m_Particles);

            for (int i = 0; i < m_Instances.Count; i++)
            {
                if (i < count)
                {
                    if (colorEqIntensity)
                    {
                        m_InstanceLights[i].intensity = m_Particles[i].GetCurrentColor(m_ParticleSystem).a * intensityModifier;
                    }
                    if (worldSpace)
                        m_Instances[i].transform.position = m_Particles[i].position;
                    else
                        m_Instances[i].transform.localPosition = m_Particles[i].position;
                    m_Instances[i].SetActive(true);
                }
                else
                {
                    m_Instances[i].SetActive(false);
                }
            }
        }
    }
}
