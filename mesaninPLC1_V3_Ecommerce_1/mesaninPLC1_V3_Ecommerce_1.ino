/*
    MultiTask P1AM

    Este programa crear un RTOS para P1AM

    Funciona con un P1AM-100 con los siguientes modulos
        - SLOT 1: P1-15TD2
        - SLOT 2: P1-15TD2
        - SLOT 3: P1-15TD2
        - SLOT 4: P1-15TD2
        - SLOT 5: P1-16ND3
        - SLOT 6: P1-16NE3
        - SLOT 7: P1-16NE3
        - SLOT 8: P1-16NE3

    Es un RTOS, que permite ejecutar varias tareas al tiempo del
    Empujador y del Sorter, ademas de hacer el llenado inicial
    del pulmon para luego funcionar automaticamente.
    Si detecta que existe un corte de luz entonces se devuelve
    a un estado seguro, guarda la informacion y luego se apaga.
         _____ _____ _____ _____ _____ _____ _____ _____ _____
        |  P  |  S  |  S  |  S  |  S  |  S  |  S  |  S  |  S  |
        |  1  |  L  |  L  |  L  |  L  |  L  |  L  |  L  |  L  |
        |  A  |  O  |  O  |  O  |  O  |  O  |  O  |  O  |  O  |
        |  M  |  T  |  T  |  T  |  T  |  T  |  T  |  T  |  T  |
        |  -  |  -  |  -  |  -  |  -  |  -  |  -  |  -  |  -  |
        |  1  |  0  |  0  |  0  |  0  |  0  |  0  |  0  |  0  |
        |  0  |  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |
        |  0  |     |     |     |     |     |     |     |     |
         ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯ ¯¯¯¯¯
    SLOT 1:
        - 01 al 09 posiciones de los elevadores de la estacion 1
        - 10 al 14 posiciones del sorter del mesanin
    SLOT 2:
        - 01 al 09 posiciones de los elevadores de la estacion 2
        - 10 al 14 posiciones del empujador del mesanin
    SLOT 3:
        - 01 al 09 posiciones de los elevadores de la estacion 3
        - 10 al 12 posiciones del elevador del mesanin
    SLOT 4:
        - 01 y 02 activa/desactiva piston 1
        - 03 y 04 activa/desactiva piston 2
        - 05 motor del sorter
        - 06 y 09 servo ON de cada grupo de servos
    SLOT 5:
        - 01 al 09 indica si existe caja en el pulmon del mesanin
        - 10 indica si el sorter tiene una caja
        - 11 y 12 indica la posicion del piston 1
        - 13 y 14 indica la posicion del piston 2
        - 15 indica si los elevadores se encuentran alarmados
        - 16 indica si el mesanin se encuentra alarmado
    SLOT 6:
        - 01 al 09 indica si existe caja en los elevadores
        - 10 sensor de presencia caja elevador del mesanin
        - 11 al 14 indica si el grupo de servos ya termino el homing
        - 15 sensor reflex de obstruccion caja elevador del mesanin
        - 16 botones para subir elevador del mesanin
    SLOT  7:
        - 01 al 12 indica si el servo ya termino la posicion a la que se le mando
        - 13 al 16 indica si los grupos de servos estan listos para hacer home
    SLOT  8:
        - 01
        - 02
        - 03 al 05 indica si se encuentran conectados los pedales
        - 06 al 08 indica si se activaron los pedales

    Salidas disponibles:  11
    Entradas disponibles: 16

    Creado por:         Santiago Lópéz
    Modificado Por:     Santiago López
    Fecha creacion:     06/04/2021
    Fecha modificacion: 3/03/2022
    Written by AUTOMATE
    Copyright (c) 2022 AUTOMATE
*/

#include <SPI.h>
#include <Ethernet.h>
#include <P1AM.h>
#include <ArduinoRS485.h>   // ArduinoModbus depends on the ArduinoRS485 library
#include <ArduinoModbus.h>


#define _TASK_SLEEP_ON_IDLE_RUN
#define _TASK_STATUS_REQUEST
#define _TASK_PRIORITY
#define _TASK_WDT_IDS
#define _TASK_TIMECRITICAL
#include <TaskScheduler.h>

#define _DEBUG_
//#define _TEST_

#ifdef _DEBUG_
#define _PP(a) Serial.print(a);
#define _PL(a) Serial.println(a);
#else
#define _PP(a)
#define _PL(a)
#endif

struct Elevador {
  int sendValue;
  int pos;
  int coil;
};





StatusRequest SRmeasureEmp;
StatusRequest SRmeasureSortEmp;
StatusRequest SRmeasureSort;
Scheduler ts;
Scheduler hs;


// Callback methods prototypes
void tParoEmergencia();
void tLeerPedales();
void tLeerSensoresElevadores();
void tLeerSensoresPulmones();
void tMoverEmpujador();
void tSubirElevadorSorter();
void tMoverSorter();
void tCycleCallback();

// Custom tasks
int tik = 900;
// Task handler

Task tskMoverEmpujador(tik, TASK_FOREVER, &tMoverEmpujador, &ts, false);
Task tskSubirElevadorSorter(tik, TASK_FOREVER, &tSubirElevadorSorter, &ts, false);
Task tskMoverSorter(tik, TASK_FOREVER, &tMoverSorter, &ts, false);
Task tskSElevators(tik, TASK_FOREVER, &tLeerSensoresElevadores, &ts, true);
Task tskSPulmones(tik, TASK_FOREVER, &tLeerSensoresPulmones, &ts, true);
Task tskSPedales(tik, TASK_FOREVER, &tLeerPedales, &ts, true);
Task tCycle(tik, TASK_FOREVER, &tCycleCallback, &ts, true);


//-- Variables para el MODBUS
byte mac[] = {0x60, 0x52, 0xD0, 0x06, 0x8F, 0x4C};  //* MAC de la tarjeta Ethernet
IPAddress ip(169, 254, 1, 238);                     //* IP estatica
IPAddress myDns(169, 254, 1, 1);                    //* DNS
IPAddress gateway(169, 254, 1, 1);                  //* Gateway
IPAddress subnet(255, 255, 0, 0);                   //* Mascara
EthernetServer server(502);                         //* Puerto de ModBus
EthernetServer data(25);                            //* Puerto de COM Virtual
ModbusTCPServer modbusTCPServer;

//* ###########
//* # DEFINES #
//* ###########

#define _rDPos(a,b) = P1.readDiscrete(a, b);

// Definicion de los SLOTS del P1AM
#define SLDELEVADOR1            1
#define SLDELEVADOR2            2
#define SLDELEVADOR3            3
#define SLDELEVADOR4            4
#define ENTELEVADOR1            5
#define ENTELEVADOR2            8
#define ENTELEVADOR3            6
#define ENTELEVADOR4            7
// Definicion de las direcciones del ModBus
#define ADDRCOIL                0
#define ADDRREGS                1000
// Tiempo de espera entre datos para el servodrive
#define INTERVALO1              6
#define INTERVALO2              10
// Cantidad de registros de COILS y HREG
#define NUMCOILS                140
#define NUMREGS                 3
// Inicio y final de COILS en el ModBus
#define INITSLOT1                0
#define ENDSLOT1                13
#define INITSLOT2               13
#define ENDSLOT2                26
#define INITSLOT3               26
#define ENDSLOT3                42
#define INITSLOT4               42
#define ENDSLOT4                52
#define INITSLOT5               52
#define ENDSLOT5                58
// Posiciones en los SLOTS del PLC de los elevadores
#define ELEVADOR1               0x00
#define ELEVADOR2               0x03
#define ELEVADOR3               0x06
#define ELEVADOR4               0x09
//-- Posiciones de elevadores para el servodrive
#define OFF                     0   //-- Pone en cero las salidad del slot
#define CTRG                    4   //-- Manda el TRIG del HOME
#define POS1                    1   //-- Manda la POS1 de los servos
#define POS1CTRG                5   //-- Manda el TRIG de la POS1
#define POS2                    2   //-- Manda la POS2 de los servos
#define POS2CTRG                6   //-- Manda el TRIG de la POS2
#define POS3                    3   //-- Manda la POS3 de los servos
#define POS3CTRG                7   //-- Manda el TRIG de la POS3
// Posiciones del mesanin para el servodrive
#define MCTRG                   16  //-- Manda TRIG de POS1 del Mesanin
#define MPOS1                   1   //-- Manda POS1 del Mesanin
#define MPOS1CTRG               17  //-- Manda TRIG de POS1 del Mesanin
#define MPOS2                   2   //-- Manda POS2 del Mesanin
#define MPOS2CTRG               18  //-- Manda TRIG de POS2 del Mesanin
#define MPOS3                   3   //-- Manda POS3 del Mesanin
#define MPOS3CTRG               19  //-- Manda TRIG de POS3 del Mesanin
#define MPOS4                   4   //-- Manda POS4 del Mesanin
#define MPOS4CTRG               20  //-- Manda TRIG de POS4 del Mesanin
#define MPOS5                   5   //-- Manda POS5 del Mesanin
#define MPOS5CTRG               21  //-- Manda TRIG de POS5 del Mesanin
#define MPOS6                   6   //-- Manda POS6 del Mesanin
#define MPOS6CTRG               22  //-- Manda TRIG de POS6 del Mesanin
#define MPOS7                   7   //-- Manda POS7 del Mesanin
#define MPOS7CTRG               23  //-- Manda TRIG de POS7 del Mesanin
#define MPOS8                   8   //-- Manda POS8 del Mesanin
#define MPOS8CTRG               24  //-- Manda TRIG de POS8 del Mesanin
#define MPOS9                   9   //-- Manda POS9 del Mesanin
#define MPOS9CTRG               25  //-- Manda TRIG de POS9 del Mesanin

//-- COILS
#define UPElevMesanin           42  //-- COIL de FSM PInit
#define SEFeeder                43  //-- COIL para indicar si existe caja en el FeederElevator
#define FeederElevator          44  //-- COIL para indicar si el FeederElevator esta arriba
#define SEBox2Sorter            45  //-- COIL para indicar si existe caja en el Sorter
#define fsmSorter               46  //-- COIL para indicar si el Sorter tiene Tarea
#define fsmSorterEmpujador      47  //-- COIL para indicar si el SorterEmpujador tiene Tarea
#define fsmEmpujador            48  //-- COIL para indicar si el Empujador tiene Tarea
#define waitFeederE             49  //-- COIL para inhabilitar el FeederElevator si no tiene Tarea
#define AutoOperationMode       51  //-- COIL para activar el modo Automatico
#define mactPST1                52  //-- COIL para activar el piston 1
#define mactPST2                53  //-- COIL para activar el piston 2
#define mactMOT                 54  //-- COIL para activar el motor del sorter
#define servosSON               55  //-- COIL para activar las estaciones
#define waitPause               56  //-- COIL para activar el modo PAUSA
#define enableStage1            76  //-- COIL para habilitar/deshabilitar la estacion 1
#define enableStage2            77  //-- COIL para habilitar/deshabilitar la estacion 2
#define enableStage3            78  //-- COIL para habilitar/deshabilitar la estacion 3
#define CoilDB                  79  //-- COIL para los pulsadores de subida del elevador 
#define ManualOperationMode     80  //-- COIL para activar el modo Manual
#define CoilE4R2BO              81  //-- COIL para el sensor reflex que detecta caja en la entrada del elevador.

#define CoilEMG                 90 //--COIL para activar el paro de emergencia



