﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

namespace HoloToolkit.Examples.Prototyping
{
    public class CycleLocalPosition : CycleArray<Vector3>
    {
        [Tooltip("Requires Interpolator")]
        public bool SmoothLerpToTarget = false;
        public float PositionPerSecond = 30.0f;
        public float SmoothPositionLerpRatio = 0.5f;

        private Interpolator mInterpolator;

        protected override void Awake()
        {
            mInterpolator = GetComponent<Interpolator>();

            base.Awake();
            
        }

        public override void SetIndex(int index)
        {
            base.SetIndex(index);

            Vector3 item = Array[Index];

            if (mInterpolator != null)
            {
                mInterpolator.SmoothLerpToTarget = SmoothLerpToTarget;
                mInterpolator.SmoothPositionLerpRatio = SmoothPositionLerpRatio;
                mInterpolator.PositionPerSecond = PositionPerSecond;
                Transform parentTransform = TargetObject.transform.parent;
                if (parentTransform == null)
                {
                    parentTransform = TargetObject.transform;
                }
                Vector3 worldPosition = parentTransform.TransformPoint(item);
                mInterpolator.SetTargetPosition(worldPosition);
            }
            else
            {
                TargetObject.transform.localPosition = item;
            }

            
        }

    }
}
