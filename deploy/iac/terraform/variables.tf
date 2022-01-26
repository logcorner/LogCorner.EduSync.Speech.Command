//Service principal
variable "client_secret" {
   type        = string
  description = "Client secret of the service principal"
  default     = "onJ7Q~Ym7vUi9viyWfiOPh4BmvQt5YqxKAMZR"
}
variable "client_id" {
   type        = string
  description = "client_id of the service principal"
  default     = "5c4919f0-7d40-40ec-837c-8a6a73c47ed3"
}

variable "tenant_id" {
   type        = string
  description = "tenant_id of the service principal"
  default     = "f12a747a-cddf-4426-96ff-ebe055e215a3"
}


// Naming
variable "name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "demo-tfquickstart"
}

variable "environment" {
  type        = string
  description = "Name of the deployment environment"
  default     = "dev"
}

// Resource information

variable "location" {
  type        = string
  description = "Location of the azure resource group."
  default     = "WestEurope"
}

// Node type information

variable "node_count" {
  type        = string
  description = "The number of K8S nodes to provision."
  default     = 3
}

variable "node_type" {
  type        = string
  description = "The size of each node."
  default     = "Standard_D2s_v3"
}

variable "dns_prefix" {
  type        = string
  description = "DNS Prefix"
  default     = "tfq"
}