//-- Pins
#define CajaSorterMesanin       10
#define MPiston1IN              11  // Entrada Piston 1 (Elevador) - Sensor Posición IN
#define MPiston1OUT             12  // Entrada Piston 1 (Elevador) - Sensor Posición OUT
#define MPiston2IN              13  // Entrada Piston 2 (Pulmon) - Sensor Posición IN
#define MPiston2OUT             14  // Entrada Piston 2 (Pulmon) - Sensor Posición OUT
#define CMDOK_EMESANIN          10
#define CMDOK_SORTER            11
#define CMDOK_EMPUJADOR         12
//-- HOME Stages
#define homeStage1              11
#define homeStage2              12
#define homeStage3              13
#define homeMesanin             14
//-- HREGS - Holdin Registers
#define HREG_SorterPos          1000    //-- Indica la siguiente posición a la que debe ir el sorter (control manual)
#define HREG_EmpujadorPos       1001    //-- Indica la posicion actual del empujador (control manual)
#define HREG_Pulmon             1002    //-- Envia el primer elemento en la cola del pulmon
#define ESElevadores            1003
//-- Mascaras para SLOTS
//! SLOT 4
#define maskPST1                0x0003  //-- Mascara para activar el piston del elevador
#define maskPST2                0x000C  //-- Mascara para activar el piston del empujador
#define maskMot                 0x0010  //-- Mascara para activar el motor del sorter
#define maskSON                 0x01E0  //-- Mascara para activar los 4 modulos de los servodrives


//* ####################
//* # CUSTOM VARIABLES #
//* ####################

//-- Others
bool ledState = 0;
bool SorterEmpujadorFlag = false;
bool queue_fill = false;
bool EMGFlag = true;
bool Piston1_Out = false;
bool Piston2_Out = false;
bool BandON = false;
bool flagManual = false;
bool flagAutom = false;
bool flagPrueba = false;
bool withPedal[3] = {false , false , false };
int  EnableStages[3] = {1 , 1 , 1 };

unsigned int stateActuadores = 0x0005; //? 0x000A;
unsigned long tiempoAhora = 0;
bool MB_C[NUMCOILS];                //-- Modbus Coil Bits
unsigned long MS2Sorter = 15;       //-- Posicion almacenada del Sorter
unsigned long MS3Empujador = 15;    //-- Posicion almacenada del Empujador
unsigned long _lastMS2Sorter = 15;
unsigned long _lastMS3Empujador = 15;
unsigned long tiempo1 = 0;          //-- Tiempo ahora en millis
int servo1 = 0;                     //-- Bits que se le mandan al servo
int sorterCMDOK = 0;
int empujadorCMDOK = 0;
int EmpujadorLastPos = 5;  //--variable para guardar la ultima posición del empujador
int SorterLastPos = 4;  //--variable para guardar la ultima posición del sorter

// -- Contadores para los delay
int contadorCallBack = 0;
int contadorTimer = 0;
int contadorEmpujador = 0;
int contadorEmpujador2 = 0;
int finContadorSorterEmpujador = 0;
int contadorUnlock = 0;
// banderas
bool flag_sensoresElevadores[9] = {false, false, false, false, false, false, false, false, false}; //bandera para activar tarea de subir elevador
bool flag_queue = false; //bandera que indica que subió alguno de los elevadores, por lo que se debe agregar una nueva posición a la lista.
bool flag_queue_elevadores = false;
bool flag_queue_sorter = false; //bandera que indica que subió alguno de los elevadores, por lo que se debe agregar una nueva posición a la lista.
bool flag_elevador_sorter = false; //bandera para verificar que el elevador subió, se pone en falso cuando la tarea del sorter se completa
bool flag_wait_reset = false;
bool flag_reset_system = false;
// banderas de las tareas del elevador
bool task1_elevador = false;
bool task2_elevador = false;
bool task3_elevador = false;
// banderas de las tareas del sorter
bool task1_sorter = false;
bool task2_sorter = false;
bool task3_sorter = false;


//-- Utils
int CMD_OK [12] = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}; //Vector para llevar el control de movimiento de los servo motores
int queue_order[9] = { -1, -1, -1, -1, -1, -1, -1, -1, -1}; //vector para llevar el control de la cola de pedidos del empujador
int queue_sorter[9] = { -1, -1, -1, -1, -1, -1, -1, -1, -1}; //vector para llevar el control de la cola de pedidos del sorter
int queue_elevadores[9] = { -1, -1, -1, -1, -1, -1, -1, -1, -1}; //vector para llevar el control de los elevadores que no subieron, por esperar caja en el pulmon
int last_order = -1; // almacena temporalmente la posición del ultimo elevador que subió para ser agregado al queue
bool elevador_arriba[10] = {false, false, false, false, false, false, false, false, false, false};
bool Mode = false;
int _sendValuesESPulmones = 0;
int _posValues[9] = { 1, 2, 4, 8, 16, 32, 64, 128, 256};
int _elevadoresUP[11] = { 0, 57, 6, 10, 15, 19, 23, 28, 32, 36, 40 }; // coils para enviar los elevadores a la posición 2
int _elevadoresDOWN[11] = { 0, 1, 5, 9, 14, 18, 22, 27, 31, 35, 39 }; // coils para enviar los elevadores a la posición 1
int _coilsSPulmon[9]    = { 58, 59, 60, 61, 62, 63, 64, 65, 66 };   //Coils para los sensores de caja de la Zona del Pulmon
int _coilsSElevators[9] = { 67, 68, 69, 70, 71, 72, 73, 74, 75 };   // Coils Para los sensores de caja de los elevadores
int _coilsDOWNElev[9] = { 84, 85, 86, 87, 88, 89,  90, 91, 92 };
int _coilsUPElev[10] = { 93, 94, 95, 96, 97, 98, 99, 100, 101, 42 };
int _neumatiS[4] = {102, 103, 104, 105}; // Coils para los sensores de los actuadores neumaticos
//-- Elevadores
bool _waitSElevadores = false;
int _valuesSElevadores[9] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int _lastValuesSElevadores[9] = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
int _pedalOK[9] = { 0, 0, 0};
int _sendValueSElevadores = 0;
int _lastSendValueSElevadores = 0;
//-- Pulmones
int _valuesSPulmones[9] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int _lastValuesSPulmones[9] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int _sendValueSPulmones = 0;
int _lastSendValueSPulmones = 0;
//-- Confirmacion elevadores
int _valuesConfElevadores[10] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int _lastValuesConfElevadores[10] = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
int _valuesConfSElevadores[10] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int _sendValueConfElevadores = 0;
int _lastSendValueConfElevadores = 0;
//-- Feeder elevador
int _valueSFeeder = 0;
int _lastValueSFeeder = 0;
int _sendValueSFeeder = 0;
int _lastSendValueSFeeder = 0;
//-- CajaSorter
int _valueSCajaSorter = 0;
int _lastValueSCajaSorter = 0;
int _sendValueSCajaSorter = 0;
int _lastSendValueSCajaSorter = 0;
//-- Pedales
bool _waitPedales = false;
int _pinsEnablePedales[3] = { 3, 4, 5 };
int _pinsValuesPedales[3] = { 6, 7, 8 };
int _valuesEnablePedales[3] = { 0, 0, 0 };
int _lastValuesEnablePedales[3] = { 0, 0, 0 };
int _valuesPedales[3] = { 0, 0, 0 };
int _lastValuesPedales[3] = { 0, 0, 0 };
int _flagPedales[3] = { 0, 0, 0 };
int _lastFlagPedales[3] = { -1, -1, -1 }; //-- No esta activado

bool _availableMesanin = true;
bool _elevMesaninUsed = false;
// Handler
bool _checkSorterTask1 = false;
bool _checkSorterTask2 = false;
bool _checkSorterEmpujadorTask1 = false;
bool _checkSorterEmpujadorTask2 = false;
bool _checkEmpujadorTask1 = false;
bool _checkEmpujadorTask2 = false;
bool _checkEmpujadorTask3 = false;

bool _Empujador2Task = false;
bool _Sorter2Task = false;
bool _SorterEmpujador2Task = false;
bool _MotorActivado = false;
bool _Pst1Activado = false;
bool _Pst2Activado = false;



//* ####################
//* # CUSTOM FUNCTIONS #
//* ####################

bool _ES_Empujador = false;
bool _ES_Sorter = false;
bool _ES_SorterEmpujador = false;
bool _ES_Elevadores = false;
bool _ES_DisableFuncts = false;
bool _ES_FeederElevador = false;
bool _Error_Task_Emp = false;
bool _Error_Task_Sort = false;
bool _Error_Task_SortEmp = false;
bool _InEmergencyStop = false;

/*
  Posicion Pines Sensores != entre versiones
  Comentar las lineas de acuerdo a la version del sistema
*/
// Version 1
int PinE4T9BO = 9;
int SlotE4T9BO = ENTELEVADOR1;

int PinE3S3BO = 9;
int SlotE3S3BO = ENTELEVADOR2;

int PinE4S1BO = 10;
int SlotE4S1BO = ENTELEVADOR2;

int PinReflexSorter = 10;
int SlotReflexSorter = ENTELEVADOR1;

int PinReflexElev = 15;
int SlotReflexElev = ENTELEVADOR2;

//Version 2
//int PinE4T9BO = 9;
//int SlotE4T9BO = ENTELEVADOR4;
//int PinE3S3BO = 9;
//int SlotE3S3BO = ENTELEVADOR2;
//int PinE4S1BO = 11;
//int SlotE4S1BO = ENTELEVADOR4;
//int PinReflexSorter = 10;
//int SlotReflexSorter = ENTELEVADOR1;
//int PinReflexElev = 10;
//int SlotReflexElev = ENTELEVADOR2;



void tLeerSensoresElevadores()
{
  LeerSensoresElevadores(true);
  llenar_queue();
  refill_queue();
  SubirElevadorPendiente();
  Ajuste_Queue_Elevador();
}



int SerialCallback() {
  int incomingByte;
  int woSBoxFeeder = P1.readDiscrete(SlotReflexElev, PinReflexSorter); //Sensor reflex en el elevador
  int woSBoxSorter = P1.readDiscrete(SlotReflexSorter, PinReflexSorter); // sensor reflex en el sorter




  if (Serial.available() > 0) {
    // read the incoming byte:
    incomingByte = Serial.read();
  }

  //EN el puerto serial enviar 0, para obtener la informacion del callback
  if  (incomingByte == 48) {

    // Imprime en el puerto serial el estado de las tareas

    _PP("Empujador Task 1 = ");         _PL(_checkEmpujadorTask1);
    _PP("Empujador Task 2 =  ");        _PL(_checkEmpujadorTask2);
    _PP("Empujador Task 3 =  ");        _PL(_checkEmpujadorTask3);
    _PP("SensoresElevadores = ");       _PL(SensoresElevadores());
    _PP("SensoresPulmones = ");         _PL(SensoresPulmones());
    _PP("ReflexSorter = ");             _PL(woSBoxSorter);
    _PP("ReflexFeeder = ");             _PL(woSBoxFeeder);
    _PP("Siguiente Pulmon = ");         _PL(queue_sorter[0]);
    _PP("StateActuadores = ");         _PL(stateActuadores);
    _PP("Posicion = "); _PL(modbusTCPServer.holdingRegisterRead(HREG_EmpujadorPos));
    _PP("Auto = "); _PL(flagAutom);
    _PP("Manual = "); _PL(flagManual);
    _PP("Banda Estado = "); _PL(BandON);

    _PP("Estaciones Activas =  (");
    for (int jj = 0; jj < 3; jj++) {
      _PP(EnableStages[jj]);
      _PP(",");
    }
    _PL(")");

    _PP("Enable Pedales=  (");
    for (int jj = 0; jj < 3; jj++) {
      _PP(_valuesEnablePedales[jj]);
      _PP(",");
    }
    _PL(")");

    _PP("Values Pedales=  (");
    for (int jj = 0; jj < 3; jj++) {
      _PP(_valuesPedales[jj]);
      _PP(",");
    }
    _PL(")");

    _PP("Queue Empujador=  (");
    for (int jj = 0; jj < 9; jj++) {
      _PP(queue_order[jj])
      _PP(",");
    }
    _PL(")");


    _PP("Queue Sorter =  (");
    for (int jj = 0; jj < 9; jj++) {
      _PP(queue_sorter[jj])
      _PP(",");
    }
    _PL(")");

    _PP("Queue Elevadores =  (");
    for (int jj = 0; jj < 9; jj++) {
      _PP(queue_elevadores[jj])
      _PP(",");
    }
    _PL(")");

    _PP("Sensores Elevadores = ");
    for (int jj = 0; jj < 9; jj++) {
      _PP(_valuesSElevadores[jj]);
      _PP(",");
    }
    _PL(")");
    _PP("Sensores Pulmon = ");
    for (int jj = 0; jj < 9; jj++) {
      _PP(_valuesSPulmones[jj]);
      _PP(",");
    }
    _PL(")");

    _PP("Elevadores arriba  = ");
    for (int jj = 0; jj < 9; jj++) {
      _PP(elevador_arriba[jj]);
      _PP(",");
    }
    _PL(")");

  }

  else if (incomingByte == 49) {
    TriggerPstEmpujador();
  }
  else if (incomingByte == 50) {
    triggerPstElevador();
  }

}

