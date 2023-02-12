
variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
}

variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
}

variable "query_http_api_service_url" {
  default = "https://conferenceapi.azurewebsites.net"
}

variable "command_http_api_service_url" {
  default = "http://10.10.1.35"
}

variable "virtual_network_name" {
default = "apim-aks-vnet"
}

variable "subnet_id" {
  default = "apim-subnet"
}

variable "api_management_name" {
  default ="logcorner-apim-agic-speech"
}

variable "sku_name" {
  default ="Developer_1"
}

variable "publisher_name" {
  default ="logconer"
}

variable "publisher_email" {
  default=  "tocane.techhnologies@gmail.com"
}