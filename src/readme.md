# Modules

  ```powershell
  Import-Module .\src\Package\Debug\ResourceManager\AzureResourceManager\AzureRM.Profile\AzureRM.Profile.psd1
  ```

- `Get-AzureRmEnvironment`
- `Login-AzureRmAccount`

  - US Gov

    ```powershell
    Login-AzureRmAccount -Environment AzureUSGovernment
    ```

## Dynamic Parameters

- `Environment`
- `Location`

## Long Running Operations

- Remove-AzureRmResourceGroup

https://github.com/Azure/azure-powershell/blob/stack-dev/tools/AzureRM.BootStrapper/AzureRM.Bootstrapper.psm1#L985