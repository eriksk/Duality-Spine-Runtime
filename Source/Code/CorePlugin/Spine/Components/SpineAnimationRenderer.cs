using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Duality;
using Duality.Drawing;
using Duality.Resources;

namespace Duality_Spine_Runtime.Spine.Components
{
    [Serializable]
    [RequiredComponent(typeof(SpineAnimationComponent))]
    public class SpineAnimationRenderer : Component, ICmpRenderer
    {
        [NonSerialized]
        private readonly SkeletonMeshRenderer _meshRenderer = new SkeletonMeshRenderer();

        public ContentRef<Material> Material { get; set; }
        public VisibilityFlag VisibilityGroup { get; set; }

        public ColorRgba Tint { get; set; }

        public SpineAnimationRenderer()
        {
            VisibilityGroup = VisibilityFlag.Group0;
            Tint = ColorRgba.White;
        }

        public bool IsVisible(IDrawDevice device)
        {
            if ((device.VisibilityMask & VisibilityFlag.ScreenOverlay) != (VisibilityGroup & VisibilityFlag.ScreenOverlay)) return false;
            if ((VisibilityGroup & device.VisibilityMask & VisibilityFlag.AllGroups) == VisibilityFlag.None) return false;
            return device.IsCoordInView(GameObj.Transform.Pos, BoundRadius);
        }

        public virtual float BoundRadius
        {
            get { return 256f; }
        }

        public int Offset { get; set; }

        public virtual void Draw(IDrawDevice device)
        {
            var animation = GameObj.GetComponent<SpineAnimationComponent>();

            if (!animation.IsAvailable)
                return;
            if (!Material.IsAvailable)
                return;

            animation.Skeleton.UpdateWorldTransform();
            _meshRenderer.Draw(device, GameObj, animation.Skeleton, Material, Tint, Offset);
        }
    }
}
