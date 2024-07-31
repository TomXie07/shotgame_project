#version 330 core

layout(location = 0) in vec3 pos;			/*vertex position*/
layout(location = 1) in vec3 color; 			/*vertex uv*/
layout(location = 2) in vec2 uv; 			/*vertex uv*/

uniform vec2 iResolution;

out vec3 vtx_pos;
out vec2 vtx_uv;

void main()
{
    vtx_pos = pos.xyz;
    vtx_uv = uv.xy;
    gl_Position = vec4(pos.x,pos.y,-1, 1.0);
}