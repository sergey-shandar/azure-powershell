using Microsoft.Azure.Management.Storage.Models;

namespace Microsoft.Azure.Commands.Compute.Strategies.Storage
{
    static class StorageAccountStrategy
    {
        public static StorageAccountCreateParameters CreatePremiumLRS(string location)
            => new StorageAccountCreateParameters
            {
#if !NETSTANDARD
                AccountType = AccountType.PremiumLRS,
#else
                Sku = new SM.Sku
                {
                    Name = SkuName.PremiumLRS
                },
#endif
                Location = location
            };
    }
}
