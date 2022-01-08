// Naming
variable "name" {
  type        = string
  description = "Location of the azure resource group."
  default     = "terraform"
}

variable "environment" {
  type        = string
  description = "Name of the deployment environment"
  default     = "dev"
}

