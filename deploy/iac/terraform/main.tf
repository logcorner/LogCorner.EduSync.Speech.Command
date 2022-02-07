

# The main resource group for this deployment
resource "azurerm_resource_group" "default" {
  name     = var.resource_group_name
  location = var.resource_group_location
}

module "logcorner-kubernetes_service" {
  source = "./modules/aks"

}

module "logcorner-container_registry" {
  source                      = "./modules/acr"
  kubernetes_cluster_identity = module.logcorner-kubernetes_service.kubernetes_cluster_identity
}