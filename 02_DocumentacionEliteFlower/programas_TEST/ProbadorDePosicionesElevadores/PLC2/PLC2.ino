  /*
  PLC 2 - Programa de pruebas posiciones elevadores

  Este programa establece una comunicacion con el cliente
  por medio del protocolo de comunicacion ModBus.
  
  Funciona con un P1AM-100 con los siguientes modulos
   - SLOT 1: P1-15TD2
   - SLOT 2: P1-15TD2
   - SLOT 3: P1-15TD2
   - SLOT 4: P1-15TD2
   - SLOT 5: P1-16NE3
   - SLOT 6: P1-16NE3
   - SLOT 7: P1-16ND3


  El programa se envarga de hacer el homing de los 5 elevadores,
  enviar las posicones de destino que tiene cada uno programado
  y recibir las interaciones de entradas.
   _____ _____ _____ _____ _____ _____ _____ _____ 
  |  P  |  S  |  S  |  S  |  S  |  S  |  S  |  S  |
  |  1  |  L  |  L  |  L  |  L  |  L  |  L  |  L  |
  |  A  |  O  |  O  |  O  |  O  |  O  |  O  |  O  |
  |  M  |  T  |  T  |  T  |  T  |  T  |  T  |  T  |
  |  -  |  -  |  -  |  -  |  -  |  -  |  -  |  -  |
  |  1  |  0  |  0  |  0  |  0  |  0  |  0  |  0  |
  |  0  |  1  |  2  |  3  |  4  |  5  |  6  |  7  |
  |  0  |     |     |     |     |     |     |     |
   ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ 

  SLOT 1: 
    - 01 al 09 el CTRG, POS0 y POS1 de los tres elevadores del elevador uno
    respectivamente.
    - 10 al 14 el CTRG, POS0, POS1, POS2 y POS3 del elevador superior en el
    mesanin.
    - 15 indica que esta recibiendo datos.
  SLOT 2:
    - 01 al 09 el CTRG, POS0 y POS1 de los tres elevadores del elevador dos
    respectivamente.
    - 10 al 12 el CTRG, POS0 y POS1 del elevador que sube las cajas al
    mesanin.
    - 13 indica que se esta estableciendo comunicacion con el P1AM-100.
    - 14 indica que no se tiene Hardware de Ethernet.
    - 15 indica que ModBus puede recibir datos.
  SLOT 3:
    - 01 al 10 indica si los 10 elevadores se encuentran en la posicion de inicio
    - 11 indica si el elevador del mesanin se encuentra en la posicion superior
    - 12 y 13 indica si el piston del elevador del mesanin se encuentra adentro
    o afuera respectivamente.
    - 12 y 13 indica si el piston del empujador de los pulmones se encuentra adentro
    o afuera respectivamente.
  SLOT 5:
    - 01 al 09 indica si los nueve pulmones tienen una presencia de una caja.
    - 11 indica si el sorter tiene una presencia de una caja.
  SLOT 7:
    - 01 al 04 indica si los grupos de servodrives estan iniciados.
    - 05 al 08 indica si los grupos de servodrives se les hizo home.
    - 09 al 12 indica si los grupos de servodrives estan alarmados.


  Creado por:         Santiago Jiménez
  Fecha creacion:     22/02/2021
  Fecha modificacion: 05/03/2021
  Written by AUTOMATE
  Copyright (c) 2021 AUTOMATE, LLC
*/

#include <SPI.h>
#include <Ethernet.h>
#include <P1AM.h>
#include <ArduinoRS485.h> // ArduinoModbus depends on the ArduinoRS485 library
#include <ArduinoModbus.h>

byte mac[] =      {0x60, 0x52, 0xD0, 0x06, 0x8F, 0x4C};
IPAddress       ip(169, 254, 1, 238);
IPAddress    myDns(169, 254, 1,   1);
IPAddress  gateway(169, 254, 1,   1);
IPAddress   subnet(255, 255, 0,   0);
EthernetServer server(502);
EthernetServer data(25);
ModbusTCPServer modbusTCPServer;

