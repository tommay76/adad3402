float cellSize = 15;
int i;
int gridSize = 20;
boolean coinToss;
float t;
void setup(){
  noStroke();
  size(1500,1000);
  background(255);
  fill (0);
  rectMode(CENTER);
  ellipseMode(CENTER);
  t = 0;
  i = 0;
}

void draw(){
  if (i == 0){
    t = random(0,999);
    coinToss = (random(0,2)>1)?true:false;
    if (coinToss) fill(255,160,160,160);
    else fill(160,160,255,160);
  }
  pushMatrix();
  float xd = (cellSize);
  float yd = ( noise(t) * 1000 - noise(t-0.01) * 1000);
  println("xd: "+xd);
  println("yd: " + yd);
  println("d: " + xd / yd);
  
  translate((cellSize / 2)+i*cellSize,noise(t)*1000);
  rotate(-atan(xd / yd));
  rect(0,0,cellSize/2,cellSize/2);
  popMatrix();
  //noise(t)*100;
  t+= 0.01;
  i ++;
  if (i * cellSize > 1500){
    i = 0;
  }
}
