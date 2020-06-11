// Tom Doyle
// 07/06/2020
// Makes a generative tree

Tree t;
ArrayList<Tree> trees;

int timer;
int MAXDEPTH = 16;
boolean showText;
void setup(){
  colorMode(HSB,360,100,100);
  fullScreen(P3D);
  background (200,0,100);
  //blueSky(10);
  pushMatrix();
  translate(width/2,height*0.9);
  drawGrass(100,100,40);
  popMatrix();
  noStroke();
  t = new Tree(0,0,0,30,true, 0);
  rectMode(CENTER);
  ellipseMode(RADIUS);
  timer = 0;
  showText = true;
  trees = new ArrayList<Tree> ();
  int j = (int)random(0,10);
  for (int i = 0; i < j; i ++){
    trees.add(new Tree((int)random(-width/2,width/2),0,(int)random(-width/2,width/2),30,true, 0));
  }
}

void draw (){
  fill(15);
  textSize(20);
  if (showText)text("'q' to quit. \n'r' to reset tree.\n't' to show/hide text.\n's' to screenshot.",20,50);
  //timer++;
  if (timer %10 != 0) return;
  pushMatrix();
  scale(1,-1);
  translate(width/2,-height*0.9);
  //drawGrass(100,100,20);
  t.grow(t.growing);
  for ( Tree b: trees){
    pushMatrix();
    translate(b.positionX,b.positionY,b.positionZ);
    b.grow(b.growing);
    popMatrix();
  }
  popMatrix();
}
void keyReleased(){
  if (key =='r'){
    boolean temp = showText;
    setup();
    showText = temp;
  }
  if (key =='q'){
    exit(); 
  }
  if (key =='t'){
    showText = !showText; 
  }
  if (key =='s'){
    save("test##.png"); 
  }
}
void mousePressed(){
  boolean temp = showText;
  setup();
  showText = temp;
  
}

class Tree {
  int treeHeight;
  int positionX;
  int positionY;
  int positionZ;
  boolean main;
  int thickness;
  boolean growing;
  int[] branchColor;
  int[] leafColor;
  ArrayList<Tree> branches;
  int depth;
  Tree (int posX, int posY, int posZ, int t, boolean m, int d){
    positionX = posX;
    positionY = posY;
    positionZ= posZ;
    thickness = t;
    main = m;
    treeHeight = 0;
    branches = new ArrayList<Tree>();
    growing = true;
    branchColor = new int[]{(int)random(0,360),(int)random(0,100),(int)random(0,75)};
    leafColor = new int[]{(int)random(0,360),(int)random(0,100),(int)random(branchColor[2],100)};
    depth = d;
  }
  void grow(boolean c){
    if (growing){
      growMain(c);
    }
    growBranches(c);
  }
  
  void growMain(boolean g){
    
    if (treeHeight==0){
      drawBase();
    }
    fill (branchColor[0],branchColor[1],branchColor[2]);
    pushMatrix();
    translate(0,thickness/2+thickness*treeHeight,positionZ);
    box(thickness);
    popMatrix();
    treeHeight++;
    int dice = (int)random(0,10);
    Tree branch = null;
    if(dice ==0 && treeHeight > 3){
      endTree();
    }else if (dice < 2 && g && depth < MAXDEPTH){
      branch = new Tree(-thickness/2, thickness/2 +thickness * treeHeight, positionZ, thickness, false, depth + 1);
    }else if (dice < 3 && g && depth < MAXDEPTH){
      branch = new Tree(thickness/2, thickness/2 +thickness * treeHeight, positionZ, thickness, false, depth + 1);
    }
    if (branch != null){
      branches.add(branch);
    }
    
  }
  
  void growBranches(boolean c) throws RuntimeException {
    for (Tree i : branches) {
      try {pushMatrix();}
      catch (RuntimeException e){
        endTree();
        return;
      }
      translate(i.positionX,i.positionY,i.positionZ);
      if (i.positionX > 0){
        rotate(PI/3);
      }else rotate(-PI/3);
      scale(0.25);
      i.grow(c);
      popMatrix();
    }
  }
  
  void drawBase(){
    //if (main){
    //  fill (branchColor[0],branchColor[1],branchColor[2]);
    //  triangle(thickness/2,0,thickness,0,0,thickness);
    //  triangle(-thickness/2,0,-thickness,0,0,thickness);
    //}
  }
  void endTree(){
    int poofSize = (int)random(thickness*2,thickness*treeHeight*.5);
    fill(leafColor[0],leafColor[1],leafColor[2]);
    pushMatrix();
    if (poofSize > thickness * treeHeight) poofSize = thickness* (treeHeight-1);
    translate(0,thickness+thickness*treeHeight,positionZ);
    sphere(poofSize);
    popMatrix();
    growing = false;
  }
}
void drawGrass(int lengthX, int lengthZ, int unitLength){
  
  fill(160,80,80);
  stroke(150,100,100);
  strokeWeight(1);
  pushMatrix();
  translate(-width,0,-width);
  
  for (int i = 0; i < lengthX-1;i++){
    beginShape(TRIANGLE_STRIP);
    for (int j = 0; j < lengthZ-1;j++){
      vertex(j*unitLength,0,i*unitLength);
      vertex(j*unitLength,0,(i+1)*unitLength);
    }
    endShape();
  }
  
  popMatrix();
}
void blueSky(int num){
  int sat = 0;
  noStroke();
  for (int i = 0; i < num; i ++){
    fill(200,100-sat,100);
    translate(0,0,-2000);
    rect(-width*1.5,i*(height*2/num)-1200,width*4,(i+1)*(height*2/num)-1200);
    translate(0,0,2000);
    sat+=100/num;
  }
}
