
output "kubernetes_cluster_identity" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_kubernetes_cluster.default.kubelet_identity[0].object_id
}

