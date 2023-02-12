<# 
./storageAccount.ps1 -ResourceGroupName 'LOGCORNER-MICROSERVICES-IAC' `
                     -location 'westeurope' `
                     -storageAccName 'tfbackenglogconer' `
                     -storageAccSkuName 'Standard_LRS' `
                     -storageResoucesContainerName 'backend-tfstate-web-api-resources'  `
                     -application 'test'  `
                     -environment 'dev'
 #>
Param
(
    [Parameter(Mandatory=$true, HelpMessage = "Please provide a resource group name")]
    [string]$ResourceGroupName,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide a resource group location")]
    [string]$location
    ,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide a storageAccName")]
    [string]$storageAccName
    ,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide a resource group location")]
    [string]$storageAccSkuName
    ,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide a resource group location")]
    [string]$storageResoucesContainerName
    # ,
    # [Parameter(Mandatory =$false, HelpMessage = "Please provide a resource group location")]
    # [System.Collections.Hashtable]$tags
    ,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide an application")]
    [string]$application
    ,
    [Parameter(Mandatory =$true, HelpMessage = "Please provide an environment")]
    [string]$environment
)

$StorageHT = @{
    ResourceGroupName = $ResourceGroupName
    Name              =  $storageAccName 
    SkuName           =  $storageAccSkuName
    Location          =  $Location
  }

$storageContainerName = $storageResoucesContainerName

$tags = @{application=$application
          environment=$environment
          deployed_by ='terraform'
        }

Get-AzResourceGroup -Name $ResourceGroupName -ErrorVariable notPresent -ErrorAction SilentlyContinue
Write-Host -ForegroundColor Green " *************** Creating resource group.. ****************"
if ($notPresent)
{
    Write-Host -ForegroundColor Yellow "resource group does not exist"
    New-AzResourceGroup -Name $ResourceGroupName -Location $location -Tag $tags
    Write-Host -ForegroundColor Green "$ResourceGroupName created succesfully at location $location"
}
else {
    Write-Host -ForegroundColor Yellow "resource group already  exist"
    Write-Host  -ForegroundColor Magenta "using resource group  $ResourceGroupName on location $location "
}

Write-Host -ForegroundColor Green "*************** Creating storage account.. ***************"  
## Get the storage account in which container has to be created  
$storageAcc=Get-AzStorageAccount -ResourceGroupName $ResourceGroupName -Name $storageAccName -ErrorAction SilentlyContinue
if(!$storageAcc){ 
    Write-Host -ForegroundColor Yellow "storage account does not exist.."    
    $storageAcc = New-AzStorageAccount @StorageHT -Tag $tags
    Write-Host -ForegroundColor Green "$storageAccName created succesfully at location $location"
}
else {
    Write-Host -ForegroundColor Yellow "storage account already  exist"
    Write-Host  -ForegroundColor Magenta "using storage account $storageAccName on location $location "
}

 ## Get the storage account context  
 $ctx=$storageAcc.Context      
 Write-Host -ForegroundColor Green "*************** Creating storage container.. ***************"  

# Foreach ($storageContainerName in $storageContainerNames)
# {
    ## Check if the storage container exists  
    if(Get-AzStorageContainer -Name $storageContainerName -Context $ctx -ErrorAction SilentlyContinue)  
    {  
        Write-Host -ForegroundColor Yellow $storageContainerName "- container already exists."  
        Write-Host  -ForegroundColor Magenta "using storage container $storageContainerName on location $location "
    }  
    else  
    {  
        Write-Host -ForegroundColor Magenta $storageContainerName "- container does not exist."   
        ## Create a new Azure Storage Account  
        New-AzStorageContainer -Name $storageContainerName -Context $ctx -Permission Container  
        Write-Host -ForegroundColor Green " $storageContainerName created succesfully at location $location"
    }
# }