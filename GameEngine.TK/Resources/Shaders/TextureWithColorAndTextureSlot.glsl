#shader vertex
#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aColor;
layout (location = 3) in float aIndex;
out vec2 texCoord;
out vec4 color;
out float texIndex;

void main() 
{
	color = vec4(aColor.rgb, 1.0);
	texIndex = aIndex;
	texCoord = aTexCoord;
	gl_Position = vec4(aPosition.xyz, 1.0);
}

#shader fragment
#version 330 core
out vec4 outputColor;
in vec2 texCoord;
in vec4 color;
in float texIndex;
uniform sampler2D u_Texture[2];

void main() 
{
	int index = int(texIndex);
	outputColor = texture(u_Texture[index], texCoord) * color;
}