#Requires -RunAsAdministrator
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

$url = "https://localhost:44300/"

Set-Location 'C:\Program Files (x86)\IIS Express\'
.\IisExpressAdminCmd.exe setupsslUrl -url:$url -UseSelfSigned
Read-Host -Prompt "Press any key to continue or CTRL+C to quit" 