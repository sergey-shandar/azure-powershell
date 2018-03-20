[pscredential] $credential = Get-Credential
[string] $imgName = "myimg4"

$vm = New-AzureRmVm -Name $imgName -Credential $credential -DataDiskSizeInGb 16,32

# Windows sysprep https://docs.microsoft.com/en-us/azure/virtual-machines/windows/capture-image-resource
$tmp = New-TemporaryFile
'& "$env:windir\System32\sysprep\sysprep" /oobe /generalize /quit' | Out-File $tmp.FullName
Invoke-AzureRmVMRunCommand -VMName $imgName -ResourceGroupName $imgName -ScriptPath $tmp.FullName -CommandId RunPowerShellScript

# Linux deprovision https://docs.microsoft.com/en-us/azure/virtual-machines/linux/capture-image#step-1-deprovision-the-vm

Stop-AzureRmVm -ResourceGroupName $imgName -Name $imgName -Force
Set-AzureRmVm -ResourceGroupName $imgName -Name $imgName -Generalized

$img = New-AzureRmImageConfig -Location eastus -SourceVirtualMachineId $vm.Id
$img = New-AzureRmImage -ResourceGroupName $imgName -Name $imgName -Image $img

[string] $vmssName = "myvmss"
$vmss = New-AzureRmVmss -Name $vmssName -Credential $credential -ImageName $img.Id # -DataDiskSizeInGb 64