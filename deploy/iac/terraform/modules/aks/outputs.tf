
output "kubernetes_cluster_identity" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_kubernetes_cluster.default.kubelet_identity[0].object_id
}


output "kubernetes_cluster_principal" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_kubernetes_cluster.default.identity[0].principal_id
}

