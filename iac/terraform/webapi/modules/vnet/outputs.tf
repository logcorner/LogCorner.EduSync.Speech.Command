
output "subnet_aks_id" {
  description = "The name of the azure kubernetes service cluster"
  value       = azurerm_subnet.aks.id
}

output "subnet_agic_id" {
  value       = azurerm_subnet.agic-aks.id
}

output "subnet_apim_id" {
  value       = azurerm_subnet.apim.id
}