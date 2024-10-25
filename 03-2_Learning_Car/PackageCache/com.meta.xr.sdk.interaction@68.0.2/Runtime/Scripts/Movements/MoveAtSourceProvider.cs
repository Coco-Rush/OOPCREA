﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;

namespace Oculus.Interaction
{
    /// <summary>
    /// Moves the selected interactable around a fixed point at its current position.
    /// </summary>
    public class MoveAtSourceProvider : MonoBehaviour, IMovementProvider
    {
        public IMovement CreateMovement()
        {
            return new MoveRelativeToTarget();
        }
    }

    public class MoveRelativeToTarget : IMovement
    {
        public Pose Pose => _current;
        public bool Stopped => true;

        private Pose _current = Pose.identity;
        private Pose _originalTarget;
        private Pose _originalSource;

        public void MoveTo(Pose target)
        {
            _originalTarget = target;
        }

        public void UpdateTarget(Pose target)
        {
            Pose source = new Pose(
                _originalSource.position,
                _originalTarget.rotation);
            Pose grabberDelta = PoseUtils.Delta(_originalTarget, target);
            PoseUtils.Multiply(source, grabberDelta, ref _current);
        }

        public void StopAndSetPose(Pose source)
        {
            _current = _originalSource = source;
        }

        public void Tick()
        {
        }
    }
}
