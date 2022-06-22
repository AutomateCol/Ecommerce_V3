/*
  PLC 1

  Este programa establece una comunicacion con el cliente
  por medio del protocolo de comunicacion ModBus.

  Funciona con un P1AM-100 con los siguientes modulos
   - SLOT 1: P1-15TD2
   - SLOT 2: P1-15TD2
   - SLOT 3: P1-04DAL-2

  El programa se encarga de encender/apagar los pilotos de los elevadores,
  activar/desactivar las bandas y asignar el voltaje de referencia para los
  variadores de velocidad.
   _____  _____ _____  _____
  |  P  ||  S  |  S  ||  S  |
  |  1  ||  L  |  L  ||  L  |
  |  A  ||  O  |  O  ||  O  |
  |  M  ||  T  |  T  ||  T  |
  |  -  ||  -  |  -  ||  -  |
  |  1  ||  0  |  0  ||  0  |
  |  0  ||  1  |  2  ||  3  |
  |  0  ||     |     ||     |
   ¯¯¯¯¯  ¯¯¯¯¯ ¯¯¯¯¯  ¯¯¯¯¯

  SLOT 1:
    - 01 al 09 pilotos de la banda 1
    - 10 al 12 pilotos de los addon banda 1
    - 13  y 14 activar bandas 1 y 2
    - 15 indica que esta recibiendo datos
  SLOT 2:
    - 01 al 09 pilotos de la banda 2
    - 10 al 12 pilotos de los addon banda 2
    - 13 indica que se esta estableciendo comunicacion con el P1AM
    - 14 indica que no se tiene Hardware de Ethernet
    - 15 indica que ModBus puede recibir datos
  SLOT 3:
    - 01 para enviar consigna de voltaje motor 1
    - 02 para enviar consigna de voltaje motor 2

  Para confirmar la salida analogica puede usar un multimetro.

  Creado por:         Santiago Jiménez
  Fecha creacion:     19/02/2021
  Fecha modificacion: 08/03/2021
  Written by AUTOMATE
  Copyright (c) 2021 AUTOMATE, LLC
*/

#include <SPI.h>
#include <Ethernet.h>
#include <P1AM.h>
#include <ArduinoRS485.h>       // ArduinoModbus depends on the ArduinoRS485 library
#include <ArduinoModbus.h>

byte mac[] =    {0x60, 0x52, 0xD0, 0x06, 0x8F, 0x1D};   //* MAC de la tarjeta Ethernet
IPAddress       ip(169, 254, 1, 236);                   //* IP estatica
IPAddress       myDns(169, 254, 1,   1);                //* DNS
IPAddress       gateway(169, 254, 1,   1);              //* Gateway
IPAddress       subnet(255, 255, 0,   0);               //* Mascara
EthernetServer  server(502);                            //* Puerto de ModBus
ModbusTCPServer modbusTCPServer;

// Definicion de los SLOTS del P1AM
#define SLDBANDA1       1         //-- SLOT para la banda 1
#define SLDBANDA2       2         //-- SLOT para la banda 2
#define SLDANALOGA      3         //-- SLOT para voltaje de referencia de bandas
// Definicion de las direcciones del ModBus
#define ADDRCOIL        0x00      //-- Direccion de inicio de los COILS
#define ADDRMOTOR1      0x31      //-- Direccion para activar/desactivar banda 1
#define ADDRMOTOR2      0x32      //-- Direccion para activar/desactivar banda 2
// Cantidad de registros de COILS y HREG
#define NUMCOILS        26        //-- Numero de COILS
#define NUMREGS         2         //-- Numero de Holding Registers
// Inicio y final de COILS en banda 1 y banda 2
#define INITBANDA1      00        //-- COIL desde donde empieza el SLOT 1
#define FINBANDA1       15        //-- COIL donde acaba el SLOT 1
#define INITBANDA2      14        //-- COIL desde donde empieza el SLOT 2
#define FINBANDA2       26        //-- COIL donde acaba el SLOT 2

#define _PP(a) Serial.print(a);
#define _PL(a) Serial.println(a);

// Inicializacion variables
long MemoriaMotor1 = 0;             //-- Memoria donde guarda el voltaje para la banda 1
long MemoriaMotor2 = 0;             //-- Memoria donde guarda el voltaje para la banda 2
boolean MB_C[NUMCOILS];             //-- Modbus Coil Bits

void updateCoilsBanda1();           //-- Actualiza los registros del SLOT 1
void updateCoilsBanda2();           //-- Actualiza los registros del SLOT 2
void updateVoltageBands();          //-- Actualiza los voltajes de las bandas

void setup()
{
  // Inicializa la conexion de Ethernet
  pinMode(LED_BUILTIN, OUTPUT);
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
  modbusTCPServer.configureHoldingRegisters(ADDRMOTOR1, NUMREGS);
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
    while (client.connected())
    {
      modbusTCPServer.poll();
      updateCoilsBanda1();
      updateCoilsBanda2();
      updateVoltageBands();
    }
  }
}

void updateCoilsBanda1()
{
  for (int i = INITBANDA1; i < FINBANDA1; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
    P1.writeDiscrete(MB_C[i], SLDBANDA1, i + 1);
    //P1.writeDiscrete(1, SLDBANDA1, i + 1);
  }
}

void updateCoilsBanda2()
{
  for (int i = INITBANDA2; i < FINBANDA2; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
    P1.writeDiscrete(MB_C[i], SLDBANDA2, i - INITBANDA2 + 1);
   // P1.writeDiscrete(1, SLDBANDA2, i - INITBANDA2 + 1);
  }
}

void updateVoltageBands()
{
  if (modbusTCPServer.holdingRegisterRead(ADDRMOTOR1) != MemoriaMotor1)
  {

    MemoriaMotor1 = modbusTCPServer.holdingRegisterRead(ADDRMOTOR1);
    _PP("Velocidad Motor"); _PL(MemoriaMotor1);
    P1.writeAnalog(MemoriaMotor1, SLDANALOGA, 1);
  }
  if (modbusTCPServer.holdingRegisterRead(ADDRMOTOR2) != MemoriaMotor2)
  {
    MemoriaMotor2 = modbusTCPServer.holdingRegisterRead(ADDRMOTOR2);
    P1.writeAnalog(MemoriaMotor2, SLDANALOGA, 2);
  }
}
