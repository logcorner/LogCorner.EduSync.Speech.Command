resource_group_location   = "westus"
resource_group_name = "demo-tfquickstart-dev"
#---------------   azure kubernetes services ----------------------------------------
aks_name   = "demo-tfquickstart-aks-dev"
node_count  = 3
node_type   = "Standard_D2s_v3"
dns_prefix  = "tfq"
environment  = "dev"

#---------------   azure container registry ----------------------------------------

acr_name= "locornermsacrdev"
sku  ="Standard"

