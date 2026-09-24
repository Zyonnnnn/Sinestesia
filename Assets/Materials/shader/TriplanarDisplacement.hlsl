#ifndef TRIPLANAR_DISPLACEMENT_INCLUDED
#define TRIPLANAR_DISPLACEMENT_INCLUDED

void GetTriplanarDisplacement_float(float3 WorldPos, float3 WorldNormal, float Scale, UnityTexture2D HeightMap, out float3 Offset)
{
    float3 blend = abs(WorldNormal);
    blend /= (blend.x + blend.y + blend.z);

    float2 uvX = WorldPos.zy * Scale;
    float2 uvY = WorldPos.xz * Scale;
    float2 uvZ = WorldPos.xy * Scale;

    float hX = SAMPLE_TEXTURE2D_LOD(HeightMap, HeightMap.samplerstate, uvX, 0).r;
    float hY = SAMPLE_TEXTURE2D_LOD(HeightMap, HeightMap.samplerstate, uvY, 0).r;
    float hZ = SAMPLE_TEXTURE2D_LOD(HeightMap, HeightMap.samplerstate, uvZ, 0).r;

    float height = hX * blend.x + hY * blend.y + hZ * blend.z;

    Offset = WorldNormal * height * Scale;
}

#endif