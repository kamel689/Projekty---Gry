int tileSize = 16;
float scl = 0.1;  //skala

PImage[] sprites = new PImage[4];


double a=81, m = Math.pow(2, 20), seed = 300;
double c = ((3-sqrt(3))/6)*m;

void setup() {
  sprites[0] = loadImage("water.png");
  sprites[1] = loadImage("sand.png");
  sprites[2] = loadImage("grass.png");
  sprites[3] = loadImage("forest.png");  
  size(1080, 720);

  
  noStroke();
  colorMode(HSB); //otrzymujemy kolory z całego zakresu dla jednej wartosci
  drawTerrain();
  
}

void draw() {
  
  
}

void keyPressed() {
  if(key == ' ') {
    noiseSeed(millis());
    drawTerrain();
  }

}

void drawTerrain() {

  for(int i = 0; i < 1000; i ++) {
    for(int j = 0; j < 1000; j ++) {
      image(sprites[getTile(i,j)], i * tileSize, j * tileSize, tileSize, tileSize);
    }
  }
  
}

double nextRand()
{
  seed = (a * seed + c) % m;
  return seed;
}

double nextRandFloat() {
  return nextRand() / m;
}

//Funkcja, która na podstawie współrzednych skorzysta z metody noise i zwróci wartosc 0 lub 1 - typ kafelka
int getTile(int x, int y) {
  double v = nextRandFloat();
  if(v < 0.35) {
    //woda - 35%
    return 0;
  } else if(v < 0.5) {
    //piasek - 15%
    return 1;
  } else if(v < 0.7) {
    //trawa - 20%
    return 2;
  } else {
    //las - 30%
    return 3;
  }
}

//Perlin Noise- umożliwia generowanie losowych liczb dla danych współrzędnych, zwraca wartosc 0 lub 1 
