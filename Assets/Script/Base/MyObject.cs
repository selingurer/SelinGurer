
namespace Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MyObject : MonoBehaviour
    {
        public virtual int VariantID { get; }

        public event Action Activated;
        public event Action DeActivated;
        public static event Action Destroyed;


        private void Awake()
        {
            Initialize();
            Registration();
        }

        private void Start()
        {
            AfterInitialize();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void AfterInitialize()
        {

        }
        protected virtual void OnDestroy()
        {
            Destroyed?.Invoke();
            UnRegistration();
        }

        protected virtual void OnEnable()
        {
            Activated?.Invoke();
        }

        protected virtual void OnDisable()
        {
            DeActivated?.Invoke();
        }

        protected virtual void Registration()
        {
            Application.quitting += UnRegistration;
        }

        protected virtual void UnRegistration()
        {
            Application.quitting -= UnRegistration;
        }

        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeActivate()
        {
            gameObject.SetActive(false);
        }
    }
}