// Definicion de los SLOTS del P1AM
#define SLDELEVADOR1  1
#define SLDELEVADOR2  2
#define SLDELEVADOR3  3
#define SLDELEVADOR4  4
#define ENTELEVADOR1  5
#define ENTELEVADOR2  6
#define ENTELEVADOR3  7
// Definicion de las direcciones del ModBus
#define ADDRCOIL      0x00
// Cantidad de registros de COILS y HREG
#define NUMCOILS      60
#define NUMREGS       2
// Inicio y final de COILS en banda 1 y banda 2
#define INITSLOT1     0
#define ENDSLOT1      22
#define INITSLOT2     21
#define ENDSLOT2      38
#define INITSLOT3     37
#define ENDSLOT3      60
// Posiciones en el SLOT de los elevadores
#define ELEVADOR1     0x00
#define ELEVADOR2     0x03
#define ELEVADOR3     0x06
#define ELEVADOR4     0x09
// Tiempo de espera entre datos para el servodrive
#define INTERVALO1    20
#define INTERVALO2    30
//-- Posiciones de elevadores para el servodrive
#define OFF           0
#define CTRG          4
#define POS1          1
#define POS1CTRG      5
#define POS2          2
#define POS2CTRG      6
#define POS3          3
#define POS3CTRG      7
// Posiciones mesanin
#define MCTRG         16
#define MPOS1         1
#define MPOS1CTRG     17
#define MPOS2         2
#define MPOS2CTRG     18
#define MPOS3         3
#define MPOS3CTRG     19
#define MPOS4         4
#define MPOS4CTRG     20
#define MPOS5         5
#define MPOS5CTRG     21
#define MPOS6         6
#define MPOS6CTRG     22
#define MPOS7         7
#define MPOS7CTRG     23
#define MPOS8         8
#define MPOS8CTRG     24
#define MPOS9         9
#define MPOS9CTRG     25

#define maskSON       0x01E0
int stateSLOT4 = 0x0005;

// Inicializacion variables
unsigned long tiempo1 = 0;
bool MemoriaDI3 = LOW;
int servo1 = 0;
boolean MB_C[NUMCOILS];        //Modbus Coil Bits
int channelTwo = 0;

// Funciones
void updateCoilsSlot1();
void updateCoilsSlot2();
void updateCoilsSlot3();
int getValue(int value, int posElevator);
void setPositionServo(int index, int pos, int poshome, int elevatorN, int slot, int regCoil);
void setPositionServo(int index, int poshome, int elevatorN, int slot, int regCoil);

void setup()
{
  // Inicializa la conexion de Ethernet
  pinMode(LED_BUILTIN,OUTPUT);
  Ethernet.begin(mac, ip, myDns, gateway, subnet);
  // Espera hasta que la comunicacion serial con P1AM se establesca
  while (!P1.init())
  {
    digitalWrite(LED_BUILTIN, HIGH);
    delay(1000);
    digitalWrite(LED_BUILTIN, LOW);
    delay(1000);
  }
  digitalWrite(LED_BUILTIN, LOW);
  P1.writeDiscrete(LOW, SLDELEVADOR2, 13);
  // Verifica que se tenga conexion a Internet
  if (Ethernet.hardwareStatus() == EthernetNoHardware)
  {
    while (true)
    {
      digitalWrite(LED_BUILTIN, HIGH);
      delay(500);
      digitalWrite(LED_BUILTIN, LOW);
      delay(500);
    }
    digitalWrite(LED_BUILTIN, LOW);
  }
  // Inicia el servidor en el puerto 25
  data.begin();
  // Inicia el servidor en el puerto 502
  server.begin();
  // start the Modbus TCP server
  if (!modbusTCPServer.begin())
  {
    while (true)
    {
      digitalWrite(LED_BUILTIN, HIGH);
      delay(250);
      digitalWrite(LED_BUILTIN, LOW);
      delay(250);
    }
    digitalWrite(LED_BUILTIN, LOW);
  }
  // Configura las direcciones para las bobinas y registros
  modbusTCPServer.configureCoils(ADDRCOIL, NUMCOILS);
  digitalWrite(LED_BUILTIN, HIGH);
}

