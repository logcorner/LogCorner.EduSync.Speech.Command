resource_group_location   = "westeurope"
resource_group_name = "demo-apim-dev"
#---------------   azure kubernetes services ----------------------------------------
aks_name   = "demo-apim-aks-dev"
node_count  = 3
node_type   = "Standard_D2s_v3"
dns_prefix  = "tfq"
environment  = "dev"

#---------------   azure container registry ----------------------------------------

acr_name= "locornermsacrdev"
sku  ="Standard"

default_tags = {
  environment = "test"
  deployed_by = "terraform"
}


