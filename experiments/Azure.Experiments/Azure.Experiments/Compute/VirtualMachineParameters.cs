﻿using Microsoft.Azure.Management.Compute.Models;
using Microsoft.Azure.Experiments.Network;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Compute;

namespace Microsoft.Azure.Experiments.Compute
{
    public sealed class VirtualMachineParameters
        : ResourceParameters<VirtualMachine>
    {
        public NetworkInterfaceParameters Ni { get; }

        public VirtualMachineParameters(
            string name,
            ResourceGroupParameters resourceGroup,
            NetworkInterfaceParameters ni)
            : base(name, resourceGroup, new[] { ni })
        {
            Ni = ni;
        }

        protected override Task<VirtualMachine> GetAsync(
            Context context, IGetParameters _)
            => context
                .CreateCompute()
                .VirtualMachines
                .GetAsync(ResourceGroup.Name, Name);

        public override string GetLocation(VirtualMachine value)
            => value.Location;
    }
}
