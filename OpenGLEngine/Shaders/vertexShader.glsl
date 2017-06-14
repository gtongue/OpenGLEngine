#version 400 core

in vec3 position;
in vec2 textureCoords;

out vec2 p_textureCoords;

uniform mat4 transformationMatrix;
uniform mat4 projectionMatrix;

void main(void)
{
	gl_Position =  projectionMatrix * transformationMatrix * vec4(position, 1.0);
	p_textureCoords = textureCoords;
}