void loop()
{
  // Espera que se establesca la comunicacion con el cliente
  EthernetClient client = server.available();
  if (client)
  {
    // Espera hasta que el cliente mande el primer bit y acepta los TCP requests
    modbusTCPServer.accept(client);
    stateSLOT4 = stateSLOT4 ^ maskSON;
    P1.writeDiscrete(stateSLOT4, SLDELEVADOR4);
    // P1.writeDiscrete(HIGH, SLDELEVADOR1, 15);
    while (client.connected())
    {
      modbusTCPServer.poll();
      //-- Actualizar registros
      //------ SLOT 1
      updateCoilsSlot1();
      //---- Estacion 1
      //-- Elevador 1
      setPositionE11(0);
      setPositionE11(1);
      setPositionE11(2);
      setPositionE11(3);
      //-- Elevador 2
      setPositionE12(0);
      setPositionE12(1);
      setPositionE12(2);
      setPositionE12(3);
      //-- Elevador 3
      setPositionE13(0);
      setPositionE13(1);
      setPositionE13(2);
      setPositionE13(3);
      //-- Alimentador
      setPositionSorter(0);
      setPositionSorter(1);
      setPositionSorter(2);
      setPositionSorter(3);
      setPositionSorter(4);
      setPositionSorter(5);
      setPositionSorter(6);
      setPositionSorter(7);
      setPositionSorter(8);
      setPositionSorter(9);
      //------ SLOT 2
      updateCoilsSlot2();
      //---- Estacion 2
      //-- Elevador 1
      setPositionE21(0);
      setPositionE21(1);
      setPositionE21(2);
      setPositionE21(3);
      //-- Elevador 2
      setPositionE22(0);
      setPositionE22(1);
      setPositionE22(2);
      setPositionE22(3);
      //-- Elevador 3
      setPositionE23(0);
      setPositionE23(1);
      setPositionE23(2);
      setPositionE23(3);
      //-- Elevador alimentador
      setPositionEM1(0);
      setPositionEM1(1);
      setPositionEM1(2);
      setPositionEM1(3);
      //------ SLOT 3
      updateCoilsSlot3();
      //---- Estacion 3
      //-- Elevador 1
      setPositionE31(0);
      setPositionE31(1);
      setPositionE31(2);
      setPositionE31(3);
      //-- Elevador 2
      setPositionE32(0);
      setPositionE32(1);
      setPositionE32(2);
      setPositionE32(3);
      //-- Elevador 3
      setPositionE33(0);
      setPositionE33(1);
      setPositionE33(2);
      setPositionE33(3);
      //-- Empujador
      setPositionPiston(0);
      setPositionPiston(1);
      setPositionPiston(2);
      setPositionPiston(3);
      setPositionPiston(4);
      setPositionPiston(5);
      setPositionPiston(6);
      setPositionPiston(7);
      setPositionPiston(8);
      setPositionPiston(9);
    }
    stateSLOT4 = stateSLOT4 ^ maskSON;
    P1.writeDiscrete(stateSLOT4, SLDELEVADOR4);
  }
}

void setPositionServo(int indexT, int pos, int poshome, int elevatorN, int slot, int regCoil)
{
  if (MB_C[indexT])
  {
    servo1 = getValue(pos, elevatorN);
    P1.writeDiscrete(servo1, slot);
    tiempo1 = millis();
    while (millis() < tiempo1 + INTERVALO1)
    {
      ;
    }
    servo1 = getValue(poshome, elevatorN);
    P1.writeDiscrete(servo1, slot);
    tiempo1 = millis();
    while (millis() < tiempo1 + INTERVALO2)
    {
      ;
    }
    servo1 = getValue(OFF, elevatorN);
    P1.writeDiscrete(servo1, slot);
    MB_C[indexT] = modbusTCPServer.coilWrite(regCoil, LOW);
    tiempo1 = millis();
  }
}

void setHomeServo(int indexT, int poshome, int elevatorN, int slot, int regCoil)
{
  if (MB_C[indexT])
  {
    servo1 = getValue(poshome, elevatorN);
    P1.writeDiscrete(servo1, slot);
    tiempo1 = millis();
    while (millis() < tiempo1 + INTERVALO2)
    {
      ;
    }
    servo1 = getValue(OFF, elevatorN);
    P1.writeDiscrete(servo1, slot);
    MB_C[indexT] = modbusTCPServer.coilWrite(regCoil, LOW);
    tiempo1 = millis();
  }
}

