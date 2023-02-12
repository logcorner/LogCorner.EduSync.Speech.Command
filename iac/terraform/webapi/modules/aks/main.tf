resource "azurerm_kubernetes_cluster" "aks" {
  name                = var.aks_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  dns_prefix          = "${var.dns_prefix}-${var.resource_group_name}-aks-${var.environment}"

  node_resource_group = "${var.resource_group_name}-node-rg"

  default_node_pool {
    name       = "agentpool"
    node_count = var.node_count
    vm_size    = var.node_type
    vnet_subnet_id = var.subnet_aks_id
 }

  identity {
    type = "SystemAssigned"
  }


  addon_profile {
    oms_agent {
      enabled                    = true
      log_analytics_workspace_id = azurerm_log_analytics_workspace.Log_Analytics_WorkSpace.id
    }

    ingress_application_gateway {
      enabled   = true
      subnet_id = var.subnet_agic_id
    }

  }

  network_profile {
    load_balancer_sku = "standard"
    network_plugin    = "azure"
  }


  tags = var.tags
}

