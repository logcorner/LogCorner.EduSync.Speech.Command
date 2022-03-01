resource "azurerm_api_management" "apim" {
  name                = "logcorner-apim-speech"
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  publisher_name      = "Logcorner"
  publisher_email     = "xyz@microsoft.com"

  sku_name = "Developer_1"

  virtual_network_type = "External"

  virtual_network_configuration {
      subnet_id = "${azurerm_subnet.apim.id}"
  }
}

resource "azurerm_api_management_api" "back-end-api" {
  name                = "command-http-api"
  resource_group_name = var.resource_group_name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Command HTTP API"
  path                = "api"
  service_url          = "http://10.10.1.5"
  protocols = ["https"]

  import {
    content_format = "openapi-link"
    content_value  = "http://10.10.1.5/swagger/v1/swagger.json"
  }
}

resource "azurerm_api_management_product" "product" {
  product_id            = "speech-microservice-command-http-api"
  api_management_name   = azurerm_api_management.apim.name
  resource_group_name   = var.resource_group_name
  display_name          = "The Speech Micro Service Command HTTP API Product"
  subscription_required = false
  approval_required     = false
  published             = true
}

resource "azurerm_api_management_product_api" "example" {
  api_name            = azurerm_api_management_api.back-end-api.name
  product_id          = azurerm_api_management_product.product.product_id
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = var.resource_group_name
}