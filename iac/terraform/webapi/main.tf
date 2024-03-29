resource "azurerm_resource_group" "resource_group" {
  name     = var.resource_group_name
  location = var.resource_group_location
  tags = (merge(var.default_tags, tomap({
    type = "resourcegroup"
    })
  ))
}

module "logcorner-vnet" {
  source                       = "./modules/vnet"
  resource_group_location      = azurerm_resource_group.resource_group.location
  resource_group_name          = azurerm_resource_group.resource_group.name
  kubernetes_cluster_principal = module.logcorner-kubernetes_service.kubernetes_cluster_principal
}



module "logcorner-kubernetes_service" {
  source                  = "./modules/aks"
  resource_group_location = azurerm_resource_group.resource_group.location
  resource_group_name     = azurerm_resource_group.resource_group.name
  aks_name                = var.aks_name
  node_count              = var.node_count
  node_type               = var.node_type
  dns_prefix              = var.dns_prefix
  subnet_aks_id           = module.logcorner-vnet.subnet_aks_id
  subnet_agic_id          = module.logcorner-vnet.subnet_agic_id
  environment             = var.environment
  tags = (merge(var.default_tags, tomap({
    type = "aks"
    })
  ))
}

module "logcorner-container_registry" {
  source                      = "./modules/acr"
  resource_group_location     = azurerm_resource_group.resource_group.location
  resource_group_name         = azurerm_resource_group.resource_group.name
  acr_name                    = var.acr_name
  sku                         = var.sku
  kubernetes_cluster_identity = module.logcorner-kubernetes_service.kubernetes_cluster_identity
  tags = (merge(var.default_tags, tomap({
    type = "acr"
    })
  ))
}

