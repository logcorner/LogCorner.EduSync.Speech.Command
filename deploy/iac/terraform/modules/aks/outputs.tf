# output "resource_group_name" {
#   description = "The name of the resource group"
#   value       = azurerm_resource_group.default.name
# }

output "kubernetes_cluster_name" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_kubernetes_cluster.default.name
}

output "container_registry_name" {
  description = "The name of the azure container registry"
  value       = azurerm_container_registry.default.name
}