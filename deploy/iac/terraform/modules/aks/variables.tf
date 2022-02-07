variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
  default     = "WestEurope"
}

variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "demo-tfquickstart"
}

// Node type information

variable "aks-name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "demo-tfquickstart-aks"
}

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

variable "environment" {
  type        = string
  description = "Name of the deployment environment"
  default     = "dev"
}