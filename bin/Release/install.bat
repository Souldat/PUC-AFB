sc create "Hindsight-G2 Storage Backup" binPath= "C:\Program Files (x86)\Exacom, Inc\Hindsight-G2\Storage Backup\Hindsight-G2_StorageBackup.exe" DisplayName= "Hindsight-G2 Storage Backup" start= auto 
sc description "Hindsight-G2 Storage Backup" "Backs up local storage containers to alternate storage location, typically a NAS device."
sc failure "Hindsight-G2 Storage Backup" reset= 86400 actions= restart/60000/restart/60000/restart/60000
pause