//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Management.Compute.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet("New", "AzureRmVmssIpConfig", SupportsShouldProcess = true)]
    [OutputType(typeof(Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetIPConfiguration))]
    public partial class NewAzureRmVmssIpConfigCommand : Microsoft.Azure.Commands.ResourceManager.Common.AzureRMCmdlet
    {
        [Parameter(
            Mandatory = false,
            Position = 0,
            ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public string Id { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string SubnetId { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true)]
        public string[] ApplicationGatewayBackendAddressPoolsId { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 4,
            ValueFromPipelineByPropertyName = true)]
        public string[] LoadBalancerBackendAddressPoolsId { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 5,
            ValueFromPipelineByPropertyName = true)]
        public string[] LoadBalancerInboundNatPoolsId { get; set; }

        [Parameter(
            Mandatory = false)]
        public SwitchParameter Primary { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        public string PrivateIPAddressVersion { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [Alias("PublicIPAddressName")]
        public string PublicIPAddressConfigurationName { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [Alias("PublicIPAddressIdleTimeoutInMinutes")]
        public int PublicIPAddressConfigurationIdleTimeoutInMinutes { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true)]
        [Alias("PublicIPAddressDomainNameLabel")]
        public string DnsSetting { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess("VirtualMachineScaleSet", "New"))
            {
                Run();
            }
        }

        private void Run()
        {
            var vIpConfigurations = new Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetIPConfiguration();

            vIpConfigurations.Name = this.MyInvocation.BoundParameters.ContainsKey("Name") ? this.Name : null;
            vIpConfigurations.Primary = this.Primary.IsPresent;
            vIpConfigurations.PrivateIPAddressVersion = this.MyInvocation.BoundParameters.ContainsKey("PrivateIPAddressVersion") ? this.PrivateIPAddressVersion : null;
            vIpConfigurations.Id = this.MyInvocation.BoundParameters.ContainsKey("Id") ? this.Id : null;

            // SubnetId
            if (this.MyInvocation.BoundParameters.ContainsKey("SubnetId"))
            {
                if (vIpConfigurations.Subnet == null)
                {
                    vIpConfigurations.Subnet = new Microsoft.Azure.Management.Compute.Models.ApiEntityReference();
                }

                vIpConfigurations.Subnet.Id = this.SubnetId;
            }

            // PublicIPAddressConfigurationName
            if (this.MyInvocation.BoundParameters.ContainsKey("PublicIPAddressConfigurationName"))
            {
                if (vIpConfigurations.PublicIPAddressConfiguration == null)
                {
                    vIpConfigurations.PublicIPAddressConfiguration = new Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetPublicIPAddressConfiguration();
                }

                vIpConfigurations.PublicIPAddressConfiguration.Name = this.PublicIPAddressConfigurationName;
            }

            // PublicIPAddressConfigurationIdleTimeoutInMinutes
            if (this.MyInvocation.BoundParameters.ContainsKey("PublicIPAddressConfigurationIdleTimeoutInMinutes"))
            {
                if (vIpConfigurations.PublicIPAddressConfiguration == null)
                {
                    vIpConfigurations.PublicIPAddressConfiguration = new Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetPublicIPAddressConfiguration();
                }

                vIpConfigurations.PublicIPAddressConfiguration.IdleTimeoutInMinutes = this.PublicIPAddressConfigurationIdleTimeoutInMinutes;
            }

            // DnsSetting
            if (this.MyInvocation.BoundParameters.ContainsKey("DnsSetting"))
            {
                if (vIpConfigurations.PublicIPAddressConfiguration == null)
                {
                    vIpConfigurations.PublicIPAddressConfiguration = new Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetPublicIPAddressConfiguration();
                }
                if (vIpConfigurations.PublicIPAddressConfiguration.DnsSettings == null)
                {
                    vIpConfigurations.PublicIPAddressConfiguration.DnsSettings = new Microsoft.Azure.Management.Compute.Models.VirtualMachineScaleSetPublicIPAddressConfigurationDnsSettings();
                }

                vIpConfigurations.PublicIPAddressConfiguration.DnsSettings.DomainNameLabel = this.DnsSetting;
            }

            // ApplicationGatewayBackendAddressPoolsId
            if (this.ApplicationGatewayBackendAddressPoolsId != null)
            {
                if (vIpConfigurations.ApplicationGatewayBackendAddressPools == null)
                {
                    vIpConfigurations.ApplicationGatewayBackendAddressPools = new List<Microsoft.Azure.Management.Compute.Models.SubResource>();
                }
                foreach (var element in this.ApplicationGatewayBackendAddressPoolsId)
                {
                    var vApplicationGatewayBackendAddressPools = new Microsoft.Azure.Management.Compute.Models.SubResource();
                    vApplicationGatewayBackendAddressPools.Id = element;
                    vIpConfigurations.ApplicationGatewayBackendAddressPools.Add(vApplicationGatewayBackendAddressPools);
                }
            }

            // LoadBalancerBackendAddressPoolsId
            if (this.LoadBalancerBackendAddressPoolsId != null)
            {
                if (vIpConfigurations.LoadBalancerBackendAddressPools == null)
                {
                    vIpConfigurations.LoadBalancerBackendAddressPools = new List<Microsoft.Azure.Management.Compute.Models.SubResource>();
                }
                foreach (var element in this.LoadBalancerBackendAddressPoolsId)
                {
                    var vLoadBalancerBackendAddressPools = new Microsoft.Azure.Management.Compute.Models.SubResource();
                    vLoadBalancerBackendAddressPools.Id = element;
                    vIpConfigurations.LoadBalancerBackendAddressPools.Add(vLoadBalancerBackendAddressPools);
                }
            }

            // LoadBalancerInboundNatPoolsId
            if (this.LoadBalancerInboundNatPoolsId != null)
            {
                if (vIpConfigurations.LoadBalancerInboundNatPools == null)
                {
                    vIpConfigurations.LoadBalancerInboundNatPools = new List<Microsoft.Azure.Management.Compute.Models.SubResource>();
                }
                foreach (var element in this.LoadBalancerInboundNatPoolsId)
                {
                    var vLoadBalancerInboundNatPools = new Microsoft.Azure.Management.Compute.Models.SubResource();
                    vLoadBalancerInboundNatPools.Id = element;
                    vIpConfigurations.LoadBalancerInboundNatPools.Add(vLoadBalancerInboundNatPools);
                }
            }

            WriteObject(vIpConfigurations);
        }
    }
}

