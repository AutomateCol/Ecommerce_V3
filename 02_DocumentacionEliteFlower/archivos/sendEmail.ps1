<#
    Version: 1.2.0
    
    Script para mandar el backup de los logs del programa EliteFlower
    
    Creado por:            Santiago Jiménez
    Fecha creacion:        05/04/2021
    Fecha modificacion:    07/04/2021
    Written by AUTOMATE
    Copyright (c) 2021 AUTOMATE, LLC
#>

<# VARIABLES PARA ALMACENAR ARCHIVO #>
$myLogFile = "logfile.json";
$myBackupFile = "backup.zip";

cd C:\Users\Usuario\   <# Cambia la ruta del CLI para almacenar el backup #>
mongoexport --db EliteFlower --collection Summary --out $myLogFile   <# Exporta los documentos de los logs de la DB #>
Compress-Archive -Path (Join-Path ($pwd).Path $myLogFile) -DestinationPath (Join-Path ($pwd).Path $myBackupFile) -Force <# Comprime los datos de los logs en un zip #>
$lines = Get-Content $myLogFile | Measure-Object -Line


<# VARIABLES PARA REVISAR CONEXION ETHERNET #>
$pingRoute = "www.google.com";
$numPings = 6;

$Connected = Test-Connection -ComputerName $pingRoute -Count $numPings -Quiet <# Verifica si el PC tiene conexion a internet #>

if ($Connected) {
    <# VARIABLES PARA ENVIAR POR CORREO #>
    $EmailPropio = "logseliteflower@gmail.com";
    $EmailDestino = "logseliteflower@gmail.com";
    $Password = "eliteflowermesanin2021";

    $Asunto = "BK-LOGS";
    $Texto = "El archivo contiene " + $lines.Lines + " logs";
    $Archivo = Join-Path ($pwd).Path $myBackupFile;
    $ServidorSMTP = "smtp.gmail.com";

    $Mensaje = New-Object System.Net.Mail.MailMessage;
    $Mensaje.From = $EmailPropio;
    $Mensaje.To.Add($EmailDestino);
    $Mensaje.IsBodyHtml = $True;
    $Mensaje.Subject = $Asunto;
    $Mensaje.Body = $Texto;
    $Adjunto = New-Object Net.Mail.Attachment($Archivo);
    $Mensaje.Attachments.Add($Adjunto);
    $ClienteSMTP = New-Object Net.Mail.SmtpClient($ServidorSMTP, 587);
    $ClienteSMTP.EnableSsl = $true;
    $ClienteSMTP.Credentials = New-Object System.Net.NetworkCredential($EmailPropio, $Password);
    $ClienteSMTP.Send($Mensaje);
}
else {
    Write-Host "Error connection";
}
