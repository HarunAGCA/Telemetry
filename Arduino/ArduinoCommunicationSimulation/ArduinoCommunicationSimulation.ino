
String readedString;
bool isConnected = false;
bool isSend = true;


void setup() {
  Serial.begin(9600);

  while (!Serial) {
  }

  Serial.flush();
}

void loop() {
  checkForMonitoringCenter();
}



void checkForMonitoringCenter() {

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

   /*
           $     ==> Starting symbol
           1:80  ==> sensorId:sensorValue
           /     ==> Sensor values sperator
           #     ==> Ending symbol
    */

    if (isSend)
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
    }
}