int SensoresElevadores() {
  //-- Lectura sensores
  int cuenta = 0;
  for (int jj = 0; jj < 9; jj++)
  {
    if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
      if (jj == 8)
      {
        _valuesSElevadores[jj] = P1.readDiscrete(SlotE3S3BO, PinE3S3BO);
      }
      else
      {
        _valuesSElevadores[jj] = P1.readDiscrete(ENTELEVADOR2, jj + 1);
      }
    }
    else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
      _valuesSElevadores[jj] = 1;
    }
    cuenta += _valuesSElevadores[jj];

  }


  return cuenta;

}


int SensoresPulmones() {
  //-- Lectura sensores
  int cuenta2 = 0;
  for (int jj = 0; jj < 9; jj++)
  {
    if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
      if (jj == 8)
      {
        _valuesSPulmones[jj] = P1.readDiscrete(SlotE4T9BO, PinE4T9BO);
      }
      else
      {
        _valuesSPulmones[jj] = P1.readDiscrete(ENTELEVADOR1, jj + 1);
      }
    }
    else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
      _valuesSPulmones[jj] = 1;
    }
    cuenta2 += _valuesSPulmones[jj];
  }
  return cuenta2;



}

void Ajuste_Queue_Elevador() {
  // esta funcion lee los sensores de los elevadores, si una de las posiciones de los sensores se encuentra en la lista
  // pero el sensor indica que tiene caja, saca la posición de la lista

  for (int jj = 0; jj < 9; jj++) {
    if (queue_elevadores[jj] != -1 && _valuesSElevadores[queue_elevadores[jj]] == 1) {
      if (jj == 8) {
        queue_elevadores[jj] = -1;
      }
      else {
        queue_elevadores[jj] = queue_elevadores[jj + 1];
      }
    }
  }



}



////////////////// Tareas del Sistema ///////////////////////////////////
////////////////////////////////////////////////////////////////////////
void tCycleCallback()
{
  /*
      Activa las tareas que estan implicadas con la peticion a resolver.
  */

  SerialCallback();
  leerSensores();

  if (P1.readDiscrete(ENTELEVADOR4, 2) == 1 && flag_wait_reset == false) {
    EMGFlag = true;  //no hay paro de emergencia
  }
  else if (P1.readDiscrete(ENTELEVADOR4, 2) == 0 && EMGFlag == true) {
    EMGFlag = false;
    flag_wait_reset = true;
    _PL("Se Activa el paro de emergencia");
    UpdatePositionN();
    stateActuadores = 0x01E5; // servos en ON, y los actuadores N y Sorter en OFF.
    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    modbusTCPServer.coilWrite(CoilEMG, HIGH);

  }

  if (modbusTCPServer.coilRead(CoilEMG) == false && EMGFlag == false) {
    _PL("Reset Activo");
    resetMode();
    resume_operation();

  }
  else if (modbusTCPServer.coilRead(CoilEMG) == true && EMGFlag == false) {
    _PL("Esperando Reset");
  }



  if (EMGFlag)
  {

    modbusTCPServer.coilWrite(CoilEMG, LOW);
    bool Manual = modbusTCPServer.coilRead(ManualOperationMode);
    bool autom;
    if (flagPrueba) {
      autom   = flagPrueba;
    }
    else {
      autom = modbusTCPServer.coilRead(AutoOperationMode);
    }


    if (Manual && digitalRead(SWITCH_BUILTIN))
    {
      if (!flagManual) {
        _PL("Se Activó el modo Manual");
        //modbusTCPServer.coilWrite(ManualOperationMode, LOW);
        flagManual = true;
        flagAutom = false;
      }
      tskSPedales.disable();    // Actualiza si estan conectados los pedales
      tskSElevators.disable(); // Lee los sensores de los Elevadores
      tskSPulmones.disable();   // Lee los Sensores de los Pulmones
      tskSubirElevadorSorter.disable(); // desabilita el elevador del sorter en automatico
      manualModePLC();
    }
    else if (!Manual) {
      flagManual = false;
    }

    if (autom) {
      if (!flagAutom) {
        _PL("Se activó el modo automatico");
        //modbusTCPServer.coilWrite(AutoOperationMode, LOW);
        flagAutom = true;
        flagManual = false;
        tskSPedales.enableDelayed();    // Actualiza si estan conectados los pedales
        tskSElevators.enableDelayed();  // Lee los sensores de los Elevadores
        tskSPulmones.enableDelayed();   // Lee los Sensores de los Pulmones
        tskSubirElevadorSorter.enable(); // habilita el elevador del sorter en automatico
      }
    }
    else {
      flagAutom = false;
    }
    //////////////////////////////////////////////////////////////////////////////

    if (queue_order[0] != -1 ) {
      tskMoverEmpujador.enable();
    }
    else {
      tskMoverEmpujador.disable();
    }
    if (queue_sorter[0] != -1) {
      //_PL("Tarea Sorter Habilitada");
      tskSubirElevadorSorter.enable();
      modbusTCPServer.holdingRegisterWrite(HREG_Pulmon, queue_sorter[0] + 1);
    }
    else {
      tskSubirElevadorSorter.disable();
      modbusTCPServer.holdingRegisterWrite(HREG_Pulmon, queue_sorter[0] + 1);
    }
  }
}



//EN el puerto serial enviar 0, para obtener la informacion del callback







void tLeerSensorFeeder()
{
  /*
      Actualiza si existe una caja en la plataforma del elevador
      del mesanin, solamente si se encuentra en el modo automatico.
  */
  LeerSensorFeeder(true);
}

void tLeerSensorCajaSorter()
{
  /*
      Actualiza si existe una caja en la plataforma del Sorter del
      mesanin, solamente si se encuentra en el modo automatico.
  */
  LeerSensorCajaSorter(true);
}

void tLeerPedales()
{
  /*
      Actualiza si existe un pedal conectado al sistema.
  */
  LeerPedales();
}

void LeerPedales() {
  if (EMGFlag)
  {
    _waitPedales = flagAutom;
    if (_waitPedales)
    {
      //_PL("Lectura de pedales");
      //-- Lectura enable y valores Pedales
      for (int ii = 0; ii < 3; ii++)
      {
        _valuesEnablePedales[ii] = P1.readDiscrete(ENTELEVADOR4, _pinsEnablePedales[ii]);
        _valuesPedales[ii] = P1.readDiscrete(ENTELEVADOR4, _pinsValuesPedales[ii]);
      }



    }
  }
}



/*

    Actualiza y manda el elevador indicado a la POS 2 si este
    no tiene caja ademas de que la estacion se encuentre
    habilitada.

*/
void tLeerSensoresPulmones()
{
  leerSensoresPulmones(true);

}



//***********************************************************************//
//************************* FUNCIONES DEL SISTEMA ***********************//
//***********************************************************************//


void tParoEmergencia()
{

}
bool disableEmergencyStop()
{

}

//*******************************************************************************************
//*******************************************************************************************
//*******************************************************************************************
//*******************************************************************************************
//*******************************************************************************************
// Funciones



void updateSorterCMDOK()
{
  /*
      Confirma que el sorter llegó a la posición enviada
  */
  sorterCMDOK = P1.readDiscrete(ENTELEVADOR3, CMDOK_SORTER);
}

//-- Actualiza el CMD_OK del empujador en el mesanin
void updateEmpujadorCMDOK()
{
  /*
      Confirma que el empujador este en la posicion enviada.
  */
  empujadorCMDOK = P1.readDiscrete(ENTELEVADOR3, CMDOK_EMPUJADOR);
}

void LeerSensorWoPedal(int initfor, int endfor)
{
  /*
      Realiza la lectura de los sensores de los elevadores para detectar si tienen
      caja o no y subirlos sin confirmacion del pedal.
  */
  for (int ii = initfor; ii < endfor; ii++)
  {
    if (_valuesSElevadores[ii] != _lastValuesSElevadores[ii])
    {
      int flag = _valuesSElevadores[ii] - _lastValuesSElevadores[ii];
      _lastValuesSElevadores[ii] = _valuesSElevadores[ii];
      if (flag < 0 ) {
        _sendValueSElevadores += _posValues[ii];
        if (_valuesSPulmones[ii] == 1) {
          setPositionUPElevador(ii + 1);
          elevador_arriba[ii] = true;
          modbusTCPServer.coilWrite(_coilsSElevators[ii], HIGH);
          flag_sensoresElevadores[ii] = HIGH;
          flag_queue = HIGH;
          last_order = ii;
          _PP("Subió Elevador sin pedal: "); _PL(ii + 1);
        }
        else {
          _PP("Elevador "); _PP(ii + 1); _PL(" Agregado a la cola");
          flag_queue_elevadores = true;

          llenar_queue_elevadores(ii); //sino hay caja en el pulmon, agrega el pedido a la cola.

        }
      }
      else if (flag > 0)
      {
        _sendValueSElevadores = (_sendValueSElevadores >= _posValues[ii])
                                ? (_sendValueSElevadores - _posValues[ii])
                                : (_sendValueSElevadores);
        modbusTCPServer.coilWrite(_coilsSElevators[ii], LOW);
      }

    }
  }
}

