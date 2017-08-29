Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.Profile\AzureRM.Profile.psd1
Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.Resources\AzureRM.Resources.psd1
Import-Module .\src\Package\Debug\Storage\Azure.Storage\Azure.Storage.psd1
Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.Storage\AzureRM.Storage.psd1
Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.TrafficManager\AzureRM.TrafficManager.psd1
Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.Sql\AzureRM.Sql.psd1

# validates:
# - USGovernmentServiceEndpoint
# - USGovernmentResourceManagerEndpoint
# - USGovernmentActiveDirectoryEndpoint

# Login-AzureRmAccount -Environment AzureUSGovernment
# $location = "USGov Iowa"
# Login-AzureRmAccount
$location = "North Central US"


$resourceGroup = "rgmy"
$trafficManagerProfileName = "tmmy"

Get-AzureRmResourceProvider
New-AzureRmResourceGroup -Name $resourceGroup -Location $location

Get-AzureRmStorageUsage

New-AzureRmTrafficManagerProfile `
    -ResourceGroupName $resourceGroup `
    -Name $trafficManagerProfileName `
    -RelativeDnsName $trafficManagerProfileName `
    -TrafficRoutingMethod Geographic `
    -MonitorProtocol HTTP `
    -MonitorPort 8000 `
    -MonitorPath / `
    -Ttl 0

Get-AzureRmTrafficManagerProfile

#New-AzureRmTrafficManagerEndpoint `
#    -ResourceGroupName $resourceGroup `
#    -ProfileName $trafficManagerProfileName `
#    -Name epmy `
#    -Type AzureEndpoints `
#    -EndpointStatus Disabled `
#    -TargetResourceId aaa

#New-AzureRmTrafficManagerEndpoint `
#    -ResourceGroupName $resourceGroup `
#    -ProfileName $trafficManagerProfileName `
#    -Name epmy `
#    -Type ExternalEndpoints `
#    -EndpointStatus Disabled `
#    -Target "example.com" `
#    -GeoMapping @("Africa") `
#    -EndpointLocation "Africa"

# Get-AzureRmTrafficManagerEndpoint -ResourceGroupName $resourceGroup -Name $trafficManagerProfileName -Type AzureEndpoints -ProfileName $trafficManagerProfileName

# Remove-AzureRmResourceGroup -Name $resourceGroup -Force