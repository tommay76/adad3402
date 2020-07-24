PVector[][] vectorField;
int cols, rows;
int cellSize;
float sizeScale = 15;
float perlinScale= .2;
float thickness = 5;
ArrayList<Vehicle> p;
int shifter;
void setup(){
  noiseSeed(5);
  colorMode(HSB,360,10,10);
  strokeWeight(thickness);
  size(1000,1000);
  background(100);
  cellSize = width/50;
  rows = width/cellSize;
  cols = height/cellSize;
  vectorField = new PVector[rows][cols];
  for (int i = 0; i < rows; i ++){
    for (int j = 0; j < cols; j ++){
        float theta = map(noise(i*perlinScale,j*perlinScale),0,1,0,TWO_PI);
        vectorField[i][j]= PVector.fromAngle(theta);
    }
  }
  for (int i = 0; i < rows; i ++){
    for (int j = 0; j < cols; j ++){
      float x = vectorField[i][j].x;
      float y = vectorField[i][j].y;
      stroke(240);
      line((i*cellSize)+cellSize/2,(j*cellSize)+cellSize/2,
          (i*cellSize)+cellSize/2+x*sizeScale,(j*cellSize)+cellSize/2+y*sizeScale);
    }
  }
  p = new ArrayList<Vehicle>();
  p.add(new Vehicle(width/2,height/2));
  shifter = 0;
}
void draw(){
  shifter++;
  //background(255);
  //drawVectors();

  
  
  stroke(0);
  
  for (Vehicle i: p){
    i.move();
  }
}
void mousePressed(){
  p.add(new Vehicle(mouseX,mouseY));
}
void drawVectors(){
   for (int i = 0; i < rows; i ++){
    for (int j = 0; j < cols; j ++){
      float x = vectorField[i][j].x;
      float y = vectorField[i][j].y;
      stroke(240);
      line((i*cellSize)+cellSize/2,(j*cellSize)+cellSize/2,
          (i*cellSize)+cellSize/2+x*sizeScale,(j*cellSize)+cellSize/2+y*sizeScale);
    }
  }
}
