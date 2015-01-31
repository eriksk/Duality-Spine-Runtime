using System;
using System.Collections.Generic;
using Duality;
using Duality.Drawing;
using Duality.Resources;
using Duality_Spine_Runtime.Spine.Utilities;
using OpenTK;
using Spine;

namespace Duality_Spine_Runtime.Spine
{
    public class SkeletonMeshRenderer
    {
        private readonly VertexCache<VertexC1P3T2> _vertexCache;
        private readonly float[] _boneVertices = new float[8];

        public SkeletonMeshRenderer()
        {
            _vertexCache = new VertexCache<VertexC1P3T2>(4);
            Bone.yDown = true;
        }

        public void Draw(IDrawDevice device, GameObject gameObject, Skeleton skeleton, ContentRef<Material> material, ColorRgba tint, float offset)
        {
            _vertexCache.Restart();

            List<Slot> drawOrder = skeleton.DrawOrder;
            float skeletonR = skeleton.R, skeletonG = skeleton.G, skeletonB = skeleton.B, skeletonA = skeleton.A;

            for (int i = 0, n = drawOrder.Count; i < n; i++)
            {
                Slot slot = drawOrder[i];
                Attachment attachment = slot.Attachment;

                if (attachment is RegionAttachment)
                {
                    var regionAttachment = (RegionAttachment)attachment;

                    float a = skeletonA * slot.A * regionAttachment.A;
                    var color = new ColorRgba(
                        skeletonR * slot.R * regionAttachment.R * a,
                        skeletonG * slot.G * regionAttachment.G * a,
                        skeletonB * slot.B * regionAttachment.B * a, a) * tint;

                    float[] uvs = regionAttachment.UVs;
                    regionAttachment.ComputeWorldVertices(slot.Bone, _boneVertices);
                    float depthOffset = GetOffset(offset, i);

                    RenderAttachment(device, material, _boneVertices, gameObject, color, uvs, depthOffset);
                }
                else if (attachment is MeshAttachment)
                {
                    var regionAttachment = (MeshAttachment)attachment;

                    float a = skeletonA * slot.A * regionAttachment.A;
                    var color = new ColorRgba(
                        skeletonR * slot.R * regionAttachment.R * a,
                        skeletonG * slot.G * regionAttachment.G * a,
                        skeletonB * slot.B * regionAttachment.B * a, a) * tint;

                    float[] uvs = regionAttachment.UVs;
                    regionAttachment.ComputeWorldVertices(slot, _boneVertices);
                    float depthOffset = GetOffset(offset, i);

                    RenderAttachment(device, material, _boneVertices, gameObject, color, uvs, depthOffset);
       
                }
                else if (attachment is SkinnedMeshAttachment)
                {
                    var regionAttachment = (SkinnedMeshAttachment)attachment;

                    float a = skeletonA * slot.A * regionAttachment.A;
                    var color = new ColorRgba(
                        skeletonR * slot.R * regionAttachment.R * a,
                        skeletonG * slot.G * regionAttachment.G * a,
                        skeletonB * slot.B * regionAttachment.B * a, a) * tint;

                    float[] uvs = regionAttachment.UVs;
                    regionAttachment.ComputeWorldVertices(slot, _boneVertices);
                    float depthOffset = GetOffset(offset, i);

                    RenderAttachment(device, material, _boneVertices, gameObject, color, uvs, depthOffset);
                }
            }
        }

        private void RenderAttachment(IDrawDevice device, ContentRef<Material> material, float[] boneVertices, GameObject gameObject, ColorRgba color, float[] uvs, float depth)
        {
            Vector3 position = gameObject.Transform.Pos;
            float actualScale = 1f * gameObject.Transform.Scale;

            device.PreprocessCoords(ref position, ref actualScale);
            
            var vertices = _vertexCache.Next();

            vertices[0].Pos.X = position.X + boneVertices[RegionAttachment.X1] * actualScale;
            vertices[0].Pos.Y = position.Y + boneVertices[RegionAttachment.Y1] * actualScale;
            vertices[1].Pos.X = position.X + boneVertices[RegionAttachment.X2] * actualScale;
            vertices[1].Pos.Y = position.Y + boneVertices[RegionAttachment.Y2] * actualScale;
            vertices[2].Pos.X = position.X + boneVertices[RegionAttachment.X3] * actualScale;
            vertices[2].Pos.Y = position.Y + boneVertices[RegionAttachment.Y3] * actualScale;
            vertices[3].Pos.X = position.X + boneVertices[RegionAttachment.X4] * actualScale;
            vertices[3].Pos.Y = position.Y + boneVertices[RegionAttachment.Y4] * actualScale;

            vertices[0].TexCoord.X = uvs[RegionAttachment.X1];
            vertices[0].TexCoord.Y = uvs[RegionAttachment.Y1];
            vertices[1].TexCoord.X = uvs[RegionAttachment.X2];
            vertices[1].TexCoord.Y = uvs[RegionAttachment.Y2];
            vertices[2].TexCoord.X = uvs[RegionAttachment.X3];
            vertices[2].TexCoord.Y = uvs[RegionAttachment.Y3];
            vertices[3].TexCoord.X = uvs[RegionAttachment.X4];
            vertices[3].TexCoord.Y = uvs[RegionAttachment.Y4];

            vertices[0].Pos.Z = position.Z + depth;
            vertices[1].Pos.Z = position.Z + depth;
            vertices[2].Pos.Z = position.Z + depth;
            vertices[3].Pos.Z = position.Z + depth;

            vertices[0].Color = material.Res.MainColor * color;
            vertices[1].Color = material.Res.MainColor * color;
            vertices[2].Color = material.Res.MainColor * color;
            vertices[3].Color = material.Res.MainColor * color;

            device.AddVertices(material, VertexMode.Quads, vertices);
        }

        private float GetOffset(float offset, int i)
        {
            return offset - ((float)i * 0.01f);
        }
    }
}