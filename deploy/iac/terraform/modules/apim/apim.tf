resource "azurerm_api_management" "apim" {
  name                = "logcorner-apim-agic-speech"
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

resource "azurerm_api_management_api" "query-http-api" {
  name                = "query-http-api"
  resource_group_name = var.resource_group_name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Query HTTP API"
  path                = "query"
  service_url          = "http://10.10.1.35"//"https://conferenceapi.azurewebsites.net"
  #service_url          = "https://conferenceapi.azurewebsites.net"
  protocols = ["https"]

  import {
    content_format = "openapi-link"
    content_value  = "http://10.10.1.35/swagger/v1/swagger.json"//"https://conferenceapi.azurewebsites.net/?format=json"
    #content_value  = "https://conferenceapi.azurewebsites.net/?format=json"
  }

    oauth2_authorization {
    authorization_server_name = azurerm_api_management_authorization_server.api-standard-apim-authorization-server.name
  }
}


resource "azurerm_api_management_api" "command-http-api" {
  name                = "command-http-api"
  resource_group_name = var.resource_group_name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Commande HTTP API"
  path                = "command"
  service_url          = "http://10.10.1.36"//"https://conferenceapi.azurewebsites.net"
  #service_url          = "https://conferenceapi.azurewebsites.net"
  protocols = ["https"]

  import {
    content_format = "openapi-link"
    content_value  = "http://10.10.1.36/swagger/v1/swagger.json"//"https://conferenceapi.azurewebsites.net/?format=json"
    #content_value  = "https://conferenceapi.azurewebsites.net/?format=json"
  }

    oauth2_authorization {
    authorization_server_name = azurerm_api_management_authorization_server.api-standard-apim-authorization-server.name
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

  authorization_endpoint = "https://datasynchrob2c.b2clogin.com/datasynchrob2c.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/authorize"
  token_endpoint         = "https://datasynchrob2c.b2clogin.com/datasynchrob2c.onmicrosoft.com/B2C_1_SignUpIn/oauth2/v2.0/token"

  client_id                    = "40a32973-b38a-4785-a568-1bc9f434dad1"
  client_registration_endpoint = "http://localhost"
  
  default_scope                = "https://datasynchrob2c.onmicrosoft.com/query/api/Speech.List"


  client_secret = "e9b7Q~PERfZL16cuU1Tp91Kn0zEfpUSkn~ZRs"

  grant_types = [
    "authorizationCode",
  ]

  client_authentication_method = [
    "Body"
  ]

  authorization_methods        = ["GET", "POST","PUT","DELETE"]
  bearer_token_sending_methods = ["authorizationHeader"]
}
