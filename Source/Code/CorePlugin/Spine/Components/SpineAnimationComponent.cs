using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using Duality;
using Duality_Spine_Runtime.Spine.Resources;
using Spine;

namespace Duality_Spine_Runtime.Spine.Components
{
    [Serializable]
    public class SpineAnimationComponent : Component, ICmpUpdatable
    {
        private ContentRef<SpineAtlas> _atlas = ContentRef<SpineAtlas>.Null;
        private ContentRef<Resources.SkeletonData> _skeletonData = ContentRef<Resources.SkeletonData>.Null;
        
        [NonSerialized]
        private SkeletonJson _skeletonJson;
        [NonSerialized] 
        private Skeleton _skeleton;
        [NonSerialized]
        private AnimationState _state;

        public ContentRef<SpineAtlas> Atlas
        {
            get { return _atlas; }
            set
            {
                _atlas = value;
                CreateSkeletonIfPossible();
            }
        }

        public ContentRef<Resources.SkeletonData> SkeletonData
        {
            get { return _skeletonData; }
            set
            {
                _skeletonData = value;
                CreateSkeletonIfPossible();
            }
        }

        public Skeleton Skeleton 
        {
            get
            {
                if(!IsAvailable)
                    CreateSkeletonIfPossible();

                return _skeleton;
            }
        }

        public string DebugAnimation
        {
            set
            {
                if (IsAvailable && Animations.Contains(value))
                {
                    _state.SetAnimation(0, value, true);
                }
            }
            get
            {
                if (IsAvailable && _state.GetCurrent(0) != null)
                    return _state.GetCurrent(0).Animation.Name;
                return "No anim";
            }
        }

        public bool FlipHorizontally 
        {
            get
            {
                if (!IsAvailable) return false;
                return _skeleton.FlipX;
            }
            set { _skeleton.FlipX = value; }
        }

        public bool FlipVertically
        {
            get
            {
                if (!IsAvailable) return false;

                return _skeleton.FlipY;
            }
            set
            {
                _skeleton.FlipY = value;
            }
        }

        public float TimeScale 
        {
            get
            {
                if (!IsAvailable) return 1f;
                return _state.TimeScale;
            }
            set { _state.TimeScale = value; }
        }

        public string[] Animations
        {
            get
            {
                if(!IsAvailable)
                    return new string[]{ "None" };
                return _state.Data.SkeletonData.Animations.Select(x => x.Name).ToArray();
            }
        }

        private void CreateSkeletonIfPossible()
        {
            if (_atlas.IsAvailable && _atlas.Res.Atlas != null && _skeletonData.IsAvailable && !string.IsNullOrEmpty(_skeletonData.Res.Content))
            {
                _skeletonJson = new SkeletonJson(_atlas.Res.Atlas);
                _skeleton = new Skeleton(_skeletonJson.ReadSkeletonData(new StringReader(_skeletonData.Res.Content)));
                _state = new AnimationState(new AnimationStateData(_skeleton.Data));
            }
        }

        public bool IsAvailable 
        {
            get
            {
                if (_skeleton == null)
                    CreateSkeletonIfPossible();
                return _skeleton != null;
            }
        }

        public void SetAnimation(string name, int track = 0, bool loop = true)
        {
            _state.SetAnimation(track, name, loop);
        }

        public void OnUpdate()
        {
            if (!IsAvailable)
                return;

            _state.Update(Time.LastDelta / 1000f);
            _state.Apply(_skeleton);
            _skeleton.Update(Time.LastDelta / 1000f);
        }
    }
}
