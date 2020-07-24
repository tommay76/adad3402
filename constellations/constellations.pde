
import peasy.PeasyCam;



PeasyCam cam;
JSONObject constellationJSON;
JSONArray constellationArray;
int rows;
int cols;
// [i] = constellation id, [i][j] = star id within constellation, 
// [i][j][0] = Right ascension (longitude measured in hours)
// [i][j][1] = Declination (angle to North/South pole in degrees
float starDictionary[][][];
int lineGuide[][][];
void setup(){
  colorMode(HSB,360,100,100);
  size(1000,1000,P3D);
  constellationJSON = loadJSONObject("constellations.json");
  cam = new PeasyCam(this, 150);
  constellationArray = constellationJSON.getJSONArray("Constellations");
  rows = ceil(sqrt(constellationArray.size()));
  cols=rows;
  starDictionary = new float[constellationArray.size()][][];
  lineGuide = new int[constellationArray.size()][][];
  for (int i =0; i < constellationArray.size(); i++){
    //println("i =",i);
    JSONObject poo = constellationArray.getJSONObject(i);
    if (poo.getString("Name").equals("Crux")) println(i,i,i,i);
    JSONArray starArray = poo.getJSONArray("stars");
    starDictionary[i] = new float[starArray.size()][2];
  
    for (int j =0; j < starArray.size(); j++){  
      //println("j =",j);
      JSONObject star = starArray.getJSONObject(j);
      starDictionary[i][j][0] = radians(15*(star.getFloat("RAh")));
      starDictionary[i][j][1] = radians(star.getFloat("DEd"));  
    }
    JSONArray lineArray = poo.getJSONArray("lines");
    lineGuide[i] = new int[lineArray.size()][2];
    for (int j =0; j < lineArray.size(); j++){
      lineGuide[i][j][0] = lineArray.getJSONArray(j).getInt(0);  
      lineGuide[i][j][1] = lineArray.getJSONArray(j).getInt(1);  
    }
      
    
  }
  println(rows*cols);
  println("STAR 1 POS:",starDictionary[0][0][0],starDictionary[0][0][1]);
}
void draw(){
  fill(0,100,100);
  textSize(12);
  background(0);
  //text("X",50,0,0);
  //text("-X",-50,0,0);
  text("S",0,50,0);
  text("N",0,-50,0);
  //text("Z",0,0,50);
  //text("-Z",0,0,-50);
  stroke(255);
  strokeWeight(1);
  noFill();
  rotateX(PI/2);
  stroke(0,100,100);
  ellipse(0,0,100,100);
  rotateX(-PI/2);
  stroke(180,100,100);
  for (int i = 0; i < 12; i ++){
    rotateY(PI/6);
    ellipse(0,0,100,100);
  }
  //for (int i =0; i < 2; i++){
  //  if (!(i == 0 || i == 2 )) ;
  //  stroke(map(i,0,starDictionary.length,0,360),0,map(i,0,starDictionary.length,100,0));
  //  for (int j =0; j < starDictionary[i].length; j++){
  //    pushMatrix();
  //    rotateY(starDictionary[i][j][0]);
  //    rotateZ(starDictionary[i][j][1]);
  //    translate(50,0,0);
  //    strokeWeight(5);
  //    point(0,0,0);
  //    popMatrix();
  //  }
  //  for (int j =0; j < lineGuide[i].length; j++){
  //    pushMatrix();
  //    PShape line = createShape(LINE);
  //    //scale(1,-1,1);
  //    int star1 = lineGuide[i][j][0];
  //    int star2 = lineGuide[i][j][1];
  //    float r = 50;
  //    float t1 = starDictionary[i][star1][0];
  //    float s1 = starDictionary[i][star1][1];
  //    float z1 = -r * cos(s1) * sin(t1);
  //    float y1 = -r * sin(s1) * sin(t1);
  //    float x1 = r * cos(t1);
  //    float t2 = starDictionary[i][star2][0];
  //    float s2 = starDictionary[i][star2][1];
  //    float z2 = -r * cos(s2) * sin(t2);
  //    float y2 = -r * sin(s2) * sin(t2);
  //    float x2 = r * cos(t2);
  //    strokeWeight(0.5);
  //    line(x1,y1,z1,x2,y2,z2);
  //    popMatrix();
  //    //if (t1 < 0 || t2 < 0){exit();}
  //  }
  //}  
  
  for (int i =0; i < starDictionary.length; i++){
    if (!(i == 0 || i == 2 )) ;
    stroke(map(i,0,starDictionary.length,0,360),0,map(i,0,starDictionary.length,100,0));
    for (int j =0; j < starDictionary[i].length; j++){
      pushMatrix();
      rotateY(starDictionary[i][j][0]);
      rotateX(starDictionary[i][j][1]);
      //println("STAR 1 POS:",starDictionary[i][j][0],starDictionary[i][j][1]);
      translate(0,0,50);
      strokeWeight(5);
      point(0,0,0);
      popMatrix();
    }
    for (int j =0; j < lineGuide[i].length; j++){
      pushMatrix();

      //scale(1,-1,1);
      int star1 = lineGuide[i][j][0];
      int star2 = lineGuide[i][j][1];
      float r = 60;
      
      float s1 = starDictionary[i][star1][0];
      float t1 = radians(90)-abs(starDictionary[i][star1][1]);
      
      float x1 = r * sin(s1) * sin(t1);
      float y1 = (starDictionary[i][star1][1]>0)? -r * cos(t1): r * cos(t1);
      float z1 = r * cos(s1) * sin(t1);
      
      float s2 = starDictionary[i][star2][0];
      float t2 = radians(90)-abs(starDictionary[i][star2][1]);
      
      float x2 = r * sin(s2) * sin(t2);
      float y2 = (starDictionary[i][star2][1]>0)?-r * cos(t2):r * cos(t2);
      float z2 = r * cos(s2) * sin(t2);
      //print(x1,y1,z1);
      strokeWeight(0.5);

      line(x1,y1,z1,x2,y2,z2);
      popMatrix();
      //if (t1 < 0 || t2 < 0){exit();}
    }
  } 
  
  
  
  
  
  //exit();
}
