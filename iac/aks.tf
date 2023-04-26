resource "azurerm_kubernetes_cluster" "kubernetes_cluster" {
  name                = var.aks_name
  location            = azurerm_resource_group.rg.location
  dns_prefix          = var.aks_dns_prefix
  resource_group_name = azurerm_resource_group.rg.name

  default_node_pool {
    name            = "agentpool"
    node_count      = var.aks_agent_count
    vm_size         = var.aks_agent_vm_size
    os_disk_size_gb = var.aks_agent_os_disk_size
    vnet_subnet_id  = resource.azurerm_subnet.kubesubnet.id

  }

  identity {
    type = "SystemAssigned"
  }

  network_profile {
    network_plugin     = var.network_plugin
    network_policy     = var.network_policy
    dns_service_ip     = var.aks_dns_service_ip
    docker_bridge_cidr = var.aks_docker_bridge_cidr
    service_cidr       = var.aks_service_cidr
    load_balancer_sku  = var.load_balancer_sku
  }
  key_vault_secrets_provider {
    secret_rotation_enabled = true
  }
  depends_on = [
    azurerm_virtual_network.vnet
  ]

  tags = (merge(var.default_tags, tomap({
    type = "kubernetes_cluster"
    })
  ))
}

output "fqdn" {
  description = "The FQDN of the Azure Kubernetes Managed Cluster."
  value       = azurerm_kubernetes_cluster.kubernetes_cluster.fqdn
}
