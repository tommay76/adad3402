class Vehicle{
  PVector acceleration;
  PVector velocity;
  PVector position;
  float weight;
  Vehicle(float X, float Y){
    this.position = new PVector(X,Y);  
    this.velocity = new PVector();
    this.acceleration = new PVector();
    this.weight=1;
  }
  void move(){
    if (position.x <= 0) position.x  = width-1;
    else if (position.x >= width) position.x  = 0;
    if (position.y <= 0) position.y = height-1;
    else if (position.y >= height) position.y = 0;
    int row = (int)position.x/cellSize;
    int col = (int)position.y/cellSize;
    
    //println("row",row);
    //print("col",col);
    acceleration.add(vectorField[row][col]).limit(0.5);
    //acceleration= vectorField[row][col];
    velocity.add(acceleration).limit(5);
    position.add(velocity);
    //println("position",position.x,position.y);
    //println("velocity",velocity.x,velocity.y);
    stroke(00,90,100);
    point(position.x,position.y);
  }
}
