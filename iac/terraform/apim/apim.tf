resource "azurerm_api_management" "apim" {
  name                = var.api_management_name
  location            = var.resource_group_location
  resource_group_name = var.resource_group_name
  publisher_name      = var.publisher_name
  publisher_email     = var.publisher_email

  sku_name = var.sku_name

  virtual_network_type = "External"

  virtual_network_configuration {
      subnet_id = data.azurerm_subnet.apim.id
  }
}

resource "azurerm_api_management_api" "query-http-api" {
  name                = "query-http-api"
  resource_group_name = var.resource_group_name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Query HTTP API"
  path                = "query"
  service_url          = var.query_http_api_service_url
  protocols = ["https"]

  # import {
  #   content_format = "openapi-link"
  #   content_value  = "${var.query_http_api_service_url}/swagger/v1/swagger.json"
   
  # }

    oauth2_authorization {
    authorization_server_name = azurerm_api_management_authorization_server.api-standard-apim-authorization-server.name
  }
}


resource "azurerm_api_management_api" "command-http-api" {
  name                = "command-http-api"
  resource_group_name = var.resource_group_name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Command HTTP API"
  path                = "command"
  service_url          = var.command_http_api_service_url
  protocols = ["https"]

  import {
    content_format = "openapi-link"
    content_value  = "${var.command_http_api_service_url}/swagger/v1/swagger.json"
   
  }

    oauth2_authorization {
    authorization_server_name = azurerm_api_management_authorization_server.api-standard-apim-authorization-server.name
  }
}

resource "azurerm_api_management_product" "product" {
  product_id            = "speech-microservice-http-api"
  api_management_name   = azurerm_api_management.apim.name
  resource_group_name   = var.resource_group_name
  display_name          = "The Speech Micro Service  HTTP API Product"
  subscription_required = false
  approval_required     = false
  published             = true
}

resource "azurerm_api_management_product_api" "product_query_http_api" {
  api_name            = azurerm_api_management_api.query-http-api.name
  product_id          = azurerm_api_management_product.product.product_id
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = var.resource_group_name
}


resource "azurerm_api_management_product_api" "product_command_http_api" {
  api_name            = azurerm_api_management_api.command-http-api.name
  product_id          = azurerm_api_management_product.product.product_id
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = var.resource_group_name
}


resource "azurerm_api_management_authorization_server" "api-standard-apim-authorization-server" {
  name                = "apim-authorization-server"
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = var.resource_group_name
  display_name        = "oauth2 authorization Server"

  authorization_endpoint = "https://workshopb2clogcorner.b2clogin.com/workshopb2clogcorner.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/authorize"
  token_endpoint         = "https://workshopb2clogcorner.b2clogin.com/workshopb2clogcorner.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/token"

  client_id                    = "63ef158a-ce8b-4d2f-b078-10bd8f404b02"
  client_registration_endpoint = "http://localhost"
  
  default_scope                = "https://workshopb2clogcorner.onmicrosoft.com/command/api/Speech.Create"


  client_secret = ""

  grant_types = [
    "authorizationCode",
  ]

  client_authentication_method = [
    "Body"
  ]

  authorization_methods        = ["GET", "POST","PUT","DELETE"]
  bearer_token_sending_methods = ["authorizationHeader"]
}
