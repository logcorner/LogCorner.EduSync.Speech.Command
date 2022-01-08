variable "agent_count" {
    default = 3
}

variable "dns_prefix" {
    default = "logcorner"
}


variable resource_group_name {
    default = "azure-k8stest"
}

variable location {
    default = "westeurope"
}

variable log_analytics_workspace_name {
    default = "testLogAnalyticsWorkspaceName"
}

# refer https://azure.microsoft.com/global-infrastructure/services/?products=monitor for log analytics available regions
variable log_analytics_workspace_location {
    default = "westeurope"
}

# refer https://azure.microsoft.com/pricing/details/monitor/ for log analytics pricing 
variable log_analytics_workspace_sku {
    default = "PerGB2018"
}