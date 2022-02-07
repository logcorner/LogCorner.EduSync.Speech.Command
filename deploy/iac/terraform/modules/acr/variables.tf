
variable "resource_group_name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "demo-tfquickstart"
}

variable "resource_group_location" {
  type        = string
  description = "Location of the azure resource group."
  default     = "WestEurope"
}

variable "acr_name" {
  default = "locornermsacr"
}

variable "sku" {
  default ="Standard"
}
variable "kubernetes_cluster_identity" {
  
}