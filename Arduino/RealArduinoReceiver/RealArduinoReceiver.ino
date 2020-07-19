
#include <SoftwareSerial.h>

SoftwareSerial transmitterCenter(2, 3); // RX, TX

#define msjSonKarakter ';'
byte highSendCode = 15;//max 15
byte lowSendCode = 10;//max 15 highSendCode dan farklı olmalı


String readedString;
bool isConnected = false;
bool isSend = true;


void setup() {
  highSendCode = highSendCode << 4;
  lowSendCode = lowSendCode << 4;
  Serial.begin(9600);
  transmitterCenter.begin(9600);
  while (!Serial) {
  }
  Serial.flush();

}

void loop() {
  checkForMonitoringCenter();
  parseEncodedValue();
}


String metin = "";
void parseEncodedValue(){
  while(transmitterCenter.available() > 0){
    byte data = transmitterCenter.read();
    byte c = karakterAl(data);//c 0 dan farklıysa veri alınmistir
    if(c > 0){
      if((char)c != msjSonKarakter)// . isareti mesaj sonlandirma karakteri
        metin += (char)c;
      else{
        if(isSend){
          metin += '#';
          Serial.print(metin);
        }
        metin = "";
      }
    }
  }
}

byte veri[2];
boolean kontrol[2];
byte karakterAl(byte data){//0 dan farklı deger donduyse o karakterdir
  if((data & 0xF0) == highSendCode){
    veri[0] = data;
    kontrol[0] = true;
  }
  else if((data & 0xF0) ==  lowSendCode){
    veri[1] = data;
    kontrol[1] = true;
  }
  if(kontrol[0] & kontrol[1]){
    byte karakter = (veri[0] << 4) | (veri[1] & 0x0F);
    kontrol[0] = false;
    kontrol[1] = false;
    //sendAck();
    
    return karakter;
  }
  return 0;
}
void sendAck(){
  transmitterCenter.print((char)0xFF);
}























   /*
          $     ==> Starting symbol
          1:80  ==> sensorId:sensorValue
          /     ==> Sensor values sperator
          #     ==> Ending symbol
    */

/*  if (isSend)
  {

    Serial.print("$");
    Serial.print("1:");
    Serial.print(random(0, 140));
    Serial.print("/2:");
    Serial.print(random(0, 90));
    Serial.print("/3:");
    Serial.print(random(0, 100));
    Serial.print("/4:");
    Serial.print(random(0, 100));
    Serial.print("/5:");
    Serial.print(random(0, 100));
    Serial.print("/6:");
    Serial.print(random(0, 100));
    Serial.print("/7:");
    Serial.print(random(0, 100));
    Serial.print("/8:");
    Serial.print(random(0, 100));
    Serial.print("/9:");
    Serial.print(random(0, 100));
    Serial.print("/10:");
    Serial.print(random(0, 100));
    Serial.print("/11:");
    Serial.print(random(0, 100));
    Serial.print("/12:");
    Serial.print(random(0, 100));
    Serial.print("/13:");
    Serial.print(random(0, 100));
    Serial.print("/14:");
    Serial.print(random(0, 100));
    Serial.print("/15:");
    Serial.print(random(0, 100));
    Serial.print("#");

    delay(1000);
    

  }*/


void checkForMonitoringCenter(){
  
  if (Serial.available() > 0)
  {
    readedString = Serial.readStringUntil('#');

    if (readedString == "CONNECT")
    {
      Serial.print("CONNECTED#");
      isConnected = true;
    }
    else if (readedString == "DISCONNECT")
    {
      Serial.print("DISCONNECTED#");
      isConnected = false;
    }
    else if (readedString == "TEST")
    {
      Serial.print("TESTOK#");
    }
    else if (readedString == "SEND") {
      isSend = true;
    }
    else if (readedString == "STOP")
    {
      Serial.print("STOPPED#");
      isSend = false;
    }
    else
    {
      Serial.print("ERROR#");
      isSend = false;
    }

  }
}
