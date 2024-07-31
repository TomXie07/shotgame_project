#version 330 core

uniform vec2 iResolution;
uniform float iTime;
uniform int iFrame;



in vec3 vtx_pos; // [-1, 1]
in vec2 vtx_uv; // [0, 1]

out vec4 frag_color;

#define NUM_STAR 100.

// return random vec2 between 0 and 1
vec2 hash2d(float t)
{
    t += 1.0;
    float x = fract(cos(t * 837.1) * 682.1);
    float y = fract(cos((t + x) * 956.1) * 173.3);

    return vec2(x, y);
}

vec3 renderParticle(vec2 uv, vec2 pos, float brightness, vec3 color)
{
    float d = length(uv - pos);
    return brightness / d * color;
}

vec3 renderStars(vec2 uv)
{
    vec3 fragColor = vec3(0.0);

    float t = iTime;
    for(float i = 0.; i < NUM_STAR; i++)
    {
        vec2 pos = hash2d(i) * 2. - 1.; // map to [-1, 1]
        float brightness = .0015;
        brightness *= sin(1.5 * t + i) * .5 + .5; // flicker
        vec3 color = vec3(0.15, 0.71, 0.92);

        fragColor += renderParticle(uv, pos, brightness, color);
    }

    return fragColor;
}

void main()
{
    vec3 outputColor = renderStars(vtx_pos.xy);

    vec2 uv = vec2(vtx_uv.x, -vtx_uv.y);
    vec3 buzzColor = vec3(0);

    frag_color = vec4(mix(outputColor, buzzColor, (sin(iTime) + 1) * .5 * .2), 1.0);
}