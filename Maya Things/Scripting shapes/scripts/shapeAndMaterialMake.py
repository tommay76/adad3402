import maya.cmds as mc
from random import *
myMaterials = ["myOtherSurface","myMaterial","sphereBoi"]

def applyMaterial(node):
    if mc.objExists(node):
        shd = mc.shadingNode('aiStandardSurface', name="%sSurface"%node, asShader=True)
        shdSG = mc.sets(name='%sSG' % shd, empty=True, renderable=True, noSurfaceShader=True)
        hsv = mc.shadingNode('hsvToRgb', name="%sSurfacehsvConverter"%node, asUtility=True)
        mc.connectAttr('%s.outColor' % shd, '%s.surfaceShader' % shdSG)
        mc.connectAttr('%s.outRgb' % hsv, '%s.baseColor' % shd,  f=True)
        mc.sets(node, e=True, forceElement=shdSG)

def randomizeMat(materialName):
    hue = randrange(0,360)
    saturation = uniform(0.6,1.)
    vibrance = random()
    mc.setAttr(materialName+"SurfacehsvConverter.inHsv",hue,saturation,vibrance)
    mc.setAttr(materialName+"Surface.diffuseRoughness",random())
    mc.setAttr(materialName+"Surface.metalness",random())
    mc.setAttr(materialName+"Surface.specular",random())
for i in ['a','b','c','d']:
    ii = mc.ls(i)
    if len(ii) != 0:
        mc.delete(ii)
    mc.polyCube(name=i)
    mc.move(uniform(-10,10),0,uniform(-10,10))
    applyMaterial(i)
    randomizeMat(i)