void updateCoilsSlot1()
{
  //Read the Coil bits from the Modbus Library
  for (int i = INITSLOT1; i < ENDSLOT1; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

void updateCoilsSlot2()
{
  //Read the Coil bits from the Modbus Library
  for (int i = INITSLOT2; i < ENDSLOT2; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

void updateCoilsSlot3()
{
  //Read the Coil bits from the Modbus Library
  for (int i = INITSLOT3; i < ENDSLOT3; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

int getValue(int value, int posElevator) {
  return value << posElevator;
}



//-- Actuadores SLOT 1
void setPositionE11(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    ( 0, CTRG,           ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  0); break;
    case 1: setPositionServo( 1, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  1); break;
    case 2: setPositionServo( 2, POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  2); break;
    case 3: setPositionServo( 3, POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  3); break;
    default: setPositionServo( 1, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  1); break;
  }
}

void setPositionE12(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    ( 4, CTRG,           ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  4); break;
    case 1: setPositionServo( 5, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  5); break;
    case 2: setPositionServo( 6, POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  6); break;
    case 3: setPositionServo( 7, POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  7); break;
    default: setPositionServo( 5, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  5); break;
  }
}

void setPositionE13(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (  8, CTRG,           ELEVADOR3, SLDELEVADOR1, ADDRCOIL +   8); break;
    case 1: setPositionServo(  9, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +   9); break;
    case 2: setPositionServo( 10, POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +  10); break;
    case 3: setPositionServo( 11, POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +  11); break;
    default: setPositionServo(  9, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +   9); break;
  }
}

void setPositionSorter(int posSorter){
  switch(posSorter) {
    case 0: setHomeServo    (12, CTRG,             ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 12); break;
    case 1: setPositionServo(13, MPOS1, MPOS1CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 13); break;
    case 2: setPositionServo(14, MPOS2, MPOS2CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 14); break;
    case 3: setPositionServo(15, MPOS3, MPOS3CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 15); break;
    case 4: setPositionServo(16, MPOS4, MPOS4CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 16); break;
    case 5: setPositionServo(17, MPOS5, MPOS5CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 17); break;
    case 6: setPositionServo(18, MPOS6, MPOS6CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 18); break;
    case 7: setPositionServo(19, MPOS7, MPOS7CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 19); break;
    case 8: setPositionServo(20, MPOS8, MPOS8CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 20); break;
    case 9: setPositionServo(21, MPOS9, MPOS9CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 21); break;
    default: setPositionServo(17, MPOS5, MPOS5CTRG,  ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 17); break;
  }
}

//-- Actuadores SLOT 2
void setPositionE21(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (22, CTRG,           ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 22); break;
    case 1: setPositionServo(23, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 23); break;
    case 2: setPositionServo(24, POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 24); break;
    case 3: setPositionServo(25, POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 25); break;
    default: setPositionServo(23, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 23); break;
  }
}

void setPositionE22(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (26, CTRG,           ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 26); break;
    case 1: setPositionServo(27, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 27); break;
    case 2: setPositionServo(28, POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 28); break;
    case 3: setPositionServo(29, POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 29); break;
    default: setPositionServo(27, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 27); break;
  }
}

void setPositionE23(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (30, CTRG,           ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 30); break;
    case 1: setPositionServo(31, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 31); break;
    case 2: setPositionServo(32, POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 32); break;
    case 3: setPositionServo(33, POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 33); break;
    default: setPositionServo(31, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 31); break;
  }
}

void setPositionEM1(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (34, CTRG,           ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 34); break;
    case 1: setPositionServo(35, POS1, POS1CTRG, ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 35); break;
    case 2: setPositionServo(36, POS2, POS2CTRG, ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 36); break;
    case 3: setPositionServo(37, POS3, POS3CTRG, ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 37); break;
    default: setPositionServo(35, POS1, POS1CTRG, ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 35); break;
  }
}

//-- Actuadores SLOT 3
void setPositionE31(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (48, CTRG,           ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 48); break;
    case 1: setPositionServo(49, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 49); break;
    case 2: setPositionServo(50, POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 50); break;
    case 3: setPositionServo(51, POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 51); break;
    default: setPositionServo(49, POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 49); break;
  }
}

void setPositionE32(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (52, CTRG,           ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 52); break;
    case 1: setPositionServo(53, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 53); break;
    case 2: setPositionServo(54, POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 54); break;
    case 3: setPositionServo(55, POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 55); break;
    default: setPositionServo(53, POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 53); break;
  }
}

void setPositionE33(int posElevator){
  switch(posElevator){
    case 0: setHomeServo    (56, CTRG,           ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 56); break;
    case 1: setPositionServo(57, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 57); break;
    case 2: setPositionServo(58, POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 58); break;
    case 3: setPositionServo(59, POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 59); break;
    default: setPositionServo(57, POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 57); break;
  }
}

void setPositionPiston(int posPiston){
  switch(posPiston) {
    case 0: setHomeServo    (38, MCTRG,             ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 38); break;
    case 1: setPositionServo(39, MPOS1, MPOS1CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 39); break;
    case 2: setPositionServo(40, MPOS2, MPOS2CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 40); break;
    case 3: setPositionServo(41, MPOS3, MPOS3CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 41); break;
    case 4: setPositionServo(42, MPOS4, MPOS4CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 42); break;
    case 5: setPositionServo(43, MPOS5, MPOS5CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 43); break;
    case 6: setPositionServo(44, MPOS6, MPOS6CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 44); break;
    case 7: setPositionServo(45, MPOS7, MPOS7CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 45); break;
    case 8: setPositionServo(46, MPOS8, MPOS8CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 46); break;
    case 9: setPositionServo(47, MPOS9, MPOS9CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 47); break;
    default: setPositionServo(43, MPOS5, MPOS5CTRG,  ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 43); break;
  }
}

void setDownElevator(int posElev) {
  switch(posElev) {
    case 1: setPositionE11(1); break;
    case 2: setPositionE12(1); break;
    case 3: setPositionE13(1); break;
    case 4: setPositionE21(1); break;
    case 5: setPositionE22(1); break;
    case 6: setPositionE23(1); break;
    case 7: setPositionE31(1); break;
    case 8: setPositionE32(1); break;
    case 9: setPositionE33(1); break;
    default: setPositionE33(1); break;
  }
}