void LeerSensorWithPedal(int initfor, int endfor, int station)
{
  /*
      Realiza la lectura de los sensores de los elevadores para detectar si tienen
      caja o no y subirlos con confirmacion del pedal.
  */

  for (int ii = initfor; ii < endfor; ii++)
  {
    // _PP("Pedal Conectado en estación "); _PP(station + 1); _PL(" - Esperando señal del pedal");
    if (_valuesSElevadores[ii] == 0 && _valuesSPulmones[ii] == 1 && _valuesPedales[station] == 1) {
      _pedalOK[station] = 1;
      setPositionUPElevador(ii + 1);
      elevador_arriba[ii] = true;
      modbusTCPServer.coilWrite(_coilsSElevators[ii], HIGH);
      flag_sensoresElevadores[ii] = HIGH;
      flag_queue = HIGH;
      last_order = ii;
      _PP("Subió Elevador con pedal: "); _PL(ii + 1);
    }
    else if (_valuesSElevadores[ii] == 0 && _valuesSPulmones[ii] == 0 && _valuesPedales[station] == 1) {
      _PP("Elevador "); _PP(ii + 1); _PL(" Agregado a la cola");
      flag_queue_elevadores = true;
      llenar_queue_elevadores(ii); //sino hay caja en el pulmon, agrega el pedido a la cola.
    }



  }
}
void SubirElevadorPendiente() {
  //Esta funcion revisa la cola de elevadores penditentes por subir,
  // los elevadores pendientes son los que no tienen caja en el pulmon.
  int pos = queue_elevadores[0];
  int station;


  if (queue_elevadores[0] != -1) {
    _PP("pedalOK"); _PL(_pedalOK[pos]);

    if (pos < 3) {
      station = 0;
    }
    else if (pos > 5) {
      station = 2;
    }
    else {
      station = 1;
    }

    if (_valuesEnablePedales[station] == 0) {
      if (_valuesSPulmones[pos] == 1 ) {

        setPositionUPElevador(pos + 1);
        delay(20);
        if ( P1.readDiscrete(ENTELEVADOR3, pos + 1)) {
          elevador_arriba[pos] = true;
          modbusTCPServer.coilWrite(_coilsSElevators[pos], HIGH);
          flag_sensoresElevadores[pos] = HIGH;
          flag_queue = HIGH;
          last_order = pos;
          _PP("Subió Elevador Pendiente: "); _PL(pos + 1);
          restar_queue_elevadores();
          _pedalOK[station] = 0;

        }
      }
    }

    else if (_valuesEnablePedales[station] == 1) {
      if (_valuesSPulmones[pos] == 1 && _pedalOK[station] == 1) {

        setPositionUPElevador(pos + 1);
        delay(20);
        if ( P1.readDiscrete(ENTELEVADOR3, pos + 1)) {
          elevador_arriba[pos] = true;
          modbusTCPServer.coilWrite(_coilsSElevators[pos], HIGH);
          flag_sensoresElevadores[pos] = HIGH;
          flag_queue = HIGH;
          last_order = pos;
          _PP("Subió Elevador Pendiente: "); _PL(pos + 1);
          restar_queue_elevadores();
          _pedalOK[station] = 0;



        }

      }
    }
  }
}

void SubirElevadorPostEMG() {
  //Cuando ocurre un paro de emergencia y el elevador está subiendo, el sorter queda con la tarea asignada para dicha posición,
  //sin embargo el elevador regresa a la posición abajo y no sube, por que necesita el flanco.
  //Está funcion sube los elevadores que no tienen caja, despues de liverarse el paro de emergencia. y reasigna la cola al empujador

  LeerSensoresElevadores(false);
  leerSensoresPulmones(false);
  int station;

  for (int jj = 0; jj < 9; jj++) {
    if (jj < 3) {
      station = 0;
    }
    else if (jj > 5) {
      station = 2;
    }
    else {
      station = 1;
    }

    if (_valuesEnablePedales[station] == 0) {  // si el pedal en la estacion está desconectado

      if (_valuesSElevadores[jj] == 0 && _valuesSPulmones[jj] == 1) {
        setPositionUPElevador(jj + 1);
        elevador_arriba[jj] = true;
      }
      else if (_valuesSElevadores[jj] == 0 && _valuesSPulmones[jj] == 0) {
        flag_queue_elevadores = true;
        llenar_queue_elevadores(jj);
      }
      else if (_valuesEnablePedales[station] == 0) { // si el pedal en la estacion está conectado

        if (_valuesSElevadores[jj] == 0 && _valuesSPulmones[jj] == 1 && _valuesPedales[station] == 1) {
          setPositionUPElevador(jj + 1);
          elevador_arriba[jj] = true;
        }
        else if (_valuesSElevadores[jj] == 0 && _valuesSPulmones[jj] == 0 && _valuesPedales[station] == 1) {
          flag_queue_elevadores = true;
          llenar_queue_elevadores(jj);
        }
      }

    }
  }
}


//* ####################
//* # MODBUS FUNCTIONS #
//* ####################

/************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************
  ///************************************ Funciones Necesarias para Operar el Modo Manual ********************************
************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************
************************************************************************************************************************/



//-- En el modo de operacion manual se puede mover de forma manual todos los actuadores
void manualModePLC()
{
  /*
      Modo manual en el que se activan los movimientos de los elevadores y los servos del mesanin mandados por el operador.
  */
  updatePositions();
  updatePositions1();
  UpdatePositionN();
  leerSensores();



}

void updatePositions1()
{
  /*
      Actualiza la posicion del empujador y del sorter de acuerdo a la petición enviada por el
      Modbus
  */
  if (modbusTCPServer.holdingRegisterRead(HREG_SorterPos) != _lastMS2Sorter) //-- MS2Sorter
  {
    MS2Sorter = modbusTCPServer.holdingRegisterRead(HREG_SorterPos);
    if (P1.readDiscrete(ENTELEVADOR2, homeMesanin) == 1)
    {
      setPositionSorter(MS2Sorter);
    }
    _lastMS2Sorter = MS2Sorter;
  }
  if (modbusTCPServer.holdingRegisterRead(HREG_EmpujadorPos) != _lastMS3Empujador) //-- MS3Empujador
  {
    MS3Empujador = modbusTCPServer.holdingRegisterRead(HREG_EmpujadorPos);
    if (P1.readDiscrete(ENTELEVADOR2, homeMesanin) == 1)
    {
      setPositionEmpujador(MS3Empujador);
    }
    _lastMS3Empujador = MS3Empujador;
  }
}

uint8_t valor = 0;
void UpdatePositionN() {
  /*
     Actualiza la posicion de los actuadores neumaticos de acuerdo a la peticion enviada por Modbus
  */
  if (EMGFlag) {

    if (MB_C[mactPST1])
    {
      MB_C[mactPST1] = modbusTCPServer.coilWrite(mactPST1, LOW);
      triggerPstElevador();
    }
    if (MB_C[mactPST2])
    {
      MB_C[mactPST2] = modbusTCPServer.coilWrite(mactPST2, LOW);
      TriggerPstEmpujador();
    }
    if (MB_C[mactMOT])
    {
      MB_C[mactMOT] = modbusTCPServer.coilWrite(mactMOT, LOW);
      TriggerBandaSorter();
    }
    if (MB_C[servosSON])
    {
      MB_C[servosSON] = modbusTCPServer.coilWrite(servosSON, LOW);
      stateActuadores = stateActuadores ^ maskSON;
      _PL(stateActuadores);
      P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    }
  }
  else  if (!EMGFlag) {
    if (P1.readDiscrete(ENTELEVADOR1, MPiston1IN) == 0 && Piston1_Out == true) {
      _PL("Piston 1 Vuelve - EMG");
      triggerPstElevador();

    }
    if (P1.readDiscrete(ENTELEVADOR1, MPiston2IN) == 0 && Piston2_Out == true) {
      _PL("Piston 2 Vuelve - EMG");
      TriggerPstEmpujador();

    }
    if (BandON) {
      TriggerBandaSorter();
    }
  }

}

//-- Actualizar registros mesanin

//! # Funciones de actualizar registros modbus #

