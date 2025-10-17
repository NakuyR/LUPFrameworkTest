using Manager;
using UnityEngine;

namespace Manager
{
    public class MainStage : BaseStage
    {
        protected override void Awake() 
        {
            base.Awake();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void LoadStage(string stage)
        {
            base.LoadStage(stage);
        }
    }
}

