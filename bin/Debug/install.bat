sc create "PUC AFB" binPath= "C:\Users\weston.l.smith\Desktop\Solutions\PUC AFB\bin\Debug\PUC_AFB.exe" DisplayName= "PUC AFB" start= auto 
sc description "PUC AFB" "PUC Automated File Backup - Backs up local files to alternate storage location, typically a NAS device."
sc failure "PUC AFB" reset= 86400 actions= restart/60000/restart/60000/restart/60000
pause