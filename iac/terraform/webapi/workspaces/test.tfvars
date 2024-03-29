resource_group_location   = "westeurope"
resource_group_name = "LOGCORNER-MICROSERVICES-IAC"
#---------------   azure kubernetes services ----------------------------------------
aks_name   = "demo-apim-aks-test"
node_count  = 3
node_type   = "Standard_D4s_v4"
dns_prefix  = "tfq"
environment  = "test"

#---------------   azure container registry ----------------------------------------

acr_name= "locornermsacrtest"
sku  ="Standard"

default_tags = {
  environment = "test"
  deployed_by = "terraform"
}

#---------------   azure ad application registration ----------------------------------------

tenantName = "workshopb2clogcorner"
ConfidentialClientDisplayName="LogCorner.EduSync.ConfidentialClient"