resource "azuread_application" "swagger_ui_application" {
  display_name     = "LogCorner.EduSync.Speech.SwaggerUI"
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"
  web {
    redirect_uris = ["http://localhost:6000/swagger/oauth2-redirect.html",
    "https://localhost:6001/swagger/oauth2-redirect.html"]

  }
  api {
    requested_access_token_version = 2
  }

  required_resource_access {
    resource_app_id = azuread_application.web_api_application.application_id

    resource_access {
      id   = element(random_uuid.random_id[*].result, 0) 
      type = "Scope"

    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 1) 
      type = "Scope"
    }

    resource_access {
      id   = element(random_uuid.random_id[*].result, 2) 
      type = "Scope"
    }
   
  }

  required_resource_access {
    resource_app_id = azuread_application.notification_server_application.application_id

    resource_access {
      id   = element(random_uuid.random_id[*].result, 3)
      type = "Scope"
    }
  }
}
resource "azuread_application_password" "swagger_ui_application_password" {
  display_name          = "secret"
  application_object_id = azuread_application.swagger_ui_application.object_id
}

resource "azuread_service_principal" "swagger_ui_application" {
  application_id = azuread_application.swagger_ui_application.application_id
}

resource "azurerm_key_vault_secret" "swagger_ui_application_clientid" {
  name         = "SwaggerUI--OAuthClientId"
  value        = azuread_application.swagger_ui_application.application_id
  key_vault_id = data.azurerm_key_vault.main.id
}
resource "azurerm_key_vault_secret" "swagger_ui_application_secret" {
  name         = "SwaggerUI--OAuthClientSecret"
  value        = azuread_application_password.swagger_ui_application_password.value
  key_vault_id = data.azurerm_key_vault.main.id
}

resource "azurerm_key_vault_secret" "swagger_ui_tenant_name" {
  name         = "SwaggerUI--TenantName"
  value        = var.tenantName
  key_vault_id = data.azurerm_key_vault.main.id
}

output "application_password" {
  sensitive = true
  value     = azuread_application_password.swagger_ui_application_password.value
}

output "api_client_id" {
  description = "API CLIENT_ID"
  value       = azuread_application.web_api_application.application_id
}

output "client_id" {
  description = "client CLIENT_ID"
  value       = azuread_application.swagger_ui_application.application_id
}