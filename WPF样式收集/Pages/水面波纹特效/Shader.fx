float dx : register(C0);
float dy : register(C1);

sampler2D Background : register(S0);
sampler2D Height : register(S1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float xoff = tex2D(HeightSampler, float2(saturate(uv.x + dx), uv.y)).x - tex2D(HeightSampler, float2(saturate(uv.x - dx), uv.y)).x;
	float yoff = tex2D(HeightSampler, float2(uv.x, saturate(uv.y + dy))).x - tex2D(HeightSampler, float2(uv.x, saturate(uv.y - dy))).x;
	uv.x = saturate(uv.x + xoff / 20);
	uv.y = saturate(uv.y + yoff / 20);
	float4 color = tex2D(BackgroundSampler, uv);
	color.rgb += (xoff + yoff) / 2;
	return color;
}