//-- Actualiza un grupo de registros
void updateCoilsSlot1()
{
  /*
      Actualiza los registros que hacen referencia al SLOT 1.
  */
  for (int i = INITSLOT1; i < ENDSLOT1; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

//-- Actualiza un grupo de registros
void updateCoilsSlot2()
{
  /*
      Actualiza los registros que hacen referencia al SLOT 2.
  */
  for (int i = INITSLOT2; i < ENDSLOT2; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

//-- Actualiza un grupo de registros
void updateCoilsSlot3()
{
  /*
      Actualiza los registros que hacen referencia al SLOT 3.
  */

  for (int i = INITSLOT3; i < ENDSLOT3; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

//-- Actualiza un grupo de registros
void updateCoilsSlot5()
{
  /*

      Actualiza los registros que hacen referencia al SLOT 5.

  */

  for (int i = INITSLOT5; i < ENDSLOT5; i++)
  {
    MB_C[i] = modbusTCPServer.coilRead(i);
  }
}

void setPositionDOWNElevador(int posElevator)
{
  /*
      Envia el elevador indicado a la posicion 1.
  */
  switch (posElevator)
  {
    case 1:  setPositionServoW(_elevadoresDOWN[1], POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR1, _elevadoresDOWN[1]); break;
    case 2:  setPositionServoW(_elevadoresDOWN[2], POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR1, _elevadoresDOWN[2]); break;
    case 3:  setPositionServoW(_elevadoresDOWN[3], POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR1, _elevadoresDOWN[3]); break;
    case 4:  setPositionServoW(_elevadoresDOWN[4], POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR3, _elevadoresDOWN[4]); break;
    case 5:  setPositionServoW(_elevadoresDOWN[5], POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR3, _elevadoresDOWN[5]); break;
    case 6:  setPositionServoW(_elevadoresDOWN[6], POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR3, _elevadoresDOWN[6]); break;
    case 7:  setPositionServoW(_elevadoresDOWN[7], POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR2, _elevadoresDOWN[7]); break;
    case 8:  setPositionServoW(_elevadoresDOWN[8], POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR2, _elevadoresDOWN[8]); break;
    case 9:  setPositionServoW(_elevadoresDOWN[9], POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR2, _elevadoresDOWN[9]); break;
    case 10: setPositionServoW(_elevadoresDOWN[10], POS1, POS1CTRG, ELEVADOR4, SLDELEVADOR3, _elevadoresDOWN[10]); break;
    default: break;
  }
}

void setPositionUPElevador(int posElevator)
{
  /*
      Envia el elevador indicado a la posicion 2.
  */
  switch (posElevator)
  {
    case 1:  setPositionServoW(_elevadoresUP[1], POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR1, _elevadoresUP[1]); break;
    case 2:  setPositionServoW(_elevadoresUP[2], POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR1, _elevadoresUP[2]); break;
    case 3:  setPositionServoW(_elevadoresUP[3], POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR1, _elevadoresUP[3]); break;
    case 4:  setPositionServoW(_elevadoresUP[4], POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR3, _elevadoresUP[4]); break;
    case 5:  setPositionServoW(_elevadoresUP[5], POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR3, _elevadoresUP[5]); break;
    case 6:  setPositionServoW(_elevadoresUP[6], POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR3, _elevadoresUP[6]); break;
    case 7:  setPositionServoW(_elevadoresUP[7], POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR2, _elevadoresUP[7]); break;
    case 8:  setPositionServoW(_elevadoresUP[8], POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR2, _elevadoresUP[8]); break;
    case 9:  setPositionServoW(_elevadoresUP[9], POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR2, _elevadoresUP[9]); break;
    case 10: setPositionServoW(_elevadoresUP[10], POS2, POS2CTRG, ELEVADOR4, SLDELEVADOR3, _elevadoresUP[10]); break;
    default: break;
  }
}
//! # p.}  SLOT 1 #

//-- Ingresa a cual posicion se quiere enviar el elevador E11
void setPositionE11(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 1 en la estacion 1.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (  0,  CTRG,           ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  0); break;
    case 1: setPositionServo(  1,  POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  1); break;
    case 2: setPositionServo( 57,  POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL + 57); break;
    case 3: setPositionServo(  3,  POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR1, ADDRCOIL +  3); break;
    default: break;
  }
}
//-- Ingresa a cual posicion se quiere enviar el elevador E12
void setPositionE12(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 2 en la estacion 1.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    ( 4,  CTRG,           ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  4); break;
    case 1: setPositionServo( 5,  POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  5); break;
    case 2: setPositionServo( 6,  POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  6); break;
    case 3: setPositionServo( 7,  POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR1, ADDRCOIL +  7); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador E13
void setPositionE13(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 3 en la estacion 1.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (  8,  CTRG,           ELEVADOR3, SLDELEVADOR1, ADDRCOIL +   8); break;
    case 1: setPositionServo(  9,  POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +   9); break;
    case 2: setPositionServo( 10,  POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +  10); break;
    case 3: setPositionServo( 11,  POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR1, ADDRCOIL +  11); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el sorter del mesanin
void setPositionSorter(int posSorter)
{
  /*
      Envia el sorter a la posicion indicada por posSorter
  */
  switch (posSorter)
  {
    case 1: setPositionMesanin(MPOS1,  MPOS1CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 2: setPositionMesanin(MPOS2,  MPOS2CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 3: setPositionMesanin(MPOS3,  MPOS3CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 4: setPositionMesanin(MPOS4,  MPOS4CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 5: setPositionMesanin(MPOS5,  MPOS5CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 6: setPositionMesanin(MPOS6,  MPOS6CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 7: setPositionMesanin(MPOS7,  MPOS7CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 8: setPositionMesanin(MPOS8,  MPOS8CTRG, ELEVADOR4, SLDELEVADOR1); break;
    case 9: setPositionMesanin(MPOS9,  MPOS9CTRG, ELEVADOR4, SLDELEVADOR1); break;
    default: break;
  }
}

//! # Actuadores SLOT 2 #

//-- Ingresa a cual posicion se quiere enviar el elevador E21
void setPositionE21(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 1 en la estacion 2.

  */
  switch (posElevator)
  {
    case 0: setHomeServo    (13,  CTRG,           ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 13); break;
    case 1: setPositionServo(14,  POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 14); break;
    case 2: setPositionServo(15,  POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 15); break;
    case 3: setPositionServo(16,  POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR3, ADDRCOIL + 16); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador E22
void setPositionE22(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 2 en la estacion 2.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (17,  CTRG,           ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 17); break;
    case 1: setPositionServo(18,  POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 18); break;
    case 2: setPositionServo(19,  POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 19); break;
    case 3: setPositionServo(20,  POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR3, ADDRCOIL + 20); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador E23
void setPositionE23(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 3 en la estacion 2.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (21,  CTRG,           ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 21); break;
    case 1: setPositionServo(22,  POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 22); break;
    case 2: setPositionServo(23,  POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 23); break;
    case 3: setPositionServo(24,  POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR3, ADDRCOIL + 24); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el empujador del mesanin
void setPositionEmpujador(int posPiston)
{
  /*
      Envia el empujador a la posicion indicada.
  */
  switch (posPiston)
  {
    case 1: setPositionMesanin(MPOS1,  MPOS1CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 2: setPositionMesanin(MPOS2,  MPOS2CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 3: setPositionMesanin(MPOS3,  MPOS3CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 4: setPositionMesanin(MPOS4,  MPOS4CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 5: setPositionMesanin(MPOS5,  MPOS5CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 6: setPositionMesanin(MPOS6,  MPOS6CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 7: setPositionMesanin(MPOS7,  MPOS7CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 8: setPositionMesanin(MPOS8,  MPOS8CTRG, ELEVADOR4, SLDELEVADOR2); break;
    case 9: setPositionMesanin(MPOS9,  MPOS9CTRG, ELEVADOR4, SLDELEVADOR2); break;
    default: break;
  }
}

//! # Actuadores SLOT 3 #

//-- Ingresa a cual posicion se quiere enviar el elevador E31
void setPositionE31(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 1 en la estacion 3.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (26,  CTRG,           ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 26); break;
    case 1: setPositionServo(27,  POS1, POS1CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 27); break;
    case 2: setPositionServo(28,  POS2, POS2CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 28); break;
    case 3: setPositionServo(29,  POS3, POS3CTRG, ELEVADOR1, SLDELEVADOR2, ADDRCOIL + 29); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador E32
void setPositionE32(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 2 en la estacion 3.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (30,  CTRG,           ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 30); break;
    case 1: setPositionServo(31,  POS1, POS1CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 31); break;
    case 2: setPositionServo(32,  POS2, POS2CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 32); break;
    case 3: setPositionServo(33,  POS3, POS3CTRG, ELEVADOR2, SLDELEVADOR2, ADDRCOIL + 33); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador E33
void setPositionE33(int posElevator)
{
  /*
      Envia a la posicion indicada el elevador 3 en la estacion 3.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (34,  CTRG,           ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 34); break;
    case 1: setPositionServo(35,  POS1, POS1CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 35); break;
    case 2: setPositionServo(36,  POS2, POS2CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 36); break;
    case 3: setPositionServo(37,  POS3, POS3CTRG, ELEVADOR3, SLDELEVADOR2, ADDRCOIL + 37); break;
    default: break;
  }
}

//-- Ingresa a cual posicion se quiere enviar el elevador del mesanin
void setPositionEM1(int posElevator)
{
  /*
      Envia el elevador de alimentacion a la posicion indicada.
  */
  switch (posElevator)
  {
    case 0: setHomeServo    (38,  CTRG,           ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 38); break;
    case 1: setPositionServo(39,  POS1, POS1CTRG, ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 39); break;
    case 2: setPositionServo(40,  POS2, POS2CTRG, ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 40); break;
    case 3: setPositionServo(41,  POS3, POS3CTRG, ELEVADOR4, SLDELEVADOR3, ADDRCOIL + 41); break;
    default: break;
  }
}

//! # Home de mesanin #

//-- Ingresa cual de los servos del mesanin se manda a home
void setHomeMesanin(int posPiston)
{
  /*
      Envia hacer home al servo indicado en el mesanin.
  */
  switch (posPiston)
  {
    case 0: setHomeServo(12,  MCTRG, ELEVADOR4, SLDELEVADOR1, ADDRCOIL + 12); break; //-- Sorter
    case 1: setHomeServo(25,  MCTRG, ELEVADOR4, SLDELEVADOR2, ADDRCOIL + 25); break; //-- Empujador
    default: break;
  }
}

//! # Funciones de posiciones servos #

//-- Manda el servo del mesanin a una posicion
void setPositionMesanin(int pos, int poshome, int elevatorN, int slot)
{
  /*
      Funcion para enviar las curvas de posicion al servo.
  */
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
  tiempo1 = millis();
}

//-- Manda el servo de los elevadores a una posicion
void setPositionServoW(int indexT, int pos, int poshome, int elevatorN, int slot, int regCoil)
{
  /*
      Funcion para enviar las curvas de posicion al servo.
  */
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
  tiempo1 = millis();
}

//-- Manda el servo de los elevadores a una posicion
void setPositionServo(int indexT, int pos, int poshome, int elevatorN, int slot, int regCoil)
{
  /*
      Funcion para enviar las curvas de posicion al servo.
  */
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
    tiempo1 = millis();
    MB_C[indexT] = modbusTCPServer.coilWrite(regCoil, LOW);
  }
}

//-- Manda el servo a home
void setHomeServo(int indexT, int poshome, int elevatorN, int slot, int regCoil)
{
  /*
      Funcion para enviar las curvas de posicion al servo.
  */
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
    tiempo1 = millis();
    MB_C[indexT] = modbusTCPServer.coilWrite(regCoil, LOW);
  }
}

//-- Mueve el valor que se le ingresa a una posicion de bit deseada
int getValue(int value, int posElevator)
{
  return value << posElevator;
}

void updatePositions()
{
  /*
      Verifica que los actuadores se encuentren en la posicion inicial al
      entrar o finalizar la conexion con el PLC.
      Se revisan los estados de todos los COILS asociados a los actuadores en el sistema.
      En los casos positivos se ejecuta la posición indicada para el actuador.
  */
  //****************************** SLOT 1*********************************
  updateCoilsSlot1();
  updateCoilsSlot5();
  //? Estacion 1
  //* HOME
  setPositionE11(0); //! Elevador 1
  setPositionE12(0); //! Elevador 2
  setPositionE13(0); //! Elevador 3
  //* Posiciones
  // envia la posición indicada, solo si la estación completa ya hizo HOME
  if (P1.readDiscrete(ENTELEVADOR2, homeStage1) == 1)
  {
    //! Elevador 1
    setPositionE11(1);
    setPositionE11(2);
    setPositionE11(3);
    //! Elevador 2
    setPositionE12(1);
    setPositionE12(2);
    setPositionE12(3);
    //! Elevador 3
    setPositionE13(1);
    setPositionE13(2);
    setPositionE13(3);
  }
  //? Sorter
  setHomeMesanin(0);
  //****************************** SLOT 2*********************************
  updateCoilsSlot2();
  //? Estacion 2
  //* HOME
  setPositionE21(0); //! Elevador 1
  setPositionE22(0); //! Elevador 2
  setPositionE23(0); //! Elevador 3
  //* Posiciones
  // envia la posición indicada, solo si la estación completa ya hizo HOME
  if (P1.readDiscrete(ENTELEVADOR2, homeStage2) == 1)
  {
    //! Elevador 1
    setPositionE21(1);
    setPositionE21(2);
    setPositionE21(3);
    //! Elevador 2
    setPositionE22(1);
    setPositionE22(2);
    setPositionE22(3);
    //! Elevador 3
    setPositionE23(1);
    setPositionE23(2);
    setPositionE23(3);
  }
  //? Empujador
  setHomeMesanin(1);


  //****************************** SLOT 3*********************************
  updateCoilsSlot3();
  //? Estacion 3
  //* HOME
  setPositionE31(0); //! Elevador 1
  setPositionE32(0); //! Elevador 2
  setPositionE33(0); //! Elevador 3
  //* Posiciones
  // envia la posición indicada, solo si la estación completa ya hizo HOME
  if (P1.readDiscrete(ENTELEVADOR2, homeStage3) == 1)
  {
    //! Elevador 1
    setPositionE31(1);
    setPositionE31(2);
    setPositionE31(3);
    //! Elevador 2
    setPositionE32(1);
    setPositionE32(2);
    setPositionE32(3);
    //! Elevador 3
    setPositionE33(1);
    setPositionE33(2);
    setPositionE33(3);
  }
  //! Elevador Mesanin
  //* HOME
  setPositionEM1(0);
  //* Posiciones
  // envia el elevador del Sistema de alimentación a la posición indicada, solo si la estación completa ya hizo HOME
  if (P1.readDiscrete(ENTELEVADOR2, homeMesanin) == 1)
  {
    setPositionEM1(1);
    setPositionEM1(2);
    setPositionEM1(3);
  }
}

void initActuators()
{
  /*
      Verifica que los actuadores se encuentren en la posicion inicial al
      entrar o finalizar la conexion con el PLC.
  */
  //-- Piston1 FeederElevator
  int verificacionPst1 = stateActuadores & maskPST1;
  if (verificacionPst1 != 1)
  {
    stateActuadores = stateActuadores ^ maskPST1;
    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    _Pst1Activado = false;
  }
  //-- Piston2 Empujador
  int verificacionPst2 = stateActuadores & maskPST2;
  if (verificacionPst2 != 4)
  {
    stateActuadores = stateActuadores ^ maskPST2;
    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    _Pst2Activado = false;
  }
  //-- Motor Sorter
  int verificacionMotor = stateActuadores & 0x10;
  if (verificacionMotor != 0)
  {
    stateActuadores = stateActuadores ^ maskMot;
    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    _MotorActivado = false;
  }
}

void initSortEmpPos()
{
  manualModePLC();
  delay(10);

  for (int jj = 0; jj < 11; jj++) {
    setPositionDOWNElevador(jj);
    elevador_arriba[jj] = false;
    delay(10);
  }
  setPositionEmpujador(EmpujadorLastPos + 1);
  delay(10);
  setPositionSorter(SorterLastPos + 1);
}



void leerSensoresPulmones(bool stat) {
  /*
          Actualiza los sensores de los pulmones teniendo encuenta
          que la estacion se encuentre habilitada.
  */
  // Serial.println(P1.readDiscrete(ENTELEVADOR4, 2));
  if (EMGFlag)
  {
    //-- Lectura sensores
    for (int jj = 0; jj < 9; jj++)
    {
      if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
        if (jj == 8)
        {
          _valuesSPulmones[jj] = P1.readDiscrete(SlotE4T9BO, PinE4T9BO);
        }
        else
        {
          _valuesSPulmones[jj] = P1.readDiscrete(ENTELEVADOR1, jj + 1);
        }
      }
      else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
        _valuesSPulmones[jj] = 1;
      }
    }





    if (stat) {
      if (queue_fill == false) {
        //-- Stage 1

        for (int ii = 0; ii < 9; ii++)
        {
          if (_valuesSPulmones[ii] == 0) {

            llenar_queue_sorter(ii);
          }
        }
        queue_fill = true;
      }
    }

    else {
      for (int ii = 0; ii < 9; ii++)
      {

        if (_valuesSPulmones[ii] == 1)
        {
          modbusTCPServer.coilWrite(_coilsSPulmon[ii], LOW);
        }
        else
        {
          modbusTCPServer.coilWrite(_coilsSPulmon[ii], HIGH);
        }

      }

    }
  }



}


void LeerSensoresElevadores(bool stat)
{
  //-- Lectura sensores
  for (int jj = 0; jj < 9; jj++)
  {
    if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
      if (jj == 8)
      {
        _valuesSElevadores[jj] = P1.readDiscrete(SlotE3S3BO, PinE3S3BO);
      }
      else
      {
        _valuesSElevadores[jj] = P1.readDiscrete(ENTELEVADOR2, jj + 1);
      }
    }
    else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
      _valuesSElevadores[jj] = 1;
    }
  }


  if (EMGFlag)
  {
    if (stat) {
      _waitSElevadores = modbusTCPServer.coilRead(ManualOperationMode);
    }
    else {
      _waitSElevadores = true;
    }

    if (flagAutom) {
      _waitSElevadores = true;
    }

    //    _PP("_waitSElevadores = "); _PL(_waitSElevadores);

    if (_waitSElevadores)
    {
      bool _stage1 = modbusTCPServer.coilRead(enableStage1);
      bool _stage2 = modbusTCPServer.coilRead(enableStage2);
      bool _stage3 = modbusTCPServer.coilRead(enableStage3);



      if (stat) {

        //        _PL("Esperando Retiro de caja");

        if (_stage1 == true)
        {

          int idx_st1 = 0;
          if (_valuesEnablePedales[idx_st1] == 0)
          {

            //_pedalOK[idx_st1] = 1;
            LeerSensorWoPedal(0, 3);

          }
          else if (_valuesEnablePedales[idx_st1] == 1)
          {
            LeerSensorWithPedal(0, 3, 0);
          }
        }

        //-- Stage 2
        if (_stage2 == true)
        {
          int idx_st2 = 1;
          if (_valuesEnablePedales[idx_st2] == 0)
          {


            LeerSensorWoPedal(3, 6);

          }
          else if (_valuesEnablePedales[idx_st2] == 1)
          {

            LeerSensorWithPedal(3, 6, 1);
          }
        }

        //-- Stage 3
        if (_stage3 == true)
        {
          int idx_st3 = 2;
          if (_valuesEnablePedales[idx_st3] == 0)
          {

            //_pedalOK[idx_st3] = 1;
            LeerSensorWoPedal(6, 9);

          }
          else if (_valuesEnablePedales[idx_st3] == 1)
          {

            LeerSensorWithPedal(6, 9, 2);
          }
        }

      }

      else {
        for (int ii = 0; ii < 9; ii++)
        {
          if (_valuesSElevadores[ii] == 1)
          {
            modbusTCPServer.coilWrite(_coilsSElevators[ii], LOW);
          }
          else
          {
            modbusTCPServer.coilWrite(_coilsSElevators[ii], HIGH);
          }
        }
      }
    }
  }
}



void LeerSensoresNeumaticos() {
  // función para leer los sensores de los actuadores neumaticos y enviar la señal por MODBUS
  int _valuesSPistones[4] = {0, 0, 0, 0};
  _valuesSPistones[0] = P1.readDiscrete(ENTELEVADOR1, MPiston1IN);
  _valuesSPistones[1] = P1.readDiscrete(ENTELEVADOR1, MPiston1OUT);
  _valuesSPistones[2] = P1.readDiscrete(ENTELEVADOR1, MPiston2IN);
  _valuesSPistones[3] = P1.readDiscrete(ENTELEVADOR1, MPiston2OUT);
  for (int ii = 0; ii < 4; ii++)
  {
    if (_valuesSPistones[ii] == 1)
    {
      modbusTCPServer.coilWrite(_neumatiS[ii], LOW);
    }
    else
    {
      modbusTCPServer.coilWrite(_neumatiS[ii], HIGH);
    }

  }
}

void LeerSensorFeeder(bool stat)
{
  /*
      Actualiza si existe una caja en la plataforma del elevador
      del mesanin, solamente si se encuentra en el modo automatico.
  */

  if (EMGFlag)
  {
    _valueSFeeder = P1.readDiscrete(SlotE4S1BO, PinE4S1BO);
    if (stat)
    {
      if (_valueSFeeder != _lastValueSFeeder)
      {
        int flag = _valueSFeeder - _lastValueSFeeder;
        _lastValueSFeeder = _valueSFeeder;
        if (flag < 0)
        {
          _sendValueSFeeder = (_sendValueSFeeder >= 1)
                              ? (_sendValueSFeeder - 1)
                              : (_sendValueSFeeder);
          modbusTCPServer.coilWrite(SEFeeder, LOW);
        }
        else if (flag > 0)
        {
          _sendValueSFeeder += 1;
          modbusTCPServer.coilWrite(SEFeeder, HIGH);
        }
      }
      if (_sendValueSFeeder != _lastSendValueSFeeder)
      {
        _lastSendValueSFeeder = _sendValueSFeeder;
      }
    }
    else
    {
      if (_valueSFeeder == 1) {
        modbusTCPServer.coilWrite(SEFeeder, HIGH);
      }
      else if (_valueSFeeder == 0) {
        modbusTCPServer.coilWrite(SEFeeder, LOW);

      }

    }
  }

}

void LeerSensorCajaSorter(bool stat)
{
  /*

      Actualiza si existe una caja en la plataforma del Sorter del
      mesanin, solamente si se encuentra en el modo automatico.

  */

  if (EMGFlag)
  {
    _valueSCajaSorter = P1.readDiscrete(ENTELEVADOR1, CajaSorterMesanin);

    if (stat)
    {
      if (_valueSCajaSorter != _lastValueSCajaSorter)
      {
        int flag = _valueSCajaSorter - _lastValueSCajaSorter;
        _lastValueSCajaSorter = _valueSCajaSorter;
        if (flag < 0)
        {
          _sendValueSCajaSorter = (_sendValueSCajaSorter >= 1)
                                  ? (_sendValueSCajaSorter - 1)
                                  : (_sendValueSCajaSorter);
          modbusTCPServer.coilWrite(SEBox2Sorter, LOW);
        }
        else if (flag > 0)
        {
          _sendValueSCajaSorter += 1;
          modbusTCPServer.coilWrite(SEBox2Sorter, HIGH);
        }
      }

      if (_sendValueSCajaSorter != _lastSendValueSCajaSorter)
      {
        _lastSendValueSCajaSorter = _sendValueSCajaSorter;
      }
    }
    else {
      //      Serial.print("Sensor caja sorter = ");
      //      Serial.println(_valueSCajaSorter);

      if (_valueSCajaSorter == 1) {
        modbusTCPServer.coilWrite(SEBox2Sorter, HIGH);
      }
      else if (_valueSCajaSorter == 0) {
        modbusTCPServer.coilWrite(SEBox2Sorter, LOW);
      }
    }
  }
}

void LeerBotonSubida() {
  if (EMGFlag)
  {
    int BotonSubida = P1.readDiscrete(ENTELEVADOR2, 16);
    if (BotonSubida == 1) {
      modbusTCPServer.coilWrite(CoilDB, HIGH);
    }
    else if (BotonSubida == 0) {
      modbusTCPServer.coilWrite(CoilDB, LOW);
    }
  }
}

void  LeerSensorEntradaElevador() {
  if (EMGFlag)
  {
    int E4R2BO = P1.readDiscrete(ENTELEVADOR2, 10);
    if (E4R2BO == 1) {
      modbusTCPServer.coilWrite(CoilE4R2BO, HIGH);
    }
    else if (E4R2BO == 0) {
      modbusTCPServer.coilWrite(CoilE4R2BO, LOW);
    }

  }
}

void  LeerSensorSorter() {
  if (EMGFlag)
  {
    int E4R1BO = P1.readDiscrete(ENTELEVADOR1, 10);
    if (E4R1BO == 1) {
      modbusTCPServer.coilWrite(SEBox2Sorter , HIGH);
    }
    else if (E4R1BO == 0) {
      modbusTCPServer.coilWrite(SEBox2Sorter , LOW);
    }

  }
}

void llenar_queue() {
  int cuenta = 0;
  if (flag_queue) {

    for (int ii = 0; ii < 9; ii++) {
      if (queue_order[ii] == last_order) {
        cuenta += 1;
      }
    }

    if (cuenta == 0) {
      for (int ii = 0; ii < 9; ii++)
      {
        if (queue_order[ii] == -1) {
          queue_order[ii] = last_order;
          last_order = -1;
          flag_queue = false;
          break;
        }
      }
    }
    else {
      cuenta = 0;
      flag_queue = false;
    }
  }
}

void llenar_queue_sorter(int item) {
  int cuenta = 0;

  for (int ii = 0; ii < 9; ii++) {
    if (queue_sorter[ii] == item) {
      cuenta += 1;
    }
  }

  if (cuenta == 0) {
    for (int ii = 0; ii < 9; ii++)
    {
      if (queue_sorter[ii] == -1) {
        queue_sorter[ii] = item;
        item = -1;

        break;
      }
    }
  }
  else {
    cuenta = 0;

  }
}

void llenar_queue_elevadores(int item) {
  int cuenta = 0;


  if (flag_queue_elevadores) {
    for (int ii = 0; ii < 9; ii++) {
      if (queue_elevadores[ii] == item) {
        cuenta += 1;
      }
    }

    if (cuenta == 0) {
      for (int ii = 0; ii < 9; ii++)
      {
        if (queue_elevadores[ii] == -1) {
          queue_elevadores[ii] = item;
          flag_queue_elevadores = false;
          item = -1;

          break;
        }
      }
    }
    else {
      cuenta = 0;
      flag_queue_elevadores = false;

    }
  }
}



void restar_queue() {

  for (int ii = 0; ii < 9; ii++) {
    if (ii == 8) {
      queue_order[ii] = -1;
    }
    else {
      queue_order[ii] = queue_order[ii + 1];
    }
  }

}

void restar_queue_sorter() {

  for (int ii = 0; ii < 9; ii++) {
    if (ii == 8) {
      queue_sorter[ii] = -1;
    }
    else {
      queue_sorter[ii] = queue_sorter[ii + 1];
    }
  }
}

void restar_queue_elevadores() {
  for (int ii = 0; ii < 9; ii++) {
    if (ii == 8) {
      queue_elevadores[ii] = -1;
    }
    else {
      queue_elevadores[ii] = queue_elevadores[ii + 1];
    }
  }
}

void leerSensores() {
  EnableStages[0] = modbusTCPServer.coilRead(enableStage1);
  EnableStages[1] = modbusTCPServer.coilRead(enableStage2);
  EnableStages[2] = modbusTCPServer.coilRead(enableStage3);
  leerSensoresPulmones(false);
  LeerSensoresElevadores(false);
  LeerSensoresNeumaticos();
  LeerSensorFeeder(false);
  LeerSensorCajaSorter(false);
  LeerBotonSubida();
  LeerSensorSorter();
  LeerSensorEntradaElevador();
  LeerPedales();
}

void setup()
{
  Serial.begin(115200);

  pinMode(SWITCH_BUILTIN, INPUT);
  pinMode(LED_BUILTIN, OUTPUT);

  Ethernet.begin(mac, ip, myDns, gateway, subnet);                //-- Inicializa la conexion de Ethernet
  while (!P1.init())                                              //-- Espera hasta que la comunicacion serial con P1AM se establesca
  {
    digitalWrite(LED_BUILTIN, HIGH);
    delay(1000);
    digitalWrite(LED_BUILTIN, LOW);
    delay(1000);
  }
  digitalWrite(LED_BUILTIN, LOW);
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
  data.begin();                                                   //-- Inicia el servidor en el puerto 25
  server.begin();                                                 //-- Inicia el servidor en el puerto 502
  if (!modbusTCPServer.begin())                                   // start the Modbus TCP server
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
  modbusTCPServer.configureCoils(ADDRCOIL, NUMCOILS);             //-- Configura las direcciones para las bobinas
  modbusTCPServer.configureHoldingRegisters(ADDRREGS, NUMREGS);   //-- Configura las direcciones para los registros
  digitalWrite(LED_BUILTIN, HIGH);
  ts.setHighPriorityScheduler(&hs);
  initActuators();
  initSortEmpPos();

}


void loop()
{

  //si el switch button (boton del plc está en 1/arriba)
  if (digitalRead(SWITCH_BUILTIN))                                //-- Automatico
  {
    //Serial.println(digitalRead(SWITCH_BUILTIN));
    digitalWrite(LED_BUILTIN, HIGH);
    EthernetClient client = server.available();
    bool connectflag = false;
    if (digitalRead(SWITCH_BUILTIN))                                //-- Automatico
    {
      //Serial.println(digitalRead(SWITCH_BUILTIN));
      digitalWrite(LED_BUILTIN, HIGH);
      EthernetClient client = server.available();
      bool connectflag = false;
      //verifica que los actuadores neumaticos esten en la posicion inicial y el motor del sorter esté apagado.

      if (client)
      {
        _PL("TaskScheduler StatusRequest. Complex Test.");
        modbusTCPServer.accept(client);
        tCycle.enableDelayed();
        //-- Check2disableEmergencyStop

        while (client.connected())
        {
          if (connectflag == false) {
            _PL("conectado");
            connectflag = true;
            queue_fill = false;
          }
          modbusTCPServer.poll();
          ts.execute();
          //  leerSensores();
        }

        _elevMesaninUsed = false;
        _availableMesanin = true;

        //-- Handlers
        if  (!client.connected()) {
          tCycle.disable();
          _PL("desconectado");
          connectflag = false;
        }
      }
    }
  }
  //****************************************************************
  //*************** se habilita el modo de prueba *****************
  //****************************************************************

  else
  {
    if (millis() > tiempoAhora + 2000)
    {
      ledState = ledState ? 0 : 1;
      digitalWrite(LED_BUILTIN, ledState);
      tiempoAhora = millis();
    }

    EthernetClient client = server.available();
    if (client)
    {
      stateActuadores = 0x0005;
      modbusTCPServer.accept(client);
      while (client.connected())
      {
        if (millis() > tiempoAhora + 20)
        {
          modbusTCPServer.poll();
          manualModePLC();
          leerSensores();
          tiempoAhora = millis();
        }
      }
    }
    else {
      ciclo_prueba();
    }
  }
}

void ciclo_prueba() {
  //-- Enable TASK_FOREVER
  //-- Emergency stop

  //-- Handler
  tCycle.enableDelayed();
  //-- Check2disableEmergencyStop
  if (_InEmergencyStop == true)
  {
    _ES_Elevadores = disableEmergencyStop();
    _ES_Empujador = false;
    _ES_Sorter = false;
    _ES_SorterEmpujador = false;
  }
  else
  {
    _ES_Elevadores = false;
    _ES_Empujador = false;
    _ES_Sorter = false;
    _ES_SorterEmpujador = false;
    //-- Sensores
    tskSPedales.enableDelayed();    // Actualiza si estan conectados los pedales
    tskSElevators.enableDelayed();  // Lee los sensores de los Elevadores
    tskSPulmones.enableDelayed();   // Lee los Sensores de los Pulmones

    //-- Varios

    modbusTCPServer.coilWrite(ManualOperationMode, HIGH);
    modbusTCPServer.coilWrite(enableStage1, HIGH);
    modbusTCPServer.coilWrite(enableStage2, HIGH);
    modbusTCPServer.coilWrite(enableStage3, HIGH);
    modbusTCPServer.coilWrite(servosSON, HIGH);
    modbusTCPServer.coilWrite(servosSON, HIGH);
    manualModePLC();
    delay(1000);

    for (int jj = 0; jj < 11; jj++) {
      setPositionDOWNElevador(jj);
      elevador_arriba[jj] = false;
      delay(50);
    }
    setPositionEmpujador(EmpujadorLastPos + 1);
    delay(10);
    setPositionSorter(SorterLastPos + 1);

    EthernetClient client = server.available();
    _PL("Ejecutando Prueba");


    int incomingByte;
    bool loop_flag = false;
    _PL("Envie 0 al serial para iniciar");

    while (true)
    {
      if (Serial.available() > 0) {
        // read the incoming byte:
        incomingByte = Serial.read();
        // say what you got:
        //    Serial.print("I received: ");
        //    Serial.println(incomingByte, DEC);
      }

      if (incomingByte == 48) {
        loop_flag = true;
        flagPrueba = true;
        queue_fill = false;
        _PL("Inicio Prueba");
      }
      while (loop_flag) {
        modbusTCPServer.poll();
        ts.execute();
      }

    }
  }
}


void tMoverEmpujador() {

  int  Pos = queue_order[0];
  bool sorter_box = false;

  if (EMGFlag) {

    //-- Lectura sensores de los elevadores


    for (int jj = 0; jj < 9; jj++)
    {
      if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
        if (jj == 8)
        {
          _valuesSElevadores[jj] = P1.readDiscrete(SlotE3S3BO, PinE3S3BO);
          _valuesSPulmones[jj] = P1.readDiscrete(SlotE4T9BO, PinE4T9BO);
        }
        else
        {
          _valuesSElevadores[jj] = P1.readDiscrete(ENTELEVADOR2, jj + 1);
          _valuesSPulmones[jj] = P1.readDiscrete(ENTELEVADOR1, jj + 1);
        }
      }
      else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
        _valuesSElevadores[jj] = 1;
      }
    }


    _PL("Esperando Confirmacion de sensores Pulmon y Elevadores");

    if (_valuesSPulmones[Pos] == 1 &&  _valuesSElevadores[Pos] == 0) {

      _PL("Esperando CMD_OK del elevador");
      if ((task2_sorter == true && task3_sorter == false) || BandON == true) {
        sorter_box = true; // no permite que el empujador se mueva si el sorter está entregando caja.
      }
      else {
        sorter_box = false;
      }
      if ( P1.readDiscrete(ENTELEVADOR3, Pos + 1) == 1 && _checkEmpujadorTask1 == false && sorter_box == false) {
        setPositionEmpujador(Pos + 1);
        EmpujadorLastPos = Pos;
        _checkEmpujadorTask1 = true;
        _PP("Empujador a POS = "); _PL(Pos + 1);
        delay(10);
      }

      _PL("Esperando CMD_OK del Empujador");


      if ((P1.readDiscrete(ENTELEVADOR3, 12) == 1) && (_checkEmpujadorTask1 == true ) && (_checkEmpujadorTask2 == false) && P1.readDiscrete(ENTELEVADOR1, MPiston2IN) == 1 && P1.readDiscrete(ENTELEVADOR1, MPiston2OUT) == 0 && elevador_arriba[Pos] == true) {
        _PL("Piston 2 OUT");

        _checkEmpujadorTask2 = true;
        TriggerPstEmpujador();
      }
    }
    if (_valuesSPulmones[Pos] == 0 &&  _valuesSElevadores[Pos] == 0) {
      if ((_checkEmpujadorTask2 == true) && _checkEmpujadorTask3 == false && P1.readDiscrete(ENTELEVADOR1, MPiston2IN) == 0 && P1.readDiscrete(ENTELEVADOR1, MPiston2OUT) == 1 ) {
        _PL("Piston 2 IN");
        _PP("Elevador : "); _PP(Pos); _PL("  Baja");
        _checkEmpujadorTask3 = true;
        TriggerPstEmpujador();
        setPositionDOWNElevador(Pos + 1);
        elevador_arriba[Pos] = false;
      }
    }

    if ((_checkEmpujadorTask3 == true) && _valuesSPulmones[Pos] == 0 &&  _valuesSElevadores[Pos] == 1 && P1.readDiscrete(ENTELEVADOR1, MPiston2IN) == 1 && P1.readDiscrete(ENTELEVADOR1, MPiston2OUT) == 0) {
      restar_queue();
      _checkEmpujadorTask1 = false;
      _checkEmpujadorTask2 = false;
      _checkEmpujadorTask3 = false;
      if ((task1_sorter || task2_sorter) && (SorterLastPos == Pos || SorterLastPos == Pos + 2)) {
        _PL("esperando que el sorter termine su tarea");
      }
      else {

        if ((Pos - 1) >= 1) {
          if (Pos - 1  == SorterLastPos) {
            setPositionEmpujador(Pos + 1);
            EmpujadorLastPos = Pos;
          }
          else {

            setPositionEmpujador(Pos);
            EmpujadorLastPos = Pos - 1;
          }

        }
        else {
          if (Pos + 1 == SorterLastPos) {
            setPositionEmpujador(Pos + 1);
            EmpujadorLastPos = Pos;
          }
          else {
            setPositionEmpujador(Pos + 2);
            EmpujadorLastPos = Pos + 1;
          }
        }
      }


      llenar_queue_sorter(Pos);
      tskMoverEmpujador.disable();
      _PL("Empujador Task End");
    }
  }

}

void tSubirElevadorSorter() {
  /*      Actualiza si existe una caja en la plataforma del Sorter del
      mesanin, solamente si se encuentra en el modo automatico y si
      se cumplen las siguientes entradas:
            woSBoxSorter ->  P1.readDiscrete(ENTELEVADOR1, 10) == 0
            botonSubida -> P1.readDiscrete(ENTELEVADOR2, 16) == 1
            woSBoxFeeder -> P1.readDiscrete(ENTELEVADOR2, 10) == 1
            _availableTask -> modbusTCPServer.coilRead(waitFeederE ) == true
  */
  if (EMGFlag)
  {

    int botonSubida = P1.readDiscrete(ENTELEVADOR2, 16);  // boton doble confirmacion
    int woSBoxFeeder = P1.readDiscrete(SlotReflexElev, PinReflexElev); //Sensor reflex en el elevador
    int woSBoxSorter = P1.readDiscrete(SlotReflexSorter, PinReflexSorter); // sensor reflex en el sorter
    int boxFeeder = P1.readDiscrete(SlotE4S1BO, PinE4S1BO); // sensor de caja en el elevador
    int cuentaSPulmon = SensoresPulmones();
    bool _availableTask = modbusTCPServer.coilRead(waitFeederE);  //bandera para inhabilitar la ejecución de la tarea
    if (EMGFlag) {

      if (task1_elevador == false && queue_sorter[0] != -1) {
        _PL("Esperando Confirmacion para subir Elevador");
        _PL("Presionar Boton");
      }
      else if (task1_elevador == true && task2_elevador == false) {
        _PL("Esperando llegada del elevador");
      }
      else if (task2_elevador == true && task3_elevador == false) {
        _PL("Esperando Confirmacion del piston 1");
      }

      if (woSBoxSorter && queue_sorter[0] != -1 && task1_elevador == false && task2_elevador == false && task3_elevador == false) {
        task1_elevador = true;
        task2_elevador = true;
        task3_elevador = true;
        tskSubirElevadorSorter.disable();
        tskMoverSorter.enable();
      }

      if (P1.readDiscrete(ENTELEVADOR1, MPiston1IN) == 1 && P1.readDiscrete(ENTELEVADOR1, MPiston1OUT) == 0)
      {
        if (botonSubida == 1 && boxFeeder == 1 && woSBoxFeeder == 0 && woSBoxSorter == 0 && task1_elevador == false && queue_sorter[0] != -1)
        {
          setPositionUPElevador(10);
          elevador_arriba[10] = true;
          delay(10);
          modbusTCPServer.coilWrite(FeederElevator, HIGH);
          task1_elevador = true;
        }
        if (task1_elevador == true && task2_elevador == false) {
          if (P1.readDiscrete(ENTELEVADOR1, MPiston1IN) == 1 && P1.readDiscrete(ENTELEVADOR1, MPiston1OUT) == 0)
          {
            if (P1.readDiscrete(ENTELEVADOR3, CMDOK_EMESANIN) == 1 && P1.readDiscrete(ENTELEVADOR3, CMDOK_SORTER) == 1)
            {
              _PL("Verificando: Subida Elevador Sorter");
              _PL("Piston 1 salió");

              triggerPstElevador();
              task2_elevador = true;
            }
          }
        }
      }

      else if (P1.readDiscrete(ENTELEVADOR1, MPiston1IN) == 0 && P1.readDiscrete(ENTELEVADOR1, MPiston1OUT) == 1) {
        if (task2_elevador == true && task3_elevador == false && woSBoxSorter == 1) {
          _PL("Piston 1 Regresa");
          _PL("Elevador Sorter: Baja");
          task3_elevador = true;

          setPositionDOWNElevador(10);
          elevador_arriba[10] = false;
          triggerPstElevador();
          delay(10);
          tskSubirElevadorSorter.disable();
          tskMoverSorter.enable();
        }
      }

    }
  }
}

void tMoverSorter() {
  int  Pos = queue_sorter[0];
  int woSBoxSorter = P1.readDiscrete(SlotReflexSorter, PinReflexSorter); // sensor reflex en el sorter


  //-- Lectura sensores de los sensores del pulmon

  for (int jj = 0; jj < 9; jj++)
  {
    if ((jj < 3 && EnableStages[0] == 1) || (jj > 5 && EnableStages[2] == 1) || (jj >= 3 && jj <= 5 && EnableStages[1] == 1)) {
      if (jj == 8)
      {
        _valuesSPulmones[jj] = P1.readDiscrete(SlotE4T9BO, PinE4T9BO);
      }
      else
      {

        _valuesSPulmones[jj] = P1.readDiscrete(ENTELEVADOR1, jj + 1);
      }
    }
    else if ((jj < 3 && EnableStages[0] == 0) || (jj > 5 && EnableStages[2] == 0) || (jj >= 3 && jj <= 5 && EnableStages[1] == 0)) {
      _valuesSPulmones[jj] = 1;
    }
  }

  if (EMGFlag && flagAutom )
  {


    if (task3_elevador == true && task1_sorter == false && woSBoxSorter == 1 && _valuesSPulmones[Pos] == 0) {
      if (Pos == EmpujadorLastPos && P1.readDiscrete(ENTELEVADOR3, 12) == 1) {
        if ((Pos - 1) >= 1) {
          setPositionEmpujador(Pos);
          EmpujadorLastPos = Pos - 1;
        }
        else {
          setPositionEmpujador(Pos + 2);
          EmpujadorLastPos = Pos + 1 ;
        }
      }
      _PP("Sorter se mueve a la Posicion = "); _PL(Pos + 1);
      setPositionSorter(Pos + 1);
      SorterLastPos = Pos;
      delay(10);
      task1_sorter = true;
    }
    if (task1_sorter == true && task2_sorter == false && P1.readDiscrete(ENTELEVADOR3, CMDOK_SORTER) == 1) {
      _PP("Sorter llego a la posicion = "); _PL(Pos + 1);
      if (Pos == EmpujadorLastPos && P1.readDiscrete(ENTELEVADOR3, 12) == 1 && _checkEmpujadorTask1 == false) {
        if ((Pos - 1) >= 1) {
          setPositionEmpujador(Pos);
          EmpujadorLastPos = Pos - 1 ;
        }
        else {
          setPositionEmpujador(Pos + 2);
          EmpujadorLastPos = Pos + 1 ;
        }
      }
      _PP("Pos = "); _PL(Pos);
      _PP("Empujador Last Pos = "); _PL(EmpujadorLastPos);

      if (Pos != EmpujadorLastPos) {
        if (!BandON) {
          _PL("Se activa la banda del sorter");
          TriggerBandaSorter();
        }
        if (BandON) {
          task2_sorter = true;
        }
      }

    }
    if (task2_sorter == true && task3_sorter == false  && _valuesSPulmones[Pos] == 1) {
      _PL("Sorter Entregó la caja");
      _PL("Regresando a la Posición de inicio");
      setPositionSorter(5);
      SorterLastPos = 4;
      delay(10);
      if (BandON) {
        _PL("Se desactiva la banda del sorter");
        TriggerBandaSorter();
      }

      if (!BandON) {
        task3_sorter = true;
      }
    }

    if (task3_sorter == true && P1.readDiscrete(ENTELEVADOR3, CMDOK_SORTER) == 1) {
      _PL("Termina la tarea del Sorter");
      task1_elevador = false;
      task2_elevador = false;
      task3_elevador = false;
      task1_sorter = false;
      task2_sorter = false;
      task3_sorter = false;
      restar_queue_sorter();
      tskMoverSorter.disable();
    }




  }
}
void refill_queue() {
  //esta funcion pregunta por los sensores de los elevadores y compara contra la lista del queue;
  //si hay algun sensor activo que no esté en la lista y hay caja en los pulmones, lo mete en la lista.
  int cuenta = 0;
  int sPulmon = 0;
  int station;
  for (int ii = 0; ii < 9; ii++) {
    if  (_valuesSElevadores[ii] == 0) {

      for (int jj = 0; jj < 9; jj++) {

        if (ii == queue_order[jj]) {
          cuenta = 1;
        }

      }
      if (cuenta == 0 && _valuesSPulmones[ii] == 1) {
        if (ii < 3) {
          station = 0;
        }
        else if (ii > 5) {
          station = 2;
        }
        else {
          station = 1;
        }

        if ((_valuesEnablePedales[station] == 0) || (_valuesEnablePedales[station] == 1 && _valuesPedales[station] == 1)) {

          _PP("posicion sin llenar = "); _PL(ii + 1);
          last_order = ii;
          flag_queue = true;
          llenar_queue();
        }
      }
    }
    cuenta = 0;
  }
}

void TriggerBandaSorter() {


  if (BandON) {
    stateActuadores = stateActuadores ^ maskMot;
    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    _PP("Banda Sorter OFF - state: "); _PL(stateActuadores);
    BandON = false;
  }

  else if (P1.readDiscrete(ENTELEVADOR3, CMDOK_SORTER ) == 1 && P1.readDiscrete(ENTELEVADOR3, CMDOK_EMPUJADOR) == 1 ) { // solo prende la banda si el empujador y el sorter no se están moviendo
    if (!BandON) {
      stateActuadores = stateActuadores ^ maskMot;
      P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
      _PP("Banda Sorter ON - state: "); _PL(stateActuadores);
      BandON = true;
    }
  }
}


void TriggerPstEmpujador() {

  stateActuadores = stateActuadores ^ maskPST2;
  P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
  if (Piston2_Out) {
    Piston2_Out = false;
  }
  else {
    Piston2_Out = true;
  }
}

void triggerPstElevador()
{


  stateActuadores = stateActuadores ^ maskPST1;

  P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
  if (Piston1_Out) {
    Piston1_Out = false;
  }
  else {
    Piston1_Out = true;
  }

}

void resetMode() {
  if (flag_reset_system == false) {
    for (int jj = 0; jj < 11; jj++) {
      setPositionDOWNElevador(jj);
      elevador_arriba[jj] = false;
      delay(5);
    }
    for (int jj = 0; jj < 11; jj++) {
      flag_sensoresElevadores[jj] = false;
      queue_order[jj] = -1;
      //queue_sorter[jj] = -1;
      queue_elevadores[jj] =  -1;
    }
    EmpujadorLastPos = 5;
    SorterLastPos = 4;
    setPositionEmpujador(EmpujadorLastPos + 1);
    delay(10);
    setPositionSorter(SorterLastPos + 1);
    // reinicio de variables




    flag_queue = false; //bandera que indica que subió alguno de los elevadores, por lo que se debe agregar una nueva posición a la lista.
    flag_queue_elevadores = false;
    flag_queue_sorter = false; //bandera que indica que subió alguno de los elevadores, por lo que se debe agregar una nueva posición a la lista.
    flag_elevador_sorter = false; //bandera para verificar que el elevador subió, se pone en falso cuando la tarea del sorter se completa
    // banderas de las tareas del elevador
    task1_elevador = false;
    task2_elevador = false;
    task3_elevador = false;
    // banderas de las tareas del sorter
    task1_sorter = false;
    task2_sorter = false;
    task3_sorter = false;
    _checkEmpujadorTask1 = false;
    _checkEmpujadorTask2 = false;
    _checkEmpujadorTask3 = false;
    queue_fill = false;
    stateActuadores = 0x01E5; // servos en ON, y los actuadores en OFF.
    //    P1.writeDiscrete(stateActuadores, SLDELEVADOR4);
    flag_reset_system = true;

  }

}


void resume_operation() {
  //funcion para continuar la operacion de la maquina una vez se realicé el reset despues de un paro de emergencia
  int CMDOK_ALL = 0;
  for (int jj = 0; jj < 12; jj++) {
    CMDOK_ALL += P1.readDiscrete(ENTELEVADOR3, jj + 1);
  }
  _PP("CMDOK_ALL = "); _PL(CMDOK_ALL);
  if (CMDOK_ALL == 12) {
    flag_wait_reset = false;
    delay(300);
    SubirElevadorPostEMG();
    flag_reset_system = false;
    delay(100);
    _PL("resume_operation");
  }

}
