

# The main resource group for this deployment
resource "azurerm_resource_group" "default" {
  name     = "${var.name}-${var.environment}-rg"
  location = var.location
}

