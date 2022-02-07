resource_group_location   = "westus"
resource_group_name = "demo-tfquickstart-test"
#---------------   azure kubernetes services ----------------------------------------
aks_name   = "demo-tfquickstart-aks-test"
node_count  = 3
node_type   = "Standard_D2s_v3"
dns_prefix  = "tfq"
environment  = "test"

#---------------   azure container registry ----------------------------------------

acr_name= "locornermsacrtest"
sku  ="Standard"

default_tags = {
  environment = "test"
  deployed_by = "terraform"
}
