resource "random_id" "log_analytics_workspace_name_suffix" {
    byte_length = 8
}

locals {
  cluster_name = "${replace(var.dns_prefix, "-", "")}${replace(var.name, "-", "")}clutser"
}
resource "azurerm_log_analytics_workspace" "test" {
    # The WorkSpace name has to be unique across the whole of azure, not just the current subscription/tenant.
    name                = "${var.log_analytics_workspace_name}-${random_id.log_analytics_workspace_name_suffix.dec}"
    location            = "${azurerm_resource_group.default.location}"
    resource_group_name = "${azurerm_resource_group.default.name}"
    sku                 = var.log_analytics_workspace_sku
}

resource "azurerm_log_analytics_solution" "test" {
    solution_name         = "ContainerInsights"
    location              = azurerm_log_analytics_workspace.test.location
    resource_group_name   = "${azurerm_resource_group.default.name}"
    workspace_resource_id = azurerm_log_analytics_workspace.test.id
    workspace_name        = azurerm_log_analytics_workspace.test.name

    plan {
        publisher = "Microsoft"
        product   = "OMSGallery/ContainerInsights"
    }
}

resource "azurerm_kubernetes_cluster" "k8s" {
    name                =  "${local.cluster_name}"
    location            = "${azurerm_resource_group.default.location}"
    resource_group_name = "${azurerm_resource_group.default.name}"
    dns_prefix          = var.dns_prefix

    linux_profile {
        admin_username = "ubuntu"

        ssh_key {
            key_data = file(var.ssh_public_key)
        }
    }

    default_node_pool {
        name            = "agentpool"
        node_count      = var.agent_count
        vm_size         = "Standard_D2_v2"
    }

/*     service_principal {
        client_id     = var.client_id
        client_secret = var.client_secret
    } */

    addon_profile {
        oms_agent {
        enabled                    = true
        log_analytics_workspace_id = azurerm_log_analytics_workspace.test.id
        }
    }

    network_profile {
        load_balancer_sku = "Standard"
        network_plugin = "kubenet"
    }

  role_based_access_control {
    enabled = true
  }
   # azure will assign the id automatically
  identity {
    type = "SystemAssigned"
  }
    tags = {
        Environment = "Development"
